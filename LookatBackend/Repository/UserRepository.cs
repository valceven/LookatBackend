using LookatBackend.Dtos.UpdateUser;
using LookatBackend.Interfaces;
using LookatBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LookatBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LookatDbContext _context;

        public UserRepository(LookatDbContext context)
        {
            _context = context;
        }

        // Get all users
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Get user by ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Create a new user with password hashing
        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Update an existing user
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserRequestDto user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return NotFound();

            // Only update properties that are not null
            existingUser.FirstName = user.FirstName ?? existingUser.FirstName;
            existingUser.LastName = user.LastName ?? existingUser.LastName;
            existingUser.Purok = user.Purok ?? existingUser.Purok;
            existingUser.BarangayLoc = user.BarangayLoc ?? existingUser.BarangayLoc;
            existingUser.CityMunicipality = user.CityMunicipality ?? existingUser.CityMunicipality;
            existingUser.Province = user.Province ?? existingUser.Province;
            existingUser.Email = user.Email ?? existingUser.Email;
            existingUser.PhysicalIdNumber = user.PhysicalIdNumber ?? existingUser.PhysicalIdNumber;
            existingUser.IdType = user.IdType ?? existingUser.IdType;
            existingUser.Date = user.Date ?? existingUser.Date;

            await _context.SaveChangesAsync();
            return Ok(existingUser);
        }


        // Delete a user
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}
