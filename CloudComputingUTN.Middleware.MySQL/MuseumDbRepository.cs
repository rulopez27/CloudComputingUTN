using CloudComputingUTN.Entities;
using CloudComputingUTN.Middleware;
using Microsoft.EntityFrameworkCore;

namespace CloudComputingUTN.Middleware.MySQL
{
    public class MuseumDbRepository : IMuseumDbRepository
    {
        private MuseumDbContext _museumDbContext;
        public MuseumDbRepository(MuseumDbContext? museumDbContext = null)
        {
            _museumDbContext = museumDbContext ?? new MuseumDbContext();
        }

        public void CreateArtist(Artist artist)
        {
            try
            {
                using (_museumDbContext)
                {
                    _museumDbContext.Artists.Add(artist);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateArtwork(Artwork artwork)
        {
            try
            {
                using (_museumDbContext)
                {
                    _museumDbContext.Artworks.Add(artwork);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Artist GetArtistById(int artistId)
        {
            try
            {
                using (_museumDbContext)
                {
                    return _museumDbContext.Artists
                    .Include(a => a.ArtworkGallery).Single(a => a.ArtistId == artistId);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ICollection<Artist> GetArtists()
        {
            try
            {
                using (_museumDbContext)
                {
                    return _museumDbContext.Artists
                        .Include(a => a.ArtworkGallery).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            };
        }

        public Artwork GetArtworkById(int artworkId)
        {
            try
            {
                using (_museumDbContext)
                {
                    return _museumDbContext.Artworks
                            .Include(a => a.Artist)
                            .Single(a => a.ArtworkId == artworkId);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ICollection<Artwork> GetArtworks()
        {
            try
            {
                using (_museumDbContext)
                {
                    return (ICollection<Artwork>)_museumDbContext.Artworks
                        .Include(a => a.Artist);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}