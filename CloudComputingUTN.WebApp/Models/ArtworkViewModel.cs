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
        }

        public ArtworkViewModel(Artwork artwork)
        {
            Artwork = artwork;
            Title = "";
            ClassName = "";
            Message = "";
        }

        public Artwork Artwork { get; set; }
    }
}
