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
using static System.Net.WebRequestMethods;

namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly LookatDbContext _context;

        public UserController(IUserRepository userRepo, LookatDbContext context)
        {
            _userRepo = userRepo;
            _context = context;
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

        [HttpPost("register")] // THIS IS TEMPORARY BECAUSE NEED TO CHECK HOW TO SEND SMS
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

    
            if (await _context.Users.AnyAsync(u => u.MobileNumber == registerDto.MobileNumber))
                return BadRequest("Mobile number is already registered.");

            await _context.SaveChangesAsync();

            return Ok(new { Message = "OTP sent. Please verify to complete registration." });
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VerifyOtpRequestDto verifyRegisterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Step 1: Check if the OTP record exists
            var otpRecord = await _context.OtpRecords
                .FirstOrDefaultAsync(o => o.MobileNumber == verifyRegisterDto.UserDto.MobileNumber
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


    }
}
