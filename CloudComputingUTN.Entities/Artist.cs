using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudComputingUTN.Entities
{
    /// <summary>
    /// Entity that represents an Artist
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// Artist ID
        /// </summary>
        [Key]
        [DisplayName("Artist ID")]
        public int ArtistId { get; set; }

        /// <summary>
        /// Artist Name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid name")]
        [DisplayName("Artist Name")]
        public string ArtistName { get; set; }

        /// <summary>
        /// Artist Wiki Page
        /// </summary>
        [DisplayName("Artist Wiki")]
        public string ArtistWikiPage { get; set; }
        
        /// <summary>
        /// Artist's artwork collection
        /// </summary>
        public virtual ICollection<Artwork> ArtworkGallery { get; set; }

        /// <summary>
        /// Initializes an empty Artist
        /// </summary>
        public Artist()
        {

        }

        /// <summary>
        /// Initializes an Artist with defined properties
        /// </summary>
        /// <param name="artistName"></param>
        /// <param name="artistWikiPage"></param>
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
