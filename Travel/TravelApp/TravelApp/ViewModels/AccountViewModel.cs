using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TravelApp.Annotations;
using TravelApp.Helpers;
using TravelApp.Models;
using TravelApp.Services;
using TravelApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Forms.Application;

namespace TravelApp.ViewModels
{
    class AccountViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService = new ApiService();
        public AccountViewModel()
        {
            if (string.IsNullOrEmpty(Setting.AccessToken))
            {
                Login = true;
                Logout = false;
            }
            else
            {
                Login = false;
                Logout = true;
                GetUserCommand.Execute(null);
            }
        }


        private bool _login;
        private bool _logout;
        private AspNetUser _user;

        public bool Logout
        {
            get => _logout;
            set
            {
                if (value == _logout) return;
                _logout = value;
                OnPropertyChanged();
            }
        }
        public bool Login
        {
            get => _login;
            set
            {
                if (value == _login) return;
                _login = value;
                OnPropertyChanged();
            }
        }
        public AspNetUser User
        {
            get => _user;
            set
            {
                if (Equals(value, _user)) return;
                _user = value;
                OnPropertyChanged();
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return new Command(() =>
                {
                    Preferences.Clear();
                    Login = true;
                    Logout = false;
                    var mainPage = new MainPage() as TabbedPage;
                    mainPage.CurrentPage = mainPage.Children[2];
                    Current.MainPage = new NavigationPage(mainPage);
                });
            }
        }

        public ICommand LoginViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    Current.MainPage.Navigation.PushAsync(new LoginPage());
                });
            }
        }

        public ICommand ChangePasswordViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    Current.MainPage.Navigation.PushAsync(new ChangePasswordPage());
                });
            }
        }

        public ICommand EditProfileViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    Current.MainPage.Navigation.PushAsync(new EditProfilePage(User));
                });
            }
        }

        public ICommand GetUserCommand
        {
            get
            {
                return new Command(async () =>
                {
                    User = new AspNetUser();
                    User = await _apiService.GetUserAsync();
                });
            }
        }

        public ICommand RegisterViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    Current.MainPage.Navigation.PushAsync(new RegisterPage());
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
