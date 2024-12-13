using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookatBackend.Models
{
    public class DocumentType
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string DocumentName { get; set; }

        [Required]
        public double Price { get; set; }

        public Boolean IsAvailable { get; set; }

        [ForeignKey("Barangay")]
        public string BarangayId { get; set; } // Foreign key to Barangay model
        public Barangay Barangay { get; set; } // Navigation property to Barangay
    }
}
