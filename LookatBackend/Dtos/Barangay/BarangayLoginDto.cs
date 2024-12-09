using System.ComponentModel.DataAnnotations;

namespace LookatBackend.Dtos.Barangay.BarangayLoginDto
{
    public class LoginDTO
    {
        [Required]
        public string BarangayId { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }

}