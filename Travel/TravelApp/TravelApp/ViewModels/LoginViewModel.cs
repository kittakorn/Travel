using System;
using System.Collections.Generic;
using System.Text;
using TravelApp.Helpers;
using TravelApp.Models;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private Login _login;

        public LoginViewModel()
        {
            Login = new Login();
        }

        public Login Login
        {
            get => _login;
            set
            {
                if (Equals(value, _login)) return;
                _login = value;
                OnPropertyChanged();
            }
        }

        public Command LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (await ApiService.LoginAsync(Login))
                        {
                            Toast.Success("เข้าสู่ระบบสำเร็จ");
                            MessagingCenter.Send(this, "UpdateComment");
                            MessagingCenter.Send(this, "CheckLogin");
                            await Application.Current.MainPage.Navigation.PopModalAsync();
                        }
                        else
                            Toast.Warning("ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
                    }
                    catch (Exception ex)
                    {
                        Toast.Error(ex.Message);
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
            }
        }
    }
}
