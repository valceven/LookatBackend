using Microsoft.AspNetCore.Mvc;
using LookatBackend.Dtos.User;
using LookatBackend.Dtos.UpdateUser;
using LookatBackend.Interfaces;
using LookatBackend.Mappers;
using LookatBackend.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Microsoft.Win32;
using LookatBackend.Services;
using static System.Net.WebRequestMethods;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly LookatDbContext _context;
        private readonly EmailService _emailService;

        public UserController(IUserRepository userRepo, LookatDbContext context, EmailService emailService)
        {
            _userRepo = userRepo;
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _userRepo.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToUserDto());
        }

        [HttpPost("request-otp")]
        public async Task<IActionResult> RequestOtp([FromBody] RegisterRequestDto registerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the email is already registered
            if (await _context.Users.AnyAsync(u => u.Email == registerRequest.Email))
                return BadRequest("Email is already registered.");

            // Generate OTP
            var otp = new Random().Next(1000, 10000);

            // Save OTP with expiration time
            var otpRecord = new OtpRecords
            {
                Email = registerRequest.Email,
                Otp = otp,
                ExpirationTime = DateTime.UtcNow.AddMinutes(5)
            };

            _context.OtpRecords.Add(otpRecord);
            await _context.SaveChangesAsync();

            // Send OTP email
            var subject = "Your OTP Code";
            var body = $"Your OTP code is {otp}. It will expire in 5 minutes.";

            try
            {
                await _emailService.SendEmailAsync(registerRequest.Email, subject, body);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to send email: " + ex.Message);
            }

            return Ok(new { Message = "OTP sent to your email. Please verify to complete registration." });
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDto verifyOtpRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validate OTP
            var otpRecord = await _context.OtpRecords
                .FirstOrDefaultAsync(o => o.Email == verifyOtpRequest.UserDto.Email && o.Otp == verifyOtpRequest.Otp);

            if (otpRecord == null)
                return BadRequest("Invalid OTP.");

            if (otpRecord.ExpirationTime < DateTime.UtcNow)
                return BadRequest("OTP has expired. Please request a new one.");

            // If OTP is valid, mark as verified
            otpRecord.ExpirationTime = DateTime.UtcNow; // Invalidate OTP
            _context.OtpRecords.Update(otpRecord);

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(verifyOtpRequest.UserDto.Password);
            // Register the user
            var user = new User
            {
                Email = verifyOtpRequest.UserDto.Email,
                Password = hashedPassword // Hash the password for security
                                          // You can add other fields here (like Name, Phone Number, etc.)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await _context.SaveChangesAsync();

            return Ok(new { Message = "OTP verified successfully. User registered successfully." });
        }


        

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VerifyOtpRequestDto verifyRegisterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Step 1: Check if the OTP record exists
            var otpRecord = await _context.OtpRecords
                .FirstOrDefaultAsync(o => o.Email == verifyRegisterDto.UserDto.Email
                                           && o.Otp == verifyRegisterDto.Otp);

            if (otpRecord == null)
                return BadRequest("Invalid OTP.");

            // Step 2: Check if the OTP has expired
            if (otpRecord.ExpirationTime < DateTime.UtcNow)
                return BadRequest("OTP has expired. Please request a new one.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(verifyRegisterDto.UserDto.Password);

            // Create a new user from the DTO
            var user = new User
            {
                MobileNumber = verifyRegisterDto.UserDto.MobileNumber,
                Password = hashedPassword,
                IsVerified = false // Default to unverified
            };

            // Step 5: Save the user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Step 6: Optionally, update the OTP record status (mark it as used or verified)

            _context.OtpRecords.Update(otpRecord);
            await _context.SaveChangesAsync();

            // Step 7: Return the created user excluding OTP and password
            return CreatedAtAction(nameof(Create), new { id = user.UserId }, new { user.UserId, user.MobileNumber, user.IsVerified });
        }






        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequestDto updateDto)
        {
            var userModel = await _userRepo.UpdateAsync(id, updateDto);

            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel.ToUserDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userModel = await _userRepo.DeleteAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Find user by email
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginRequest.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                return Unauthorized("Invalid credentials.");
            }

            // Create JWT token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.IsVerified == true ? "User" : "Admin") // Assuming IsVerified is for role or any other condition
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHere")); // Secret key should be same as in Program.cs
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "YourIssuer",
                audience: "YourAudience",
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token expiration time
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }



    }
}
