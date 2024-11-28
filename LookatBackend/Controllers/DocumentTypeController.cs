using Microsoft.AspNetCore.Mvc;
using LookatBackend.Models;
using LookatBackend.Dtos.DocumentType.CreateDocumentTypeRequestDto;
using LookatBackend.Mappers;
using LookatBackend.Dtos.DocumentType.UpdateDocumentTypeRequestDto;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAll()
        {
            var documentTypes = await _context.DocumentTypes.ToListAsync();
            var documentDtos = documentTypes.Select(d => d.ToDocumentTypeDto());

            return Ok(documentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var document = await _context.DocumentTypes.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return Ok(document.ToDocumentTypeDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDocumentTypeRequestDto documentTypeDto)
        {
            var documentModel = documentTypeDto.ToDocumentTypeFromCreateDto();
            await _context.DocumentTypes.AddAsync(documentModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = documentModel.DocumentId }, documentModel.ToDocumentTypeDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDocumentTypeRequestDto documentTypeDto)
        {
            var documentTypeModel = await _context.DocumentTypes.FirstOrDefaultAsync(x => x.DocumentId == id);

            if (documentTypeModel == null)
            {
                return NotFound();
            }

            documentTypeModel.DocumentName = documentTypeDto.DocumentName;
            documentTypeModel.Price = documentTypeDto.Price;

            await _context.SaveChangesAsync();

            return Ok(documentTypeModel.ToDocumentTypeDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var documentTypeModel = await _context.DocumentTypes.FirstOrDefaultAsync(x => x.DocumentId == id);

            if (documentTypeModel == null)
            {
                return NotFound();
            }

            _context.DocumentTypes.Remove(documentTypeModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
