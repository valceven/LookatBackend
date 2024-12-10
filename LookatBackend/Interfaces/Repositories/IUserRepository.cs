using LookatBackend.Dtos.UpdateUser;
using LookatBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LookatBackend.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(int id, UpdateUserRequestDto updateDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
