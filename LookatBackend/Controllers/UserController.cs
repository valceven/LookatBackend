using LookatBackend.Dtos.UpdateUser;
using LookatBackend.Dtos.User;
using LookatBackend.Interfaces;
using LookatBackend.Models;
using LookatBackend.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly LookatDbContext _context;
        private readonly EmailService _emailService;

        public UserController(IUserService userService, LookatDbContext context, EmailService emailService)
        {
            _userService = userService;
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(registerDto);
                return Ok(new { Message = "OTP sent. Please verify to complete registration.", user.MobileNumber });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            try
            {
                var user = await _userService.CreateUserAsync(userDto);
                return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequestDto updateDto)
        {
            var user = await _userService.UpdateUserAsync(id, updateDto);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
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
                Password = hashedPassword, // Hash the password for security
                                          // You can add other fields here (like Name, Phone Number, etc.)
                UserName = verifyOtpRequest.UserDto.UserName,
                FirstName = verifyOtpRequest.UserDto.FirstName,
                LastName = verifyOtpRequest.UserDto.LastName,
                MobileNumber = verifyOtpRequest.UserDto.MobileNumber,
                Date = verifyOtpRequest.UserDto.Date,
                PhysicalIdNumber = verifyOtpRequest.UserDto.PhysicalIdNumber,
                Purok = verifyOtpRequest.UserDto.Purok,
                BarangayLoc = verifyOtpRequest.UserDto.BarangayLoc,
                CityMunicipality = verifyOtpRequest.UserDto.CityMunicipality,
                Province = verifyOtpRequest.UserDto.Province,
                IsVerified = verifyOtpRequest.UserDto.IsVerified,
                //BarangayId = verifyOtpRequest.UserDto.BarangayId,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await _context.SaveChangesAsync();

            return Ok(new { Message = "OTP verified successfully. User registered successfully." });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Find user by email
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

            if (user == null)
                return BadRequest("Invalid email or password.");

            // Verify password using BCrypt
            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
                return BadRequest("Invalid password.");

            HttpContext.Session.SetString("UserId", user.UserId.ToString());


            return Ok(user);
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the email is already registered
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userRequest.Email);

            if (user == null)
                return BadRequest("No user found with this email");

            // Generate OTP
            var otp = new Random().Next(1000, 10000);

            // Save OTP with expiration time
            var otpRecord = new OtpRecords
            {
                Email = userRequest.Email,
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
                await _emailService.SendEmailAsync(userRequest.Email, subject, body);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to send email: " + ex.Message);
            }

            return Ok(new { Message = "OTP sent to your email. Please verify to complete changing of password." });
        }



        [HttpPost("password-otp-verify")]
        public async Task<IActionResult> ChangePasswordOTP([FromBody] UserChangePassDto verifyOtpRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validate OTP

            var otpRecord = await _context.OtpRecords
                .FirstOrDefaultAsync(o => o.Otp == verifyOtpRequest.Otp);

            if (otpRecord == null)
                return BadRequest("Invalid OTP.");

            if (otpRecord.ExpirationTime < DateTime.UtcNow)
                return BadRequest("OTP has expired. Please request a new one.");

            // If OTP is valid, mark as verified
            otpRecord.ExpirationTime = DateTime.UtcNow; // Invalidate OTP
            _context.OtpRecords.Update(otpRecord);



            return Ok(new { Message = "OTP verified successfully" });
        }


        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Ensure the email exists and retrieve the user
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == changePasswordDto.Email);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            // Hash the new password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.Password);

            // Update the password in the user entity
            user.Password = hashedPassword;

            // Save changes to the database
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Password updated successfully" });
        }







    }
}
