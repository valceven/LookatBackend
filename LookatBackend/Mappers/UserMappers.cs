using LookatBackend.Dtos.User;
using LookatBackend.Models;
using BCrypt.Net;  // For password hashing

namespace LookatBackend.Mappers
{
    public static class UserMappers
    {
        // Mapper to convert User model to UserDto
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                UserId = userModel.UserId, // Assuming UserId is an auto-generated key
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                UserName = userModel.UserName,
                MobileNumber = userModel.MobileNumber,
                Date = userModel.Date,
                PhysicalIdNumber = userModel.PhysicalIdNumber,
                Purok = userModel.Purok,
                BarangayLoc = userModel.BarangayLoc,
                CityMunicipality = userModel.CityMunicipality,
                Province = userModel.Province,
                Email = userModel.Email
            };
        }

        // Mapper to convert CreateUserRequestDto to User model
        public static User ToUserFromCreateDto(this CreateUserRequestDto userDto)
        {
            return new User
            {
                UserName = userDto.UserName,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password), // Hashing password
                MobileNumber = userDto.MobileNumber,
                Date = userDto.Date,
                PhysicalIdNumber = userDto.PhysicalIdNumber,
                Purok = userDto.Purok,
                BarangayLoc = userDto.BarangayLoc,
                CityMunicipality = userDto.CityMunicipality,
                Province = userDto.Province,
                Email = userDto.Email
                // Optional fields like ProfilePicture can be handled later
            };
        }
    }
}
