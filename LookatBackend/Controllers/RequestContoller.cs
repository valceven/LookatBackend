using Microsoft.AspNetCore.Mvc;
using LookatBackend.Dtos.CreateRequestRequestDto;
using LookatBackend.Dtos.UpdateRequestRequestDto;
using LookatBackend.Interfaces;

namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _requestService.GetAllAsync();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var request = await _requestService.GetByIdAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        [HttpGet("by/{barangayId}")] 
        public async Task<IActionResult> GetAllByBarangay([FromRoute] string barangayId)
        {
            var request = await _requestService.GetAllByBarangayIdAsync(barangayId);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequestRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Request data is required.");
            }

            var createdRequest = await _requestService.CreateAsync(requestDto);
            return CreatedAtAction(nameof(Get), new { id = createdRequest.RequestId }, createdRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRequestRequestDto requestDto)
        {
            var updatedRequest = await _requestService.UpdateAsync(id, requestDto);

            if (updatedRequest == null)
            {
                return NotFound();
            }

            return Ok(updatedRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var success = await _requestService.DeleteAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
