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
                FirstName = userModel.FirstName ?? "N/A", // Default to "N/A" if null
                LastName = userModel.LastName ?? "N/A",
                UserName = userModel.UserName ?? "N/A",
                MobileNumber = userModel.MobileNumber ?? "N/A",
                Date = userModel.Date, // Use default DateTime value if null
                PhysicalIdNumber = userModel.PhysicalIdNumber ?? "N/A",
                Purok = userModel.Purok ?? "N/A",
                BarangayLoc = userModel.BarangayLoc ?? "N/A",
                CityMunicipality = userModel.CityMunicipality ?? "N/A",
                Province = userModel.Province ?? "N/A",
                Email = userModel.Email ?? "N/A",
                IsVerified = userModel.IsVerified
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
                Date = (DateTime)userDto.Date,
                PhysicalIdNumber = userDto.PhysicalIdNumber,
                Purok = userDto.Purok,
                BarangayLoc = userDto.BarangayLoc,
                CityMunicipality = userDto.CityMunicipality,
                Province = userDto.Province,
                Email = userDto.Email,
                IsVerified = userDto.IsVerified

                // Optional fields like ProfilePicture can be handled later
            };
        }
    }
}
