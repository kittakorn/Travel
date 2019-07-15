using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Plugin.Toast;
using TravelApp.Annotations;
using TravelApp.Helpers;
using TravelApp.Services;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        readonly ApiService _apiService = new ApiService();

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
                    if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                        Toast.Show();
                    else
                    {
                        try
                        {
                            if (await _apiService.LoginAsync(Email, Password))
                            {
                                Toast.Success("เข้าสู่ระบบสำเร็จ");
                                var mainPage = new MainPage() as TabbedPage;
                                mainPage.CurrentPage = mainPage.Children[2];
                                Application.Current.MainPage = new NavigationPage(mainPage);
                            }
                            else
                                Toast.Warning("ชื่อผู้ใช้หรือหรัสผ่านไม่ถูกต้อง");
                        }
                        catch
                        {
                            Toast.Error();
                        }
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
