using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookatBackend.Models
{
    public class Request
    {

        [Key]
        public int RequestId { get; set; }

        [ForeignKey("DocumentType")]
        [Required]
        public int requestType { get; set; }
        public DocumentType DocumentType { get; set; }


    }
}
