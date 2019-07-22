using System;
using System.Collections.Generic;
using System.Text;
using TravelApp.Helpers;
using TravelApp.Models;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    public class EditAccountViewModel : BaseViewModel
    {
        private EditProfile _profile;
        public EditAccountViewModel() { }

        public EditAccountViewModel(User user)
        {
            Profile = new EditProfile { Name = user.Name, Email = user.Email };
        }

        public Command UpdateUserCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (await ApiService.PutUserAsync(Profile))
                        {
                            Toast.Success("อัพเดตข้อมูลแล้ว");
                            MessagingCenter.Send(this, "UpdateAccount");
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        else
                            Toast.Warning("กรุณาตรวจสอบข้อมูล");
                    }
                    catch (Exception ex)
                    {
                        Toast.Error(ex.Message);
                    }
                });
            }
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

    }
}
