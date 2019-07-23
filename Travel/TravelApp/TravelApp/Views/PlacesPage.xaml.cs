using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models;
using TravelApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlacesPage : ContentPage
    {
        private PlaceViewModel viewModel;

        public PlacesPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new PlaceViewModel();
        }

        async void OnPlaceSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var place = (Place) e.SelectedItem;
            if (place == null)
                return;
            await Shell.Current.GoToAsync($"placedetail?name={place.PlaceName}");

            PlacesListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Places.Count == 0)
                viewModel.LoadPlaceCommand.Execute(null);
        }
    }
}