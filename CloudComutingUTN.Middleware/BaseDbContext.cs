using CloudComputingUTN.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudComputingUTN.Middleware
{
    public abstract class BaseDbContext : DbContext
    {
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Artwork> Artworks { get; set; }
    }
}
