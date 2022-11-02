using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudComputingUTN.Entities
{
    /// <summary>
    /// Entity that represents an Artwork
    /// </summary>
    public class Artwork
    {
        /// <summary>
        /// Artwork ID
        /// </summary>
        [Key]
        [DisplayName("Artwork ID")]
        public int ArtworkId { get; set; }

        /// <summary>
        /// Artist ID reference
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please link this artwork to a valid artist.")]
        [DisplayName("Artist ID")]
        public int ArtistId { get; set; }

        /// <summary>
        /// Artwork Title
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid name")]
        [DisplayName("Title")]
        public string ArtworkName { get; set; }

        /// <summary>
        /// Artwork Year
        /// </summary>
        [DisplayName("Year")]
        public int? ArtworkYear { get; set; }

        /// <summary>
        /// Artwork brief description
        /// </summary>
        [DisplayName("Description")]
        public string ArtworkDescription { get; set; }

        /// <summary>
        /// Artwork URL
        /// </summary>
        [DisplayName("URL")]
        public string ArtworkURL { get; set; }

        /// <summary>
        /// Artist reference
        /// </summary>
        public virtual Artist Artist { get; set; }

        /// <summary>
        /// Initializes an empty Artwork object
        /// </summary>
        public Artwork()
        {

        }

        /// <summary>
        /// Initializes an empty Artwork object
        /// </summary>
        /// <param name="artistId">Artist ID reference</param>
        /// <param name="artworkName">Artwork title</param>
        /// <param name="artworkYear">Nullable artwork year</param>
        /// <param name="artworkDescription">Artwork description</param>
        /// <param name="artworkURL">Artwork URL</param>
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
