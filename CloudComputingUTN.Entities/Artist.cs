using System.Collections.Generic;

namespace CloudComputingUTN.Entities
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistWikiPage { get; set; }
        public virtual ICollection<Artwork> ArtworkGallery { get; set; }

        public Artist()
        {

        }

        public Artist(string artistName, string artistWikiPage)
        {
            ArtistName = artistName;
            ArtistWikiPage = artistWikiPage;
        }

        public override string ToString()
        {
            return ArtistName;
        }
    }
}
