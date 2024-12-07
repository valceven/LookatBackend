using Microsoft.AspNetCore.Mvc;
using LookatBackend.Dtos.CreateRequestRequestDto;
using LookatBackend.Dtos.UpdateRequestRequestDto;
using LookatBackend.Interfaces;
using LookatBackend.Mappers;

namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepository _requestRepository;


        public RequestController(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _requestRepository.GetAllAsync();
            var requestDtos = requests.Select(r => r.ToRequestDto());

            return Ok(requestDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var request = await _requestRepository.GetByIdAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request.ToRequestDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequestRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Request data is required.");
            }

            var requestModel = requestDto.ToRequestFromCreateDto();
            var createdRequest = await _requestRepository.CreateAsync(requestModel);

            return CreatedAtAction(nameof(Get), new { id = createdRequest.RequestId }, createdRequest.ToRequestDto());
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRequestRequestDto requestDto)
        {
            var updatedRequest = await _requestRepository.UpdateAsync(id, requestDto);

            if (updatedRequest == null)
            {
                return NotFound();
            }

            return Ok(updatedRequest.ToRequestDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deletedRequest = await _requestRepository.DeleteAsync(id);

            if (deletedRequest == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
