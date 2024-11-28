using LookatBackend.Dtos.User;
using LookatBackend.Models;

namespace LookatBackend.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                UserId = userModel.UserId, // add late
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

        public static User ToUserFromCreateDto(this CreateUserRequestDto userDto)
        {
            return new User
            {
                UserName = userDto.UserName,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName, 
                Password = userDto.Password,
                MobileNumber = userDto.MobileNumber,
                Date = userDto.Date,
                PhysicalIdNumber = userDto.PhysicalIdNumber,
                Purok = userDto.Purok,
                BarangayLoc = userDto.BarangayLoc,
                CityMunicipality = userDto.CityMunicipality,
                Province = userDto.Province,
                Email = userDto.Email
            };
        }
    }

    
}

