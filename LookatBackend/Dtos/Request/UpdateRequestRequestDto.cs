using LookatBackend.Models;

namespace LookatBackend.Dtos.UpdateRequestRequestDto
{
    public class UpdateRequestRequestDto
    {
        public int RequestType { get; set; }
        public int DocumentId { get; set; }
        public int Quantity { get; set; }
        public bool IsPending { get; set; }
    }
}
