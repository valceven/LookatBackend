using LookatBackend.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LookatBackend.Dtos.Request
{
    public class RequestDto
    {
        public int RequestId { get; set; }
        public int RequestType { get; set; }
        public int DocumentId { get; set; }
        public int Quantity { get; set; }
    }
}
