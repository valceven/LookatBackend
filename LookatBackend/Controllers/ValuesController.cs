using Microsoft.AspNetCore.Mvc;
using LookatBackend.Dtos.User;
using LookatBackend.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace LookatBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly LookatDbContext _context;

        public RegistrationController(LookatDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            // Validate the incoming data
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the mobile number already exists
            if (await _context.Users.AnyAsync(u => u.MobileNumber == registerDto.MobileNumber))
                return BadRequest("Mobile number is already registered.");

            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            // Create a new user from the DTO
            var user = new User
            {
                MobileNumber = registerDto.MobileNumber,
                Password = hashedPassword,
                IsVerified = false // Default to unverified
            };

            // Save the new user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }
    }
}
