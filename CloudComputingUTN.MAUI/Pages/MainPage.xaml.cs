using CloudComputingUTN.MAUI.Models;
using CloudComputingUTN.MAUI.PageModels;

namespace CloudComputingUTN.MAUI.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}