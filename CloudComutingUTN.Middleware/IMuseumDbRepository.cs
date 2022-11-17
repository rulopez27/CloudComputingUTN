using CloudComputingUTN.Entities;

namespace CloudComputingUTN.Middleware
{
    public interface IMuseumDbRepository
    {
        void CreateArtist(Artist artist);
        void CreateArtwork(Artwork artwork);
        Artist GetArtistById(int artistId);
        ICollection<Artist> GetArtists();
        Artwork GetArtworkById();
        ICollection<Artwork> GetArtworks();
    }
}