using Microsoft.AspNetCore.Mvc;
using LookatBackend.Models;
using LookatBackend.Dtos.DocumentType.CreateDocumentTypeRequestDto;
using LookatBackend.Mappers;

namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly LookatDbContext _context;
        public DocumentTypeController(LookatDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() {

            var documents = _context.DocumentTypes.ToList()
                .Select(d => d.ToDocumentTypeDto());

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

        [HttpPost]
        public IActionResult Create([FromBody] CreateDocumentTypeRequestDto documentTypeDto)
        {
            var documentModel = documentTypeDto.ToDocumentTypeFromCreateDto();
            _context.DocumentTypes.Add(documentModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = documentModel.DocumentId}, documentModel.ToDocumentTypeDto());

        }
    }
}
