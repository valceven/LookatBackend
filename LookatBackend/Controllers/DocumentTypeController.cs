using LookatBackend.Dtos.DocumentType.CreateDocumentTypeRequestDto;
using LookatBackend.Dtos.DocumentType.UpdateDocumentTypeRequestDto;
using LookatBackend.Interfaces;
using LookatBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeService _documentTypeService;

        public DocumentTypeController(IDocumentTypeService documentTypeService)
        {
            _documentTypeService = documentTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var documentDtos = await _documentTypeService.GetAllAsync();
            return Ok(documentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var documentDto = await _documentTypeService.GetByIdAsync(id);

            if (documentDto == null)
            {
                return NotFound();
            }

            return Ok(documentDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDocumentTypeRequestDto documentTypeDto)
        {
            if (documentTypeDto == null)
            {
                return BadRequest("Document type data is required.");
            }

            var createdDocumentDto = await _documentTypeService.CreateAsync(documentTypeDto);
            return CreatedAtAction(nameof(GetById), new { id = createdDocumentDto.DocumentId }, createdDocumentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDocumentTypeRequestDto documentTypeDto)
        {
            var updatedDocumentDto = await _documentTypeService.UpdateAsync(id, documentTypeDto);

            if (updatedDocumentDto == null)
            {
                return NotFound();
            }

            return Ok(updatedDocumentDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var success = await _documentTypeService.DeleteAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
