using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TravelApp.Annotations;
using TravelApp.Models;
using TravelApp.Services;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        public readonly ApiService _apiService = new ApiService();
        private Register _register;

        public RegisterViewModel()
        {
            Register = new Register();
        }

        public Register Register
        {
            get => _register;
            set
            {
                if (Equals(value, _register)) return;
                _register = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (await _apiService.RegisterAsync(Register))
                    {
                       Application.Current.MainPage?.DisplayAlert("แจ้งเตือน", "true", "ปิด");
                    }
                    else
                    {
                        Application.Current.MainPage?.DisplayAlert("แจ้งเตือน", "false", "ปิด");
                    }
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
