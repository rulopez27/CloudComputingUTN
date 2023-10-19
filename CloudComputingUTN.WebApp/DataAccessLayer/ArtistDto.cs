using CloudComputingUTN.Entities;

namespace CloudComputingUTN.WebApp.DataAccessLayer
{
    public class ArtistDto : DtoBase
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistWikiPage { get; set; }
        public List<ArtworkDto> Artworks { get; set; }
    }
}
