using LookatBackend.Dtos.UpdateUser;
using LookatBackend.Dtos.User;
using LookatBackend.Interfaces;
using LookatBackend.Interfaces.Services;
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
        private readonly OtpService _otpService;
        private readonly LoginService _loginService;

        public UserController(IUserService userService, LookatDbContext context, EmailService emailService, OtpService otpService, LoginService loginService)
        {
            _userService = userService;
            _context = context;
            _emailService = emailService;
            _otpService = otpService;
            _loginService = loginService;
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

            // Delegate email existence check to the service
            if (await _otpService.IsEmailRegisteredAsync(registerRequest.Email))
                return BadRequest("Email is already registered.");

            try
            {
                // Delegate OTP generation and email sending to the service
                await _otpService.GenerateAndSendOtpAsync(registerRequest.Email);
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

            try
            {
                await _otpService.VerifyOtpAndRegisterUserAsync(verifyOtpRequest);
                return Ok(new { Message = "OTP verified successfully. User registered successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginRequest)
        {
            // Call the service to handle the login logic
            return await _loginService.LoginAsync(loginRequest);
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userRequest.Email);

            if (user == null)
                return BadRequest("No user found with this email");


            try
            {
              
                await _otpService.GenerateAndSendOtpAsync(userRequest.Email);
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
            if(await _otpService.VerifyOtpForPasswordChangeAsync(verifyOtpRequest.Otp))
            {
                return Ok(new { Message = "OTP verified successfully" });
            }

            return BadRequest("Invalid OTP");

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
