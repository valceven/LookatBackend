using System;
using Microsoft.AspNetCore.Mvc;
using LookatBackend.Models;

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
            var barangays = _context.Barangays.ToList();

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
    }
}
