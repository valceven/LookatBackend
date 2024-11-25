using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LookatBackend.Dtos.Barangay.BarangayDto
{
    public class BarangayDto
    {
        public string BarangayId { get; set; }
        public string BarangayName { get; set; }
        public string Purok { get; set; }
        public string BarangayLoc { get; set; }
        public string CityMunicipality { get; set; }
        public string Province { get; set; }
    }
}
