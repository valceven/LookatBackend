using LookatBackend.Dtos.DocumentType.UpdateDocumentTypeRequestDto;
using LookatBackend.Dtos.DocumentType;

namespace LookatBackend.Interfaces
{
    public interface IDocumentTypeService
    {
        Task<List<DocumentTypeDto>> GetAllAsync();
        Task<DocumentTypeDto?> GetByIdAsync(int id);
        Task<DocumentTypeDto> CreateAsync(CreateDocumentTypeRequestDto documentTypeDto);
        Task<DocumentTypeDto?> UpdateAsync(int id, UpdateDocumentTypeRequestDto documentTypeDto);
        Task<List<DocumentTypeDto>> GetAllByBarangay(string id);
        Task<bool> DeleteAsync(int id);
    }
}
