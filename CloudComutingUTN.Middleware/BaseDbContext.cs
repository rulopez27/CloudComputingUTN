using CloudComputingUTN.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudComputingUTN.Middleware
{
    public abstract class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options) :base(options) { }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Artwork> Artworks { get; set; }
    }
}
