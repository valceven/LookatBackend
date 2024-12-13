using System.ComponentModel.DataAnnotations;

namespace LookatBackend.Dtos.User
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        // Add any other registration-related fields, such as full name, phone number, etc.
    }

}