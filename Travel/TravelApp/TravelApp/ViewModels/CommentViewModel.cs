using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TravelApp.Annotations;
using TravelApp.Helpers;
using TravelApp.Models;
using TravelApp.Services;
using TravelApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Forms.Application;

namespace TravelApp.ViewModels
{
    class CommentViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService = new ApiService();

        public CommentViewModel() { }

        public CommentViewModel(int id)
        {
            PlaceId = id;
            GetCommentCommand.Execute(null);
        }

        private List<Comment> _comments;
        private Comment _comment;
        private string _commentCount;
        private int _placeId;
        private Comment _selectComment;

        public Comment SelectComment
        {
            get => _selectComment;
            set
            {
                if (Equals(value, _selectComment)) return;
                _selectComment = value;
                OnPropertyChanged();
                if (SelectComment.CommentUserId.Equals(Setting.UserId))
                    Current.MainPage.Navigation.PushAsync(new EditOrDeleteCommentPage(SelectComment));
            }
        }

        public int PlaceId
        {
            get => _placeId;
            set
            {
                if (value == _placeId) return;
                _placeId = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(GetCommentCommand));
            }
        }

        public string CommentCount
        {
            get => _commentCount;
            set
            {
                if (value == _commentCount) return;
                _commentCount = value;
                OnPropertyChanged();
            }
        }

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

        public List<Comment> Comments
        {
            get => _comments;
            set
            {
                if (Equals(value, _comments)) return;
                _comments = value;
                OnPropertyChanged();
            }
        }

        public ICommand PostCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (string.IsNullOrEmpty(Comment.CommentDescription))
                        await Application.Current.MainPage.DisplayAlert("แจ้งเตือน", "กรุณาณกรอกข้อมูล", "ปิด");
                    if (await _apiService.PostCommentAsync(Comment))
                    {
                        GetCommentCommand.Execute(null);
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("แจ้งเตือน", "ไม่สำเร็จ", "ปิด");
                });
            }
        }

        public ICommand GetCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var place = await _apiService.GetPlaceAsync(PlaceId);
                    Comments = place.Comments.OrderByDescending(x => x.CommentId).ToList();
                    Comment = new Comment
                    {
                        CommentPlaceId = place.PlaceId,
                        CommentUserId = Setting.UserId,
                    };
                    CommentCount = $"ความคิดเห็น({place.Comments.Count})";
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
