using LookatBackend.Dtos.CreateRequestRequestDto;
using LookatBackend.Dtos.UpdateRequestRequestDto;
using LookatBackend.Interfaces;
using LookatBackend.Mappers;
using System.Collections.Generic;
using System.Threading.Tasks;
using LookatBackend.Dtos.Request;

namespace LookatBackend.Services.Request
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<List<RequestDto>> GetAllAsync()
        {
            var requests = await _requestRepository.GetAllAsync();
            return requests.Select(r => r.ToRequestDto()).ToList();
        }

        public async Task<RequestDto?> GetByIdAsync(int id)
        {
            var request = await _requestRepository.GetByIdAsync(id);
            return request?.ToRequestDto();
        }

        public async Task<RequestDto> CreateAsync(CreateRequestRequestDto requestDto)
        {
            var request = requestDto.ToRequestFromCreateDto();
            var createdRequest = await _requestRepository.CreateAsync(request);
            return createdRequest.ToRequestDto();
        }

        public async Task<RequestDto?> UpdateAsync(int id, UpdateRequestRequestDto requestDto)
        {
            var updatedRequest = await _requestRepository.UpdateAsync(id, requestDto);
            return updatedRequest?.ToRequestDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _requestRepository.DeleteAsync(id);
            return deleted != null;
        }
    }
}
