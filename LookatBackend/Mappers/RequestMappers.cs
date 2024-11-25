using LookatBackend.Dtos.CreateRequestRequestDto;
using LookatBackend.Dtos.Request;
using LookatBackend.Models;

namespace LookatBackend.Mappers
{
    public static class RequestMappers
    {
        public static RequestDto ToRequestDto(this Request requestModel)
        {
            return new RequestDto
            {
                RequestId = requestModel.RequestId,
                RequestType = requestModel.RequestType,
                DocumentId = requestModel.DocumentId,
                Quantity = requestModel.Quantity
            };
        }

        public static Request ToRequestFromCreateDto(this CreateRequestRequestDto requestDto)
        {
            return new Request
            {
                RequestType = requestDto.RequestType,
                DocumentId = requestDto.DocumentId,
                Quantity = requestDto.Quantity
            };
        }
    }
}
