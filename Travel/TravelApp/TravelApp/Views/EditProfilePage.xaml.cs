using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models;
using TravelApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        internal EditProfilePage(AspNetUser user)
        {
            InitializeComponent();
            EditProfileViewModel editProfileViewModel = new EditProfileViewModel(user);
            BindingContext = editProfileViewModel;
            NameEntry.Completed += (s, e) => { PhoneEntry.Focus(); };
            PhoneEntry.Completed += (s, e) => { PasswordEntry.Focus(); };
            PasswordEntry.Completed += (s, e) => { editProfileViewModel.UpdateUserCommand.Execute(null); };
        }



    }
}