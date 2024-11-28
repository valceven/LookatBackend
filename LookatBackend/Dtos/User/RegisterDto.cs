using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookatBackend.Dtos.User
{
    public class RegisterDto
    {
        [Required]
        [Column(TypeName = "nvarchar(15)")]
        public string MobileNumber { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }
    }
}
