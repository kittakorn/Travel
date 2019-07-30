using System;
using System.Collections.Generic;
using TravelApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TravelApp.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private int _distant;

        private Map _map;
        private Place _place;
        private Position _position;
        private string _type;
        private List<Result> _results;
        private Result _selectMap;

        public MapViewModel()
        {
        }

        public MapViewModel(Place place)
        {
            Place = place;
            Position = new Position(Convert.ToDouble(place.PlaceLatitude), Convert.ToDouble(place.PlaceLongitude));
            SetCustomMap();
        }

        public List<Result> Results
        {
            get => _results;
            set
            {
                if (Equals(value, _results)) return;
                _results = value;
                OnPropertyChanged();
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

        public Position Position
        {
            get => _position;
            set
            {
                if (value.Equals(_position)) return;
                _position = value;
                OnPropertyChanged();
            }
        }

        public Command SearchCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var placeSearch = await ApiService.GetMapAsync(Position, Distant, Type);
                    Results = placeSearch.Results;
                    Map.Pins.Clear();
                });
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

        public string Type
        {
            get => _type;
            set
            {
                if (value == _type) return;
                _type = value;
                OnPropertyChanged();
            }
        }

        public int Distant
        {
            get => _distant;
            set
            {
                if (value == _distant) return;
                _distant = value;
                OnPropertyChanged();
            }
        }

        public Result SelectMap
        {
            get => _selectMap;
            set
            {
                if (Equals(value, _selectMap)) return;
                _selectMap = value;
                OnPropertyChanged();
                Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(SelectMap.Geometry.Location.Lat, SelectMap.Geometry.Location.Lng), Distance.FromMiles(1)));
                Map.Pins.Clear();
                Map.Pins.Add(
                    new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(SelectMap.Geometry.Location.Lat, SelectMap.Geometry.Location.Lng),
                        Label = SelectMap.Name,
                        Address = SelectMap.Vicinity
                    });
            }
        }


        public void SetCustomMap()
        {
            Map = new Map(MapSpan.FromCenterAndRadius(Position, Distance.FromMiles(1)))
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Pins =
                {
                    new Pin
                    {
                        Type = PinType.Place,
                        Position = Position,
                        Label = Place.PlaceName,
                        Address = Place.PlaceAddress
                    }
                }
            };
        }
    }
}