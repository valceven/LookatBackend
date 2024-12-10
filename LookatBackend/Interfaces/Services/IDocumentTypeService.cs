using LookatBackend.Dtos.DocumentType.CreateDocumentTypeRequestDto;
using LookatBackend.Dtos.DocumentType.UpdateDocumentTypeRequestDto;
using LookatBackend.Dtos.DocumentType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LookatBackend.Interfaces
{
    public interface IDocumentTypeService
    {
        Task<List<DocumentTypeDto>> GetAllAsync();
        Task<DocumentTypeDto?> GetByIdAsync(int id);
        Task<DocumentTypeDto> CreateAsync(CreateDocumentTypeRequestDto documentTypeDto);
        Task<DocumentTypeDto?> UpdateAsync(int id, UpdateDocumentTypeRequestDto documentTypeDto);
        Task<bool> DeleteAsync(int id);
    }
}
