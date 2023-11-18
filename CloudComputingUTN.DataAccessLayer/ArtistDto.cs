using CloudComputingUTN.Entities;

namespace CloudComputingUTN.DataAccessLayer
{
    public class ArtistDto : DtoBase
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistWikiPage { get; set; }
        public List<ArtworkDto> Artworks { get; set; }

        public ArtistDto()
        {
            ArtistName = string.Empty;
            ArtistWikiPage = string.Empty;
            Artworks = new List<ArtworkDto>();
        }
    }
}
