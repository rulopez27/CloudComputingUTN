using CloudComputingUTN.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudComputingUTN.Middleware.UnitTests
{
    internal class SQLiteDbContext : BaseDbContext
    {
        public SQLiteDbContext(DbContextOptions options) : base(options)
        {
        }

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
