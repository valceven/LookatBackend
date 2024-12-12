using System.ComponentModel.DataAnnotations;

namespace LookatBackend.Dtos.User
{
    public class LoginRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
