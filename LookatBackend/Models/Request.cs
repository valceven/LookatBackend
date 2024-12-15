using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookatBackend.Models
{
    public class Request
    {

        [Key]
        public int RequestId { get; set; }
        [Required]
        public int RequestType { get; set; }

        [ForeignKey("DocumentType")]
        [Required]
        public int DocumentId { get; set; }
        public DocumentType DocumentType { get; set; }

        [Required]
        public string BarangayId { get; set; }

        public bool IsPending { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
