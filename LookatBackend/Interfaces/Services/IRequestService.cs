using LookatBackend.Dtos.CreateRequestRequestDto;
using LookatBackend.Dtos.UpdateRequestRequestDto;
using System.Collections.Generic;
using System.Threading.Tasks;
using LookatBackend.Dtos.Request;

namespace LookatBackend.Interfaces
{
    public interface IRequestService
    {
        Task<List<RequestDto>> GetAllAsync();
        Task<List<RequestDto>> GetAllByBarangayIdAsync(string barangayId);
        Task<RequestDto?> GetByIdAsync(int id);
        Task<RequestDto> CreateAsync(CreateRequestRequestDto requestDto);
        Task<RequestDto?> UpdateAsync(int id, UpdateRequestRequestDto requestDto);
        Task<bool> DeleteAsync(int id);
    }
}
