namespace LookatBackend.Dtos.Request
{
    public class RequestDto
    {
        public int RequestId { get; set; }
        public int RequestType { get; set; }
        public int DocumentId { get; set; }
        public int Quantity { get; set; }
        public bool IsPending { get; set; }
        public string? BarangayId { get; set; }
    }
}
