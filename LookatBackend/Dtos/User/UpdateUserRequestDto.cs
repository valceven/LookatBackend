
namespace LookatBackend.Dtos.UpdateUser
{
    public class UpdateUserRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public DateTime Date { get; set; }
        public string PhysicalIdNumber { get; set; }
        public string Purok { get; set; }
        public string BarangayLoc { get; set; }
        public string CityMunicipality { get; set; }
        public string Province { get; set; }
        public string FullAddress => $"{Purok}, {BarangayLoc}, {CityMunicipality}, {Province}";
        public string Email { get; set; }
    }
}