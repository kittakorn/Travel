using System;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Helpers;
using TravelApp.Models;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    [QueryProperty("PlaceName", "name")]
    public class PlaceDetailViewModel : BaseViewModel
    {
       
        private Place _place;
        private string _placeName;

        public string PlaceName
        {
            get => _placeName;
            set
            {
                if (value == _placeName) return;
                _placeName = Uri.UnescapeDataString(value);
                OnPropertyChanged();
                LoadPlaceCommand.Execute(null);
            }
        }
        public Place Place
        {
            get => _place;
            set
            {
                if (Equals(value, _place)) return;
                _place = value;
                OnPropertyChanged();
            }
        }

        public Command LoadPlaceCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var placelist = await ApiService.GetPlaceAsync();
                    Place = placelist.FirstOrDefault(x => x.PlaceName == Uri.UnescapeDataString(PlaceName));
                    Title = Place?.PlaceName;
                });
            }
        }

    }
}