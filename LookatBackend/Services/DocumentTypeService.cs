using LookatBackend.Dtos.DocumentType.CreateDocumentTypeRequestDto;
using LookatBackend.Dtos.DocumentType.UpdateDocumentTypeRequestDto;
using LookatBackend.Interfaces;
using LookatBackend.Mappers;
using LookatBackend.Dtos.DocumentType;

namespace LookatBackend.Services.DocumentType
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;

        public DocumentTypeService(IDocumentTypeRepository documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }

        public async Task<List<DocumentTypeDto>> GetAllAsync()
        {
            var documentTypes = await _documentTypeRepository.GetAllAsync();
            return documentTypes.Select(d => d.ToDocumentTypeDto()).ToList();
        }

        public async Task<DocumentTypeDto?> GetByIdAsync(int id)
        {
            var documentType = await _documentTypeRepository.GetByIdAsync(id);
            return documentType?.ToDocumentTypeDto();
        }

        public async Task<DocumentTypeDto> CreateAsync(CreateDocumentTypeRequestDto documentTypeDto)
        {
            var documentType = documentTypeDto.ToDocumentTypeFromCreateDto();
            var createdDocumentType = await _documentTypeRepository.CreateAsync(documentType);
            return createdDocumentType.ToDocumentTypeDto();
        }

        public async Task<DocumentTypeDto?> UpdateAsync(int id, UpdateDocumentTypeRequestDto documentTypeDto)
        {
            var updatedDocumentType = await _documentTypeRepository.UpdateAsync(id, documentTypeDto);
            return updatedDocumentType?.ToDocumentTypeDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deletedDocumentType = await _documentTypeRepository.DeleteAsync(id);
            return deletedDocumentType != null;
        }
    }
}
