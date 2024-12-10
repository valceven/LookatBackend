using LookatBackend.Dtos.Barangay.BarangayDto;
using LookatBackend.Dtos.Barangay.CreateBarangayRequestDto;
using LookatBackend.Dtos.Barangay.UpdateBarangayRequestDto;
using LookatBackend.Interfaces;
using LookatBackend.Mappers;
using LookatBackend.Models;

namespace LookatBackend.Services
{
    public class BarangayService : IBarangayService
    {
        private readonly IBarangayRepository _barangayRepository;

        public BarangayService(IBarangayRepository barangayRepository)
        {
            _barangayRepository = barangayRepository;
        }

        public async Task<List<BarangayDto>> GetAllBarangaysAsync()
        {
            var barangayModels = await _barangayRepository.GetAllAsync(); 
            return barangayModels.Select(m => m.ToBarangayDto()).ToList(); 
        }


        public async Task<BarangayDto> CreateBarangayAsync(CreateBarangayRequestDto barangayDto)
        {
            barangayDto.Password = $"Barangay{barangayDto.BarangayLoc}@123";
            var barangayModel = barangayDto.ToBarangayFromCreateDto();  // Data transformation

            var createdBarangay = await _barangayRepository.CreateAsync(barangayModel);
            return createdBarangay.ToBarangayDto();  // Return the created Barangay as a DTO
        }


        public async Task<Barangay?> DeleteBarangayAsync(string id)
        {
            return await _barangayRepository.DeleteAsync(id);
        }

        public async Task<BarangayDto> GetBarangayByIdAsync(string id)
        {
            var barangayModel = await _barangayRepository.GetByIdAsync(id);
            if (barangayModel == null) return null;
            return barangayModel.ToBarangayDto(); 
        }


        public async Task<BarangayDto?> UpdateBarangayAsync(string id, UpdateBarangayRequestDto barangayDto)
        {
            var barangayModel = await _barangayRepository.UpdateAsync(id, barangayDto);
            if (barangayModel == null) return null; 
            return barangayModel.ToBarangayDto();
        }

    }
}
