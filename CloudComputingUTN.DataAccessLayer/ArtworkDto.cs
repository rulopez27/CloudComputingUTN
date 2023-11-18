namespace CloudComputingUTN.DataAccessLayer
{
    public class ArtworkDto : DtoBase
    {
        public int ArtworkId { get; set; }
        public int ArtistId { get; set; }
        public string ArtworkName { get; set; }
        public int ArtworkYear { get; set; }
        public string ArtworkDescription { get; set; }
        public string ArtworkURL { get; set; }

        public ArtworkDto()
        {
            ArtworkName = string.Empty;
            ArtworkYear = 0;
            ArtworkDescription = string.Empty;
            ArtworkURL = string.Empty;
        }
    }
}
