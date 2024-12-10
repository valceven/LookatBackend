using LookatBackend.Dtos.DocumentType;
using LookatBackend.Dtos.DocumentType.UpdateDocumentTypeRequestDto;
using LookatBackend.Models;

namespace LookatBackend.Interfaces
{
    public interface IDocumentTypeRepository
    {
        Task<List<DocumentType>> GetAllAsync();
        Task<DocumentType?> GetByIdAsync(int id);
        Task<DocumentType> CreateAsync(DocumentType documentType);
        Task<DocumentType?> UpdateAsync(int id, UpdateDocumentTypeRequestDto documentTypeDto);
        Task<DocumentType?> DeleteAsync(int id);
        Task<List<DocumentType>> GetAllByBarangays(string id);
        Task<bool> ExistsByNameAsync(string documentName, string barangayId);
    }

}