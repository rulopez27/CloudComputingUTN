using CloudComputingUTN.Entities;
using Microsoft.Identity.Client;

namespace CloudComputingUTN.WebApp.Models
{
    public class ArtworksListViewModel : BaseViewModel
    {
        public List<Artwork> Artworks { get; set; }
        public ArtworksListViewModel()
        {
            Message = "";
            Title = "";
            ClassName = "";
            Artworks = new List<Artwork>();
        }
    }
}
