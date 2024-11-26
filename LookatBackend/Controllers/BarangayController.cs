using System;
using Microsoft.AspNetCore.Mvc;
using LookatBackend.Models;
using LookatBackend.Dtos.Barangay.CreateBarangayRequestDto;
using LookatBackend.Mappers;
using LookatBackend.Dtos.Barangay.UpdateBarangayRequestDto;

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
        public IActionResult GetAll()
        {
            var barangays = _context.Barangays.ToList()
                .Select(b => b.ToBarangayDto());

            return Ok(barangays);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] string id) 
        {
            var barangay = _context.Barangays.Find(id);

            if (barangay == null)
            {
                return NotFound();
            }
            return Ok(barangay);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateBarangayRequestDto barangayDto)
        {
            var barangayModel = barangayDto.ToBarangayFromCreateDto();
            _context.Barangays.Add(barangayModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = barangayModel.BarangayId }, barangayModel.ToBarangayDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] string id, [FromBody] UpdateBarangayRequestDto barangayDto)
        {
            var barangayModel = _context.Barangays.FirstOrDefault(x => x.BarangayId == id);

            if (barangayModel == null)
            {
                return NotFound();
            }

            barangayModel.BarangayName = barangayDto.BarangayName;
            barangayModel.Purok = barangayDto.Purok;
            barangayModel.BarangayLoc = barangayDto.BarangayLoc;
            barangayModel.CityMunicipality = barangayDto.CityMunicipality;
            barangayModel.Province = barangayDto.Province;

            _context.SaveChanges();

            return Ok(barangayModel.ToBarangayDto());

        }
    }
}
