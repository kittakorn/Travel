using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TravelApp.Annotations;
using TravelApp.Helpers;
using TravelApp.Models;
using TravelApp.Services;
using TravelApp.Views;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    class PlaceDetailViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService = new ApiService();

        public PlaceDetailViewModel() { }
        public PlaceDetailViewModel(int id)
        {
            PlaceId = id;
            GetPlaceCommand.Execute(null);

        }

        private Place _place;
        private int _placeId;


        public int PlaceId
        {
            get => _placeId;
            set
            {
                if (value == _placeId) return;
                _placeId = value;
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

        public ICommand GetPlaceCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Place = await _apiService.GetPlaceAsync(PlaceId);
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
