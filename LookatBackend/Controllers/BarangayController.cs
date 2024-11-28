using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LookatBackend.Models;
using LookatBackend.Dtos.Barangay.CreateBarangayRequestDto;
using LookatBackend.Mappers;
using LookatBackend.Dtos.Barangay.UpdateBarangayRequestDto;
using Microsoft.EntityFrameworkCore;

namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BarangayController : ControllerBase
    {
        private readonly LookatDbContext _context;
        public BarangayController(LookatDbContext context) 
        { 
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var barangays = await _context.Barangays
                .ToListAsync(); // Fetch all barangays asynchronously
            var barangayDtos = barangays.Select(b => b.ToBarangayDto());

            return Ok(barangayDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var barangay = await _context.Barangays.FindAsync(id);

            if (barangay == null)
            {
                return NotFound();
            }
            return Ok(barangay.ToBarangayDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBarangayRequestDto barangayDto)
        {
            var barangayModel = barangayDto.ToBarangayFromCreateDto();
            await _context.Barangays.AddAsync(barangayModel); // Add asynchronously
            await _context.SaveChangesAsync(); // Save changes asynchronously

            return CreatedAtAction(nameof(Get), new { id = barangayModel.BarangayId }, barangayModel.ToBarangayDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateBarangayRequestDto barangayDto)
        {
            var barangayModel = await _context.Barangays
                .FirstOrDefaultAsync(x => x.BarangayId == id);

            if (barangayModel == null)
            {
                return NotFound();
            }

            barangayModel.BarangayName = barangayDto.BarangayName;
            barangayModel.Purok = barangayDto.Purok;
            barangayModel.BarangayLoc = barangayDto.BarangayLoc;
            barangayModel.CityMunicipality = barangayDto.CityMunicipality;
            barangayModel.Province = barangayDto.Province;

            await _context.SaveChangesAsync();

            return Ok(barangayModel.ToBarangayDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var barangayModel = await _context.Barangays
                .FirstOrDefaultAsync(x => x.BarangayId == id);

            if (barangayModel == null)
            {
                return NotFound();
            }

            _context.Barangays.Remove(barangayModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
