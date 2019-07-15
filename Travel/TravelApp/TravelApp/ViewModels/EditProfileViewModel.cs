using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TravelApp.Annotations;
using TravelApp.Helpers;
using TravelApp.Models;
using TravelApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    class EditProfileViewModel : INotifyPropertyChanged
    {
        ApiService _apiService = new ApiService();
        private EditProfile _profile;
        public EditProfileViewModel() { }

        public EditProfileViewModel(AspNetUser user)
        {
            Profile = new EditProfile { Name = user.Name, PhoneNumber = user.PhoneNumber, Email = user.Email };
        }

        public EditProfile Profile
        {
            get => _profile;
            set
            {
                if (Equals(value, _profile)) return;
                _profile = value;
                OnPropertyChanged();
            }
        }


        public ICommand UpdateUserCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(Profile.Name) ||
                            string.IsNullOrWhiteSpace(Profile.Password) ||
                            string.IsNullOrWhiteSpace(Profile.PhoneNumber))
                            Toast.Show();
                        else if (await _apiService.PutUserAsync(Profile))
                        {
                            Toast.Success("อัพเดตข้อมูลแล้ว");
                            var mainPage = new MainPage() as TabbedPage;
                            mainPage.CurrentPage = mainPage.Children[2];
                            Application.Current.MainPage = new NavigationPage(mainPage);
                        }
                        else
                            Toast.Warning("รหัสผ่านไม่ถูกต้อง");
                    }
                    catch 
                    {
                        Toast.Error();
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
