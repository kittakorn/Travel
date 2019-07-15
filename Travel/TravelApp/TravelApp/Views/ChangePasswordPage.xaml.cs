using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
            ChangePasswordViewModel changePassword = new ChangePasswordViewModel();
            BindingContext = changePassword;
            OldPasswordEntry.Completed += (s, e) => { NewPasswordEntry.Focus(); };
            NewPasswordEntry.Completed += (s, e) => { ConfirnPasswordEntry.Focus(); };
            ConfirnPasswordEntry.Completed += (s, e) => { changePassword.UpdateUserCommand.Execute(null); };
        }
    }
}