using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Plugin.Toast;
using TravelApp.Annotations;
using TravelApp.Helpers;
using TravelApp.Models;
using TravelApp.Services;
using TravelApp.Views;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    class EditOrDeleteCommentViewModel : INotifyPropertyChanged
    {
        readonly ApiService _apiService = new ApiService();

        public EditOrDeleteCommentViewModel() { }

        public EditOrDeleteCommentViewModel(Comment comment)
        {
            Comment = comment;
        }

        private Comment _comment;

        public Comment Comment
        {
            get => _comment;
            set
            {
                if (Equals(value, _comment)) return;
                _comment = value;
                OnPropertyChanged();
            }
        }

        public ICommand PutCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (string.IsNullOrEmpty(Comment.CommentDescription)) Toast.Show();
                    if (await _apiService.PutCommentAsync(Comment))
                    {
                        Toast.Success("อัพเดตความคิดเห็นแล้ว");
                        var mainPage = new PlaceTabedPage(Comment.CommentPlaceId) as TabbedPage;
                        mainPage.CurrentPage = mainPage.Children[1];
                        Application.Current.MainPage = new NavigationPage(new MainPage());
                        await Application.Current.MainPage.Navigation.PushAsync(mainPage);
                    }
                    else Toast.Error();
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
