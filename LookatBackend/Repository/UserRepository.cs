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
        public async Task<User> UpdateUserAsync(int id, UpdateUserRequestDto user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return null;

            // Only update properties that are not null
            if (user.FirstName != null) existingUser.FirstName = user.FirstName;
            if (user.LastName != null) existingUser.LastName = user.LastName;
            existingUser.IsVerified = user.IsVerified;
            if (user.Purok != null) existingUser.Purok = user.Purok;
            if (user.BarangayLoc != null) existingUser.BarangayLoc = user.BarangayLoc;
            if (user.CityMunicipality != null) existingUser.CityMunicipality = user.CityMunicipality;
            if (user.Province != null) existingUser.Province = user.Province;
            if (user.Email != null) existingUser.Email = user.Email;
            if (user.PhysicalIdNumber != null) existingUser.PhysicalIdNumber = user.PhysicalIdNumber;
            if (user.IdType != null) existingUser.IdType = user.IdType;
            if (user.Date.HasValue) existingUser.Date = user.Date;

            await _context.SaveChangesAsync();
            return existingUser;
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
