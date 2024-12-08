namespace LookatBackend.Dtos.Barangay.CreateBarangayRequestDto
{
    public class CreateBarangayRequestDto
    {
        public string BarangayName { get; set; }
        public string? Password { get; set; }
        public string Purok { get; set; }
        public string BarangayLoc { get; set; }
        public string CityMunicipality { get; set; }
        public string Province { get; set; }
    }
}
