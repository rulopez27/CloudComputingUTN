using CloudComputingUTN.Entities;

namespace CloudComputingUTN.WebApp.Models
{
    public class ArtworkViewModel : BaseViewModel
    {
        public ArtworkViewModel()
        {
            Title = "";
            ClassName = "";
            Message = "";
            Artwork = new Artwork();
            RecordFound = true;
        }

        public ArtworkViewModel(Artwork artwork)
        {
            Artwork = artwork;
            Title = "";
            ClassName = "";
            Message = "";
            RecordFound = true;
        }

        public Artwork Artwork { get; set; }
    }
}
