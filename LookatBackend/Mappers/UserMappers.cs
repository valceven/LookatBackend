using LookatBackend.Dtos;
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
                Purok = userModel.Purok          
            };
        }

        public static User ToUserFromCreateDto(this CreateUserRequestDto userDto)
        {
            return new User
            {
                UserName = userDto.UserName,
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

