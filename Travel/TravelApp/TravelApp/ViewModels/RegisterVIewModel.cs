using System;
using System.Threading.Tasks;
using TravelApp.Helpers;
using TravelApp.Models;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
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

        public Command RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (await ApiService.RegisterAsync(Register))
                        {
                            Toast.Success("สมัครสมาชิกสำเร็จ");
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
                });
            }
        }
    }
}