using LookatBackend.Dtos.Barangay.BarangayDto;
using LookatBackend.Models;
using LookatBackend.Dtos.Barangay.CreateBarangayRequestDto;
namespace LookatBackend.Mappers
{
    public static class BarangayMappers
    {
        public static BarangayDto ToBarangayDto(this Barangay barangayModel)
        {
            return new BarangayDto
            {
                BarangayId = barangayModel.BarangayId,
                BarangayName = barangayModel.BarangayName,
                Purok = barangayModel.Purok,
                BarangayLoc = barangayModel.BarangayLoc,
                CityMunicipality = barangayModel.CityMunicipality,
                Province = barangayModel.Province
            };
        }

        public static Barangay ToBarangayFromCreateDto(this CreateBarangayRequestDto barangayDto)
        {
            return new Barangay
            {
                BarangayName = barangayDto.BarangayName,
                Purok = barangayDto.Purok,
                BarangayLoc = barangayDto.BarangayLoc,
                CityMunicipality = barangayDto.CityMunicipality,
                Province = barangayDto.Province
            };
        }
    }
}
