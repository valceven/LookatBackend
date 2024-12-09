using LookatBackend.Dtos.Barangay.UpdateBarangayRequestDto;
using LookatBackend.Interfaces;
using LookatBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LookatBackend.Repository
{
    public class BarangayRepository : IBarangayRepository
    {
        private readonly LookatDbContext _context;

        public BarangayRepository(LookatDbContext context)
        {
            _context = context;
        }

        public async Task<Barangay> CreateAsync(Barangay barangayModel)
{
            var exists = await _context.Barangays
                .AnyAsync(b =>
                    b.BarangayLoc == barangayModel.BarangayLoc &&
                    b.CityMunicipality == barangayModel.CityMunicipality &&
                    b.Province == barangayModel.Province);

            if (exists)
            {
                throw new InvalidOperationException("Barangay with the same combination already exists.");
            }

            await _context.Barangays.AddAsync(barangayModel);
            await _context.SaveChangesAsync();

            return barangayModel;
        }

        public async Task<Barangay?> DeleteAsync(string id)
        {
            var barangay = await _context.Barangays.FindAsync(id);
            if (barangay == null) return null;

            _context.Barangays.Remove(barangay);
            await _context.SaveChangesAsync();

            return barangay;
        }

        public async Task<List<Barangay>> GetAllAsync()
        {
            return await _context.Barangays.ToListAsync();
        }

        public async Task<Barangay?> GetByIdAsync(string id)
        {
            return await _context.Barangays.FindAsync(id);
        }

        public async Task<Barangay?> UpdateAsync(string id, UpdateBarangayRequestDto barangayDto)
        {
            var barangay = await _context.Barangays.FindAsync(id);
            if (barangay == null) return null;

            barangay.BarangayName = barangayDto.BarangayName;
            barangay.Purok = barangayDto.Purok;
            barangay.BarangayLoc = barangayDto.BarangayLoc;
            barangay.CityMunicipality = barangayDto.CityMunicipality;
            barangay.Province = barangayDto.Province;

            await _context.SaveChangesAsync();
            return barangay;
        }
    }
}
