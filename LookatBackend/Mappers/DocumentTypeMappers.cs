using LookatBackend.Dtos.DocumentType;
using LookatBackend.Models;

namespace LookatBackend.Mappers
{
    public static class DocumentTypeMappers
    {
        public static DocumentTypeDto ToDocumentTypeDto(this DocumentType documentTypeModel)
        {                

            return new DocumentTypeDto
            {
                DocumentId = documentTypeModel.DocumentId,
                DocumentName = documentTypeModel.DocumentName,
                Price = documentTypeModel.Price,
                IsAvailable = documentTypeModel.IsAvailable,
                BarangayId = documentTypeModel.BarangayId
            };
        }

        public static DocumentType ToDocumentTypeFromCreateDto(this CreateDocumentTypeRequestDto documentTypeDto)
        {
            return new DocumentType
            {
                DocumentName = documentTypeDto.DocumentName,
                Price = documentTypeDto.Price,
                IsAvailable = documentTypeDto.IsAvailable,
                BarangayId = documentTypeDto.BarangayId
            };
        }
    }
}
