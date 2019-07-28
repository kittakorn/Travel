using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TravelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceDetailPage : ContentPage
    {
        private PlaceDetailViewModel viewModel;

        public PlaceDetailPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new PlaceDetailViewModel();
           
        }

        async void NavigateToComment_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CommentsPage(new CommentViewModel(viewModel.Place)));
        }

        async void NavigateToMap_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }
    }
}