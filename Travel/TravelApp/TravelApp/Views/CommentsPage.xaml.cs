using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Helpers;
using TravelApp.Models;
using TravelApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentsPage : ContentPage
    {
        private CommentViewModel viewModel;

        public CommentsPage() { }

        public CommentsPage(CommentViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;

        }

        async void OnCommentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var comment = (Comment)e.SelectedItem;
            if (comment == null || Setting.UserId != comment.CommentUserId)
                return;
            await Navigation.PushModalAsync(new NavigationPage(new CommentDetailPage(comment)));
            CommentsListView.SelectedItem = null;
        }
    }
}