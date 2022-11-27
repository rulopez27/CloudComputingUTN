using CloudComputingUTN.Entities;

namespace CloudComputingUTN.WebApp.Models
{
    public class ArtistsListViewModel :BaseViewModel
    {
        public ArtistsListViewModel()
        {
            Title = "";
            Message = "";
            ClassName = "";
            Artists = new List<Artist>();
        }

        public List<Artist> Artists { get; set; }
    }
}
