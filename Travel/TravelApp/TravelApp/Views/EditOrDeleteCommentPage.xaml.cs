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
    public partial class EditOrDeleteCommentPage : ContentPage
    {
        public int id { get; set; }
        internal EditOrDeleteCommentPage(Comment comment)
        {
            InitializeComponent();
            id = comment.CommentPlaceId;
            EditOrDeleteCommentViewModel editOrDelete = new EditOrDeleteCommentViewModel(comment);
            BindingContext = editOrDelete;
        }

        protected override bool OnBackButtonPressed()
        {
            var mainPage = new PlaceTabedPage(id) as TabbedPage;
            mainPage.CurrentPage = mainPage.Children[1];
            Application.Current.MainPage = new NavigationPage(new MainPage());
            Application.Current.MainPage.Navigation.PushAsync(mainPage);
            return true;
        }
    }
}