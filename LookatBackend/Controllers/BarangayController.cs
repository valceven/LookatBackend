using Microsoft.AspNetCore.Mvc;
using LookatBackend.Dtos.Barangay.CreateBarangayRequestDto;
using LookatBackend.Mappers;
using LookatBackend.Dtos.Barangay.UpdateBarangayRequestDto;
using LookatBackend.Interfaces;


namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BarangayController : ControllerBase
    {
        private readonly IBarangayRepository _barangayRepository;

        public BarangayController(IBarangayRepository barangayRepository)
        {
            _barangayRepository = barangayRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var barangays = await _barangayRepository.GetAllAsync();
            if (barangays == null || barangays.Count == 0)
            {
                return NotFound();
            }

            var barangayDtos = barangays.Select(b => b.ToBarangayDto());
            return Ok(barangayDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var barangay = await _barangayRepository.GetByIdAsync(id);
            if (barangay == null)
            {
                return NotFound();
            }

            return Ok(barangay.ToBarangayDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBarangayRequestDto barangayDto)
        {
            if (barangayDto == null)
            {
                return BadRequest("Barangay data is required.");
            }

            barangayDto.Password = $"Barangay{barangayDto.BarangayLoc}@123";

            var barangayModel = barangayDto.ToBarangayFromCreateDto();
            var createdBarangay = await _barangayRepository.CreateAsync(barangayModel);

            if (createdBarangay == null)
            {
                return BadRequest("Unable to create Barangay.");
            }

            return CreatedAtAction(nameof(Get), new { id = createdBarangay.BarangayId }, createdBarangay.ToBarangayDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateBarangayRequestDto barangayDto)
        {
            if (barangayDto == null)
            {
                return BadRequest();
            }

            var updatedBarangay = await _barangayRepository.UpdateAsync(id, barangayDto);
            if (updatedBarangay == null)
            {
                return NotFound();
            }

            return Ok(updatedBarangay.ToBarangayDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var deletedBarangay = await _barangayRepository.DeleteAsync(id);
            if (deletedBarangay == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
