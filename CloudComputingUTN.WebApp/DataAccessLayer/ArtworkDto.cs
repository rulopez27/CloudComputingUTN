namespace CloudComputingUTN.WebApp.DataAccessLayer
{
    public class ArtworkDto : DtoBase
    {
        public int ArtworkId { get; set; }
        public int ArtistId { get; set; }
        public string ArtworkName { get; set; }
        public int ArtworkYear { get; set; }
        public string ArtworkDescription { get; set; }
        public string ArtworkURL { get; set; }
    }
}
