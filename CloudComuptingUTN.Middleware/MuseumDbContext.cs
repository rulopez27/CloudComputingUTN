using CloudComputingUTN.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudComputingUTN.Middleware
{
    public class MuseumDbContext : DbContext
    {
        public MuseumDbContext(DbContextOptions<MuseumDbContext> options) :base(options) { }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Artwork> Artworks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>()
                .Property(x => x.ArtistId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Artist>()
                .HasMany(x => x.ArtworkGallery);

            modelBuilder.Entity<Artwork>()
                .Property(x => x.ArtworkId)
                .ValueGeneratedOnAdd();
        }
    }
}
