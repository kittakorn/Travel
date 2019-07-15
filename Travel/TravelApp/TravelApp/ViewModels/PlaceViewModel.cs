using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelApp.Annotations;
using TravelApp.Models;
using TravelApp.Services;
using TravelApp.Views;
using Xamarin.Forms;
using static Xamarin.Forms.Application;

namespace TravelApp.ViewModels
{
    class PlaceViewModel : INotifyPropertyChanged
    {
        public PlaceViewModel()
        {
            GetPlaceCommand.Execute(null);    
        }

        private readonly ApiService _apiService = new ApiService();
        private List<Place> _places;
        private Place _place;

        public Place Place
        {
            get => _place;
            set
            {
                if (Equals(value, _place)) return;
                _place = value;
                OnPropertyChanged();
                Current.MainPage.Navigation.PushAsync(new PlaceTabedPage(Place.PlaceId));
            }
        }

        public async Task SelectPlace(Place place)
        {
            await Application.Current.MainPage.DisplayAlert(",", place.PlaceName, "2");
        }

        public List<Place> Places
        {
            get => _places;
            set
            {
                if (Equals(value, _places)) return;
                _places = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetPlaceCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Places = await _apiService.GetPlaceAsync();
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
