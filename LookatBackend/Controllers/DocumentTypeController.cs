using Microsoft.AspNetCore.Mvc;
using LookatBackend.Dtos.DocumentType.CreateDocumentTypeRequestDto;
using LookatBackend.Dtos.DocumentType.UpdateDocumentTypeRequestDto;
using LookatBackend.Interfaces;
using LookatBackend.Mappers;

namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;

        public DocumentTypeController(IDocumentTypeRepository documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var documentTypes = await _documentTypeRepository.GetAllAsync();
            var documentDtos = documentTypes.Select(d => d.ToDocumentTypeDto());

            return Ok(documentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var document = await _documentTypeRepository.GetByIdAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return Ok(document.ToDocumentTypeDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDocumentTypeRequestDto documentTypeDto)
        {
            if (documentTypeDto == null)
            {
                return BadRequest("Document type data is required.");
            }

            var documentModel = documentTypeDto.ToDocumentTypeFromCreateDto();
            var createdDocument = await _documentTypeRepository.CreateAsync(documentModel);

            return CreatedAtAction(nameof(Get), new { id = createdDocument.DocumentId }, createdDocument.ToDocumentTypeDto());
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDocumentTypeRequestDto documentTypeDto)
        {
            var updatedDocument = await _documentTypeRepository.UpdateAsync(id, documentTypeDto);

            if (updatedDocument == null)
            {
                return NotFound();
            }

            return Ok(updatedDocument.ToDocumentTypeDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var documentTypeModel = await _documentTypeRepository.DeleteAsync(id);

            if (documentTypeModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
