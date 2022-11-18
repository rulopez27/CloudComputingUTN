using CloudComputingUTN.Entities;
using System.Collections;

namespace CloudComputingUTN.Middleware
{
    public interface IMuseumDbRepository
    {
        void CreateArtist(Artist artist);
        void CreateArtwork(Artwork artwork);
        Task<Artist> GetArtistById(int artistId);
        Task<ICollection<Artist>> GetArtists();
        Task<Artwork> GetArtworkById(int artworkId);
        Task<ICollection<Artwork>> GetArtworks();
    }
}