using LookatBackend.Dtos.Barangay.BarangayDto;
using LookatBackend.Dtos.Barangay.CreateBarangayRequestDto;
using LookatBackend.Dtos.Barangay.UpdateBarangayRequestDto;
using LookatBackend.Models;

namespace LookatBackend.Interfaces
{
    public interface IBarangayService
    {
        Task<List<BarangayDto>> GetAllBarangaysAsync();
        Task<BarangayDto> GetBarangayByIdAsync(string id);
        Task<BarangayDto> CreateBarangayAsync(CreateBarangayRequestDto barangayDto);
        Task<BarangayDto?> UpdateBarangayAsync(string id, UpdateBarangayRequestDto barangayDto);
        Task<Barangay?> DeleteBarangayAsync(string id);
    }
}
