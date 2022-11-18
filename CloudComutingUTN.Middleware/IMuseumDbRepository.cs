using CloudComputingUTN.Entities;

namespace CloudComputingUTN.Middleware
{
    public interface IMuseumDbRepository
    {  
        Task<Artist> GetArtistById(int artistId);
        Task<ICollection<Artist>> GetArtists();
        Task<Artwork> GetArtworkById(int artworkId);
        Task<ICollection<Artwork>> GetArtworks();
        Task<Artist> CreateArtist(Artist artist);
        Task<Artwork> CreateArtwork(Artwork artwork);
        Task<Artist> UpdateArtist(Artist artist);
        Task<Artwork> UpdateArtwork(Artwork artwork);
    }
}