using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TravelApp.Annotations;
using TravelApp.Helpers;
using TravelApp.Services;
using TravelApp.Views;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        ApiService _apiService = new ApiService();

        private string _email;
        private string _password;

        public string Email
        {
            get => _email;
            set
            {
                if (value == _email) return;
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (value == _password) return;
                _password = value;
                OnPropertyChanged();
            }
        }


        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                        await Application.Current.MainPage.DisplayAlert("แจ้งเตือน", "กรุณาณกรอกข้อมูล", "ปิด");
                    Setting.AccessToken = await _apiService.LoginAsync(Email, Password);
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
