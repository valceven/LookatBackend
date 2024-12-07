using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookatBackend.Models
{

    public class Barangay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string BarangayId { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 12); 

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }

        [Required]
        [DataType("nvarchar(150)")]
        public string BarangayName { get; set; }

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

    }
}
