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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            RegisterViewModel registerViewModel = new RegisterViewModel();
            BindingContext = registerViewModel;
            EmailEntry.Completed += (s, e) => { PasswordEntry.Focus(); };
            PasswordEntry.Completed += (s, e) => { ConfirmPassowrdEntry.Focus(); };
            ConfirmPassowrdEntry.Completed += (s, e) => { NameEntry.Focus(); };
            NameEntry.Completed += (s, e) => { PhoneEntry.Focus(); };
            PhoneEntry.Completed += (s, e) => { registerViewModel.RegisterCommand.Execute(null); };
        }
        protected override bool OnBackButtonPressed()
        {
            var mainPage = new MainPage() as TabbedPage;
            mainPage.CurrentPage = mainPage.Children[2];
            Application.Current.MainPage = new NavigationPage(mainPage);
            return true;
        }
     
    }
}