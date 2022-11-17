using CloudComputingUTN.Entities;
using CloudComputingUTN.Middleware;

namespace CloudComputingUTN.Middleware.MySQL
{
    public class MuseumDbMySQLRepository : IMuseumDbRepository
    {
        public void CreateArtist(Artist artist)
        {
            throw new NotImplementedException();
        }

        public void CreateArtwork(Artwork artwork)
        {
            throw new NotImplementedException();
        }

        public Artist GetArtistById(int artistId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Artist> GetArtists()
        {
            throw new NotImplementedException();
        }

        public Artwork GetArtworkById()
        {
            throw new NotImplementedException();
        }

        public ICollection<Artwork> GetArtworks()
        {
            throw new NotImplementedException();
        }
    }
}