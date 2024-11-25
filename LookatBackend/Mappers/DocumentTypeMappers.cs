using LookatBackend.Dtos.DocumentType;
using LookatBackend.Models;
using LookatBackend.Dtos.DocumentType.CreateDocumentTypeRequestDto;

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
                Price = documentTypeModel.Price
            };
        }

        public static DocumentType ToDocumentTypeFromCreateDto(this CreateDocumentTypeRequestDto documentTypeDto)
        {
            return new DocumentType
            {
                DocumentName = documentTypeDto.DocumentName,
                Price = documentTypeDto.Price
            };
        }
    }
}
