using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookatBackend.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }

        [Required]
        public int MobileNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string PhysicalIdNumber {  get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Purok { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string BarangayLoc { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string CityMunicipality { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Province { get; set; }

        [NotMapped]
        public string FullAddress => $"{Purok}, {BarangayLoc}, {CityMunicipality}, {Province}";

        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        public bool IsVerified { get; set; }

        [NotMapped]
        public IFormFile ProfilePicture { get; set; }

        [Required]
        [ForeignKey("Barangay")]
        public string BarangayId { get; set; }
        public Barangay Barangay {  get; set; }

        

    }
}
