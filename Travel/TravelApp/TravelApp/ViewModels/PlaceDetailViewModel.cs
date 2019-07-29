using System;
using System.Linq;
using TravelApp.Models;
using TravelApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Distance = Xamarin.Forms.Maps.Distance;

namespace TravelApp.ViewModels
{
    [QueryProperty("PlaceName", "name")]
    public class PlaceDetailViewModel : BaseViewModel
    {
        private Map _map;

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

        public Map Map
        {
            get => _map;
            set
            {
                if (Equals(value, _map)) return;
                _map = value;
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
                    Place.PlaceDescription = "      " + Place.PlaceDescription;
                    await ApiService.GetPutAsync(Place.PlaceId,Place);
                    SetCustomMap();
                });
            }
        }

        public void SetCustomMap()
        {
            Map = new Map(MapSpan.FromCenterAndRadius(
                new Position(
                    Convert.ToDouble(Place.PlaceLatitude),
                    Convert.ToDouble(Place.PlaceLongitude)),
                Distance.FromMiles(1)))
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Pins =
                {
                    new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(Convert.ToDouble(Place.PlaceLatitude),
                            Convert.ToDouble(Place.PlaceLongitude)),
                        Label = Place.PlaceName,
                        Address = Place.PlaceAddress
                    }
                }
            };
        }
    }
}