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
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    class ChangePasswordViewModel : INotifyPropertyChanged
    {
        readonly ApiService _apiService = new ApiService();
        private ChangePassword _changePassword;

        public ChangePasswordViewModel()
        {
            ChangePassword = new ChangePassword();
        }
        public ChangePassword ChangePassword
        {
            get => _changePassword;
            set
            {
                if (Equals(value, _changePassword)) return;
                _changePassword = value;
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
                        if (string.IsNullOrWhiteSpace(ChangePassword.OldPassword) ||
                            string.IsNullOrWhiteSpace(ChangePassword.NewPassword) ||
                            string.IsNullOrWhiteSpace(ChangePassword.ConfirmPassword))
                            Toast.Show();
                        else if(ChangePassword.NewPassword.Length<6)
                            Toast.Warning("ความยาวไม่ต่ำกว่า 6 ตัว");
                        else if (!string.Equals(ChangePassword.ConfirmPassword, ChangePassword.NewPassword))
                            Toast.Warning("รหัสผ่านใหม่ไม่ตรงกัน");
                        else if (await _apiService.PutUserAsync(ChangePassword))
                        {
                            Toast.Success("อัพเดตรหัสผ่านแล้ว");
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        else Toast.Warning("รหัสผ่านไม่ถูกต้อง");
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
