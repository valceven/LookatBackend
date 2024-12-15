using LookatBackend.Dtos.User;
using Microsoft.EntityFrameworkCore;
using LookatBackend.Interfaces.Services;
using LookatBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LookatBackend.Services
{
    public class LoginService : ILoginService
    {
        private readonly LookatDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(LookatDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> LoginAsync(UserLoginDto loginRequest)
        {
         

            // Find user by email
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

            if (user == null)
                return new BadRequestObjectResult("Invalid email or password.");

            // Verify password using BCrypt
            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
                return new BadRequestObjectResult("Invalid password.");

            // Store user ID in session
            _httpContextAccessor.HttpContext.Session.SetString("UserId", user.UserId.ToString());

            // Return the user object (or any other relevant information)
            return new OkObjectResult(user);
        }

        public async Task<bool> ChangePasswordAsync(string email, string newPassword)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return false;


            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);


            user.Password = hashedPassword;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
