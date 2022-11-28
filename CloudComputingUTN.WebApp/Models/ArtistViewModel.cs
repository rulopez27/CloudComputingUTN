using CloudComputingUTN.Entities;

namespace CloudComputingUTN.WebApp.Models
{
    public class ArtistViewModel : BaseViewModel
    {
        public Artist Artist { get; set; }
        public ArtistViewModel()
        {
            Title = "";
            Message = "";
            ClassName = "";
            RecordFound = true;
            Artist = new Artist();
        }

        public ArtistViewModel(Artist artist)
        {
            Title = "";
            Message = "";
            ClassName = "";
            Artist = artist;
            RecordFound = true;
        }
    }
}
