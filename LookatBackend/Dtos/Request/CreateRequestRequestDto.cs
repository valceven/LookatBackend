

namespace LookatBackend.Dtos.CreateRequestRequestDto
{
    public class CreateRequestRequestDto
    {
        public int RequestType { get; set; }
        public int DocumentId { get; set; }
        public int Quantity { get; set; }
        public string? BarangayId { get; set; }
    }
}
