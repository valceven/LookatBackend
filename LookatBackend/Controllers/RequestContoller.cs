using Microsoft.AspNetCore.Mvc;
using LookatBackend.Models;

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
        public IActionResult GetAll()
        {

            var documents = _context.DocumentTypes.ToList();

            return Ok(documents);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var document = _context.DocumentTypes.Find(id);

            if (document == null)
            {
                return NotFound();
            }

            return Ok(document);
        }
    }
}
