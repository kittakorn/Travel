using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlacePage : ContentPage
    {
        public PlacePage()
        {
            InitializeComponent();
            Title = "สถานที่ท่องเที่ยว";
            BindingContext = new PlaceViewModel();
        }
    }
}