using LookatBackend.Dtos.Barangay.UpdateBarangayRequestDto;
using LookatBackend.Dtos.Barangay.CreateBarangayRequestDto;
using LookatBackend.Models;

namespace LookatBackend.Interfaces
{
    public interface IBarangayRepository
    {
        Task<List<Barangay>> GetAllAsync();
        Task<Barangay?> GetByIdAsync(string id);
        Task<Barangay> CreateAsync(Barangay barangayModel);
        Task<Barangay?> UpdateAsync(string id, UpdateBarangayRequestDto barangayDto);
        Task<Barangay?> DeleteAsync(string id);
    }

}