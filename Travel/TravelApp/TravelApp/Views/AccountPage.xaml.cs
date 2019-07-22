using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        private AccountViewModel viewModel;
        public AccountPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new AccountViewModel();
        }
        async void NavigateToLogin_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }

        async void NavigateToRegister_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new RegisterPage()));

        }

        async void NavigateToUpdate_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditAccountPage(viewModel.User));

        }

        async void NavigateToChangePassword_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePasswordPage());

        }

        void Logout_OnClicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            viewModel.CheckLogin();
        }

    }
}