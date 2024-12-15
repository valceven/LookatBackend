using LookatBackend.Dtos.CreateRequestRequestDto;
using LookatBackend.Dtos.UpdateRequestRequestDto;
using LookatBackend.Models;

namespace LookatBackend.Interfaces
{
    public interface IRequestRepository
    {
        Task<List<Request>> GetAllAsync();
        Task<Request?> GetByIdAsync(int id);
        Task<List<Request>> GetAllByBarangayId(string barangayId);
        Task<Request> CreateAsync(Request requestModel);
        Task<Request?> UpdateAsync(int id, UpdateRequestRequestDto requestDto);
        Task<Request?> DeleteAsync(int id);
    }

}