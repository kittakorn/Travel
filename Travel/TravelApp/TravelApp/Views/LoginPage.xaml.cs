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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            LoginViewModel loginViewModel = new LoginViewModel();
            BindingContext = loginViewModel;
            EmailEntry.Completed += (s, e) => { PasswordEntry.Focus(); };
            PasswordEntry.Completed += (s, e) => { loginViewModel.LoginCommand.Execute(null); };
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