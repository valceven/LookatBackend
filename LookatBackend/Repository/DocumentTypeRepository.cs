using LookatBackend.Dtos.DocumentType.CreateDocumentTypeRequestDto;
using LookatBackend.Dtos.DocumentType.UpdateDocumentTypeRequestDto;
using LookatBackend.Interfaces;
using LookatBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LookatBackend.Repository
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly LookatDbContext _context;

        public DocumentTypeRepository(LookatDbContext context)
        {
            _context = context;
        }

        public async Task<DocumentType> CreateAsync(DocumentType documentType)
        {
            await _context.DocumentTypes.AddAsync(documentType);
            await _context.SaveChangesAsync();
            return documentType;
        }

        public Task<DocumentType> CreateAsync(CreateDocumentTypeRequestDto documentTypeDto)
        {
            throw new NotImplementedException();
        }

        public async Task<DocumentType?> DeleteAsync(int id)
        {
            var documentType = await _context.DocumentTypes.FindAsync(id);
            if (documentType == null)
            {
                return null;
            }

            _context.DocumentTypes.Remove(documentType);
            await _context.SaveChangesAsync();
            return documentType;
        }

        public async Task<List<DocumentType>> GetAllAsync()
        {
            return await _context.DocumentTypes.ToListAsync();
        }

        public async Task<DocumentType?> GetByIdAsync(int id)
        {
            return await _context.DocumentTypes.FindAsync(id);
        }

        public async Task<DocumentType?> UpdateAsync(int id, UpdateDocumentTypeRequestDto documentTypeDto)
        {
            var documentType = await _context.DocumentTypes.FirstOrDefaultAsync(x => x.DocumentId == id);

            if (documentType == null)
            {
                return null;
            }

            documentType.DocumentName = documentTypeDto.DocumentName;
            documentType.Price = documentTypeDto.Price;

            await _context.SaveChangesAsync();
            return documentType;
        }
    }
}
