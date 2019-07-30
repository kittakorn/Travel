using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models;
using TravelApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TravelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private MapViewModel viewModel;
        public MapPage(Place place)
        {
            InitializeComponent();
            BindingContext = viewModel = new MapViewModel(place);
            TypePicker.Title = "ประเภท";
            TypePicker.ItemsSource = new List<string> { "ที่พัก", "ร้านอาหาร" };
            TypePicker.SelectedIndex = 0;
            DistantPicker.Title = "ระยะทาง(กิโลเมตร)";
            DistantPicker.ItemsSource = new List<int> { 1, 2, 3, 4, 5 };
            DistantPicker.SelectedIndex = 0;
        }

        public MapPage()
        {
            InitializeComponent();

        }
    }
}