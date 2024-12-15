using LookatBackend.Dtos.User;
using LookatBackend.Dtos.UpdateUser;
using LookatBackend.Interfaces;
using LookatBackend.Models;
using LookatBackend.Mappers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LookatBackend.Services.User
{
    public class UsersService : IUserService
    {
        private readonly IUserRepository _usersRepository;
      

        public UsersService(IUserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        // Get all users
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _usersRepository.GetAllUsersAsync();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                userDtos.Add(user.ToUserDto()); // Using the mapper to convert User to UserDto
            }

            return userDtos;
        }

        // Get user by ID
        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _usersRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            return user.ToUserDto(); // Using the mapper to convert User to UserDto
        }

        // Create a new user
        public async Task<UserDto> CreateUserAsync(CreateUserRequestDto userDto)
        {
            var user = userDto.ToUserFromCreateDto(); // Using the mapper to convert CreateUserRequestDto to User
            var createdUser = await _usersRepository.CreateUserAsync(user);

            return createdUser.ToUserDto(); // Using the mapper to convert User to UserDto
        }

        // Update an existing user
        public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserRequestDto updateDto)
        {
            
            var updatedUser = await _usersRepository.UpdateUserAsync(id, updateDto);            
            return updatedUser?.ToUserDto(); // Using the mapper to convert User to UserDto
        }

        // Delete a user
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _usersRepository.DeleteUserAsync(id);
        }

        





    }
}
