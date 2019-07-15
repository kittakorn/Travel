using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Toast;
using TravelApp.Annotations;
using TravelApp.Helpers;
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
                    try
                    {
                        if (await _apiService.RegisterAsync(Register))
                        {
                            await Task.Delay(1000);
                            Toast.Success("สมัครสมาชิกสำเร็จ กำลังเข้าสู่ระบบ......");
                            if (await _apiService.LoginAsync(Register.Email, Register.Password))
                            {
                                var mainPage = new MainPage() as TabbedPage;
                                mainPage.CurrentPage = mainPage.Children[2];
                                Application.Current.MainPage = new NavigationPage(mainPage);
                            }
                            else Toast.Error();
                        }
                        else
                        {
                            Toast.Show();
                        }
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
