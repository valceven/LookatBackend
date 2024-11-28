using LookatBackend.Dtos.UpdateUser;
using LookatBackend.Dtos.User;
using LookatBackend.Interfaces;
using LookatBackend.Mappers;
using LookatBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LookatBackend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LookatDbContext _context;

        public UserRepository(LookatDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User userModel)
        {
            var existingUser = await _context.Users
                                             .FirstOrDefaultAsync(u => u.Email == userModel.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }

            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();

            return userModel;
        }

        public async Task<User?> DeleteAsync(int id)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (userModel == null)
            {
                return null;
            }

            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();

            return userModel;
        }

        public async Task<List<UserDto>> GetAllAsync()
{
            return await _context.Users
                .Select(u => u.ToUserDto())
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var userModel = await _context.Users.FindAsync(id);
            return userModel;
        }

        public async Task<User?> UpdateAsync(int id, UpdateUserRequestDto updateDto)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (userModel == null)
            {
                return null;
            }

            userModel.UserName = updateDto.UserName;
            userModel.FirstName = updateDto.FirstName;
            userModel.LastName = updateDto.LastName;
            userModel.Password = updateDto.Password;
            userModel.MobileNumber = updateDto.MobileNumber;
            //userModel.Date = updateDto.Date;
            userModel.PhysicalIdNumber = updateDto.PhysicalIdNumber;
            userModel.Purok = updateDto.Purok;
            userModel.BarangayLoc = updateDto.BarangayLoc;
            userModel.CityMunicipality = updateDto.CityMunicipality;
            userModel.Province = updateDto.Province;
            userModel.Email = updateDto.Email;

            await _context.SaveChangesAsync();

            return userModel;
        }

    }
}
