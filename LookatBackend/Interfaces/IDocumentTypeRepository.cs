using LookatBackend.Dtos.DocumentType.UpdateDocumentTypeRequestDto;
using LookatBackend.Dtos.DocumentType.CreateDocumentTypeRequestDto;
using LookatBackend.Models;

namespace LookatBackend.Interfaces
{
    public interface IDocumentTypeRepository
    {
        Task<List<DocumentType>> GetAllAsync();
        Task<DocumentType?> GetByIdAsync(int id);
        Task<DocumentType> CreateAsync(DocumentType documentTypeModel);
        Task<DocumentType?> UpdateAsync(int id, UpdateDocumentTypeRequestDto documentTypeDto);
        Task<DocumentType?> DeleteAsync(int id);
    }

}