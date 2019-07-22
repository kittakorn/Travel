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
    public partial class CommentDetailPage : ContentPage
    {
        public CommentDetailPage()
        {
            InitializeComponent();
        }

        public CommentDetailPage(Comment comment)
        {
            InitializeComponent();
            BindingContext = new CommentDetailViewModel(comment);

        }

        async void Cancel_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}