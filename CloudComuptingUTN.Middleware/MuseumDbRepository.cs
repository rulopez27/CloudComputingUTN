﻿using CloudComputingUTN.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudComputingUTN.Middleware
{
    public class MuseumDbRepository : IMuseumDbRepository, IDisposable
    {
        internal MuseumDbContext dbContext;
        public MuseumDbRepository(MuseumDbContext dbContext)
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
                dbContext.Entry(artist).State = EntityState.Detached;
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
                dbContext.Entry(artwork).State = EntityState.Detached;
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

        public async Task<bool> DeleteArtist(int artistId)
        {
            try
            {
                var artist = await dbContext.Artists.SingleAsync(artist => artist.ArtistId == artistId);
                dbContext.Artists.Remove(artist);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteArtwork(int artworkId)
        {
            try
            {
                var artwork = await dbContext.Artworks.SingleAsync(artwork => artwork.ArtworkId == artworkId);
                dbContext.Artworks.Remove(artwork);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
