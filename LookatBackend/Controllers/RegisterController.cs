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

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        //{
       
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

    
        //    if (await _context.Users.AnyAsync(u => u.MobileNumber == registerDto.MobileNumber))
        //        return BadRequest("Mobile number is already registered.");

         
        //    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

      
        //    var user = new User
        //    {
        //        MobileNumber = registerDto.MobileNumber,
        //        Password = hashedPassword,
        //        IsVerified = false 
        //    };

          
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(Register), new { id = user.UserId }, user);
        //}

    }
}
