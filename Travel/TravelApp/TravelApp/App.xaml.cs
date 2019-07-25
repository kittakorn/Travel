using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TravelApp.Services;
using TravelApp.Views;

namespace TravelApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            HotReloader.Current.Run(this);
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
