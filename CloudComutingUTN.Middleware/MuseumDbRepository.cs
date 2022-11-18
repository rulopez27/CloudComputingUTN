using CloudComputingUTN.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudComputingUTN.Middleware
{
    public class MuseumDbRepository : IMuseumDbRepository, IDisposable
    {
        internal BaseDbContext dbContext;
        public MuseumDbRepository(BaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Artist> CreateArtist(Artist artist)
        {
            try
            {
                dbContext.Artists.Add(artist);
                await dbContext.SaveChangesAsync();
                return artist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Artwork> CreateArtwork(Artwork artwork)
        {
            try
            {
                dbContext.Artworks.Add(artwork);
                await dbContext.SaveChangesAsync();
                return artwork;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
        }

        public async Task<Artist> GetArtistById(int artistId)
        {
            try
            {
                Artist artist = await dbContext.Artists
                                    .Include(a => a.ArtworkGallery)
                                    .SingleAsync(a => a.ArtistId == artistId);
                return artist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Artist>> GetArtists()
        {
            try
            {
                var artists = await dbContext.Artists
                                .Include(a => a.ArtworkGallery)
                                .ToListAsync();
                return artists;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Artwork> GetArtworkById(int artworkId)
        {
            try
            {
                Artwork artwork = await dbContext.Artworks
                                    .Include(a => a.Artist)
                                    .SingleAsync(a => a.ArtworkId == artworkId);
                return artwork;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Artwork>> GetArtworks()
        {
            try
            {
                var artworks = await dbContext.Artworks
                                    .Include(a => a.Artist)
                                    .ToListAsync();
                return artworks;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Artist> UpdateArtist(Artist artist)
        {
            try
            {
                dbContext.Artists.Attach(artist);
                dbContext.Entry(artist).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return artist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Artwork> UpdateArtwork(Artwork artwork)
        {
            try
            {
                dbContext.Artworks.Attach(artwork);
                dbContext.Entry(artwork).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return artwork;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
