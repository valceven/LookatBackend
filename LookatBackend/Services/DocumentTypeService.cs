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
            var documentTypeDtos = documentTypes.Select(dt => dt.ToDocumentTypeDto()).ToList();
        
            return documentTypeDtos;
        }

        public async Task<List<DocumentTypeDto>> GetAllByBarangay(string id)
        {
            var documentTypes = await _documentTypeRepository.GetAllByBarangays(id);
            var documentTypeDtos = documentTypes.Select(dt => dt.ToDocumentTypeDto()).ToList();
        
            return documentTypeDtos;
        }

        public async Task<DocumentTypeDto?> GetByIdAsync(int id)
        {
            var documentType = await _documentTypeRepository.GetByIdAsync(id);
            return documentType?.ToDocumentTypeDto();
        }

        public async Task<DocumentTypeDto> CreateAsync(CreateDocumentTypeRequestDto documentTypeDto)
        {
            bool exists = await _documentTypeRepository.ExistsByNameAsync(documentTypeDto.DocumentName,documentTypeDto.BarangayId);

            if (exists)
            {
                throw new InvalidOperationException($"A document with the name '{documentTypeDto.DocumentName}' already exists.");
            }

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
