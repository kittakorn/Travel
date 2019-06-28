using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TravelApp.Annotations;
using TravelApp.Models;

namespace TravelApp.ViewModels
{
    class PlaceDetailViewModel : INotifyPropertyChanged
    {
        private Place _place;

        public PlaceDetailViewModel()
        {

        }
        public PlaceDetailViewModel(Place place)
        {
            Place = place;
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
