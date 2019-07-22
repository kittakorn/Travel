using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using TravelApp.Helpers;
using TravelApp.Models;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        private bool _profileVisible;
        private bool _accountVisible;
        private User _user;

        public AccountViewModel()
        {
            CheckLogin();
            MessagingCenter.Subscribe<LoginViewModel>(this, "CheckLogin",
                (sender) => CheckLogin());
            MessagingCenter.Subscribe<RegisterViewModel>(this, "CheckLogin",
                (sender) => CheckLogin());
            MessagingCenter.Subscribe<EditAccountViewModel>(this, "UpdateAccount",
                (sender) => LoadUserCommand.Execute(null));
        }

        public User User
        {
            get => _user;
            set
            {
                if (Equals(value, _user)) return;
                _user = value;
                OnPropertyChanged();
            }
        }

        public Command LoadUserCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        User = await ApiService.GetUserAsync();
                    }
                    catch (Exception e)
                    {
                        Toast.Error(e.Message);
                    }
                });
            }
        }

        public bool ProfileVisible
        {
            get => _profileVisible;
            set
            {
                if (value == _profileVisible) return;
                _profileVisible = value;
                OnPropertyChanged();
            }
        }

        public bool AccountVisible
        {
            get => _accountVisible;
            set
            {
                if (value == _accountVisible) return;
                _accountVisible = value;
                OnPropertyChanged();
            }
        }

        public void CheckLogin()
        {
            if (string.IsNullOrEmpty(Setting.AccessToken))
            {
                ProfileVisible = false;
                AccountVisible = true;
            }
            else
            {
                ProfileVisible = true;
                AccountVisible = false;
                LoadUserCommand.Execute(null);
            }
        }
    }
}
