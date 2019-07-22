using System;
using System.Collections.Generic;
using System.Text;
using TravelApp.Helpers;
using TravelApp.Models;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    public class ChangePasswordViewModel : BaseViewModel
    {
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

        public Command ChangePasswordCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (await ApiService.PutUserAsync(ChangePassword))
                        {
                            Toast.Success("อัพเดตรหัสผ่านสำเร็จ");
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        else Toast.Warning("กรุณาตรวจสอบข้อมูล");
                    }
                    catch (Exception ex)
                    {
                        Toast.Error(ex.Message);
                    }
                });
            }
        }
    }
}
