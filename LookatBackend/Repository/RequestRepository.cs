using LookatBackend.Dtos.CreateRequestRequestDto;
using LookatBackend.Dtos.UpdateRequestRequestDto;
using LookatBackend.Interfaces;
using LookatBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LookatBackend.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly LookatDbContext _context;

        public RequestRepository(LookatDbContext context)
        {
            _context = context;
        }

        public async Task<Request> CreateAsync(Request request)
        {
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<Request?> DeleteAsync(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return null;
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<List<Request>> GetAllAsync()
        {
            return await _context.Requests.ToListAsync();
        }

        public async Task<Request?> GetByIdAsync(int id)
        {
            return await _context.Requests.FindAsync(id);
        }

        public async Task<Request?> UpdateAsync(int id, UpdateRequestRequestDto requestDto)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(x => x.RequestId == id);

            if (request == null)
            {
                return null;
            }

            request.RequestType = requestDto.RequestType;
            request.DocumentId = requestDto.DocumentId;
            request.Quantity = requestDto.Quantity;

            await _context.SaveChangesAsync();
            return request;
        }
    }
}
