using LookatBackend.Dtos.Barangay.CreateBarangayRequestDto;
using LookatBackend.Dtos.Barangay.UpdateBarangayRequestDto;
using LookatBackend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BarangayController : ControllerBase
    {
        private readonly IBarangayService _barangayService;

        public BarangayController(IBarangayService barangayService)
        {
            _barangayService = barangayService;
        }

        // Get all Barangays
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var barangays = await _barangayService.GetAllBarangaysAsync();
            if (barangays == null || barangays.Count == 0)
            {
                return NotFound();
            }

            return Ok(barangays);  // Controller only generates response here
        }

        // Get Barangay by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var barangay = await _barangayService.GetBarangayByIdAsync(id);
            if (barangay == null)
            {
                return NotFound();
            }

            return Ok(barangay);
        }

        // Create Barangay
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBarangayRequestDto barangayDto)
        {
            if (barangayDto == null)
            {
                return BadRequest("Barangay data is required.");
            }

            var createdBarangay = await _barangayService.CreateBarangayAsync(barangayDto);
            if (createdBarangay == null)
            {
                return BadRequest("Unable to create Barangay.");
            }

            return CreatedAtAction(nameof(Get), new { id = createdBarangay.BarangayId }, createdBarangay);
        }

        // Update Barangay
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateBarangayRequestDto barangayDto)
        {
            if (barangayDto == null)
            {
                return BadRequest();
            }

            var updatedBarangay = await _barangayService.UpdateBarangayAsync(id, barangayDto);
            if (updatedBarangay == null)
            {
                return NotFound();
            }

            return Ok(updatedBarangay);
        }

        // Delete Barangay
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var deletedBarangay = await _barangayService.DeleteBarangayAsync(id);
            if (deletedBarangay == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
