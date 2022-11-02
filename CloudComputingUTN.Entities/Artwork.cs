using System;
using System.Collections.Generic;
using System.Text;

namespace CloudComputingUTN.Entities
{
    public class Artwork
    {
        public int ArtworkId { get; set; }
        public int ArtistId { get; set; }
        public string ArtworkName { get; set; }
        public int? ArtworkYear { get; set; }
        public string ArtworkDescription { get; set; }
        public string ArtworkURL { get; set; }

        public Artwork()
        {

        }

        public Artwork(int artistId, 
            string artworkName, 
            int? artworkYear, 
            string artworkDescription, 
            string artworkURL)
        {
            ArtistId = artistId;
            ArtworkName = artworkName;
            ArtworkYear = artworkYear;
            ArtworkDescription = artworkDescription;
            ArtworkURL = artworkURL;
        }

        public override string ToString()
        {
            return ArtworkName;
        }
    }
}
