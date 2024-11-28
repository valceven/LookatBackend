using LookatBackend.Dtos.UpdateUser;
using LookatBackend.Dtos.User;
using LookatBackend.Models;

namespace LookatBackend.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> CreateAsync(User userModel);
        Task<User?> UpdateAsync(int id, UpdateUserRequestDto userDto);
        Task<User?> DeleteAsync(int id);
    }

}