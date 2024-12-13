using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LookatBackend.Dtos.DocumentType
{
    public class DocumentTypeDto
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public double Price { get; set; }
        public string BarangayId { get; set; }

        public Boolean IsAvailable { get; set; }

    }
}
