namespace LookatBackend.Dtos.DocumentType
{
    public class CreateDocumentTypeRequestDto
    {
        public string DocumentName { get; set; }
        public double Price { get; set; }
        public string BarangayId { get; set; }

    }
}
