using Microsoft.EntityFrameworkCore;

namespace LookatBackend.Models
{
    public class LookatDbContext:DbContext

    {
        public LookatDbContext(DbContextOptions<LookatDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Barangay> Barangays { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<OtpRecords> OtpRecords { get; set; }
    }
}
