using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookatBackend.Dtos.User
{
    public class RegisterDto
    {
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        //public UserDto UserDto { get; set; }
        //public VerifyOtpRequestDto VerifyOtpRequestDto { get; set; }
    }
}
