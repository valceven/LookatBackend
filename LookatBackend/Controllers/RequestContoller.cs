using Microsoft.AspNetCore.Mvc;
using LookatBackend.Models;
using LookatBackend.Dtos.CreateRequestRequestDto;
using LookatBackend.Mappers;
using LookatBackend.Dtos.UpdateRequestRequestDto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly LookatDbContext _context;
        public RequestController(LookatDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _context.Requests.ToListAsync();
            var requestDtos = requests.Select(r => r.ToRequestDto());

            return Ok(requestDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request.ToRequestDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequestRequestDto requestDto)
        {
            var requestModel = requestDto.ToRequestFromCreateDto();
            await _context.Requests.AddAsync(requestModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = requestModel.RequestId }, requestModel.ToRequestDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRequestRequestDto requestDto)
        {
            var requestModel = await _context.Requests.FirstOrDefaultAsync(x => x.RequestId == id);

            if (requestModel == null)
            {
                return NotFound();
            }

            requestModel.RequestType = requestDto.RequestType;
            requestModel.DocumentId = requestDto.DocumentId;
            requestModel.Quantity = requestDto.Quantity;

            await _context.SaveChangesAsync();

            return Ok(requestModel.ToRequestDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var requestModel = await _context.Requests.FirstOrDefaultAsync(x => x.RequestId == id);

            if (requestModel == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(requestModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
