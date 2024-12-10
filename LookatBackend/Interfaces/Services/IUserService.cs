using LookatBackend.Dtos.User;
using LookatBackend.Dtos.UpdateUser;
using LookatBackend.Models;

namespace LookatBackend.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserDto> RegisterUserAsync(RegisterDto registerDto);
        Task<UserDto> CreateUserAsync(CreateUserRequestDto createUserRequestDto);
        Task<UserDto?> UpdateUserAsync(int id, UpdateUserRequestDto updateDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
