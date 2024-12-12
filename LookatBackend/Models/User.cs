using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookatBackend.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

  
        [Column(TypeName = "nvarchar(100)")]
        public string? UserName { get; set; }

     
        [Column(TypeName = "nvarchar(100)")]
        public string? FirstName { get; set; }

       
        [Column(TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }

     
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }

  
        [Column(TypeName = "nvarchar(15)")]
        public string? MobileNumber { get; set; } // Changed to string

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }  // Nullable DateTime

        [Column(TypeName = "nvarchar(25)")]
        public string? PhysicalIdNumber { get; set; }  // Nullable string

        [Column(TypeName = "nvarchar(100)")]
        public string? Purok { get; set; }  // Nullable string

        [Column(TypeName = "nvarchar(100)")]
        public string? BarangayLoc { get; set; }  // Nullable string

        [Column(TypeName = "nvarchar(100)")]
        public string? CityMunicipality { get; set; }  // Nullable string

        [Column(TypeName = "nvarchar(100)")]
        public string? Province { get; set; }  // Nullable string

        [NotMapped]
        public string FullAddress => $"{Purok}, {BarangayLoc}, {CityMunicipality}, {Province}";

        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        [Column(TypeName = "nvarchar(100)")]
        public string? Email { get; set; }  // Nullable string

        public bool? IsVerified { get; set; }  // Nullable boolean

        [NotMapped]
        public IFormFile? ProfilePicture { get; set; }  // Nullable IFormFile

        [ForeignKey("Barangay")]
        public string? BarangayId { get; set; }  // Nullable string
        public Barangay? Barangay { get; set; }  // Nullable navigation property
    }
}
