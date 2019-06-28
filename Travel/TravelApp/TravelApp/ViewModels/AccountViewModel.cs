using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TravelApp.Annotations;
using TravelApp.Views;
using Xamarin.Forms;
using static Xamarin.Forms.Application;

namespace TravelApp.ViewModels
{
    class AccountViewModel : INotifyPropertyChanged
    {
        public ICommand LoginViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    Current.MainPage = new NavigationPage(new MainPage());
                    Current.MainPage.Navigation.PushAsync(new LoginPage());
                });
            }
        }

        public ICommand RegisterViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    Current.MainPage = new NavigationPage(new MainPage());
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
