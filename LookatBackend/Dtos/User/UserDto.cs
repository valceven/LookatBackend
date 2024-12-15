using LookatBackend.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LookatBackend.Dtos.User
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Password { get; set; } // REQUIRED FOR REGISTER
        public string MobileNumber { get; set; } /// REQUIRED FOR REGISTER
        public DateTime? Date { get; set; }
        public string? PhysicalIdNumber { get; set; }
        public string? Purok { get; set; }
        public string? BarangayLoc { get; set; }
        public string? CityMunicipality { get; set; }
        public string? Province { get; set; }
        public string? FullAddress => $"{Purok}, {BarangayLoc}, {CityMunicipality}, {Province}";
        public string? Email { get; set; }
        public bool? IsVerified { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public string? BarangayId { get; set; } // Made nullable
        //public Barangay Barangay { get; set; }
    }
}
