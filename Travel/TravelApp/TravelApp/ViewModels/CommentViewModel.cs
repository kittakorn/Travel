using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Plugin.Toast;
using TravelApp.Annotations;
using TravelApp.Helpers;
using TravelApp.Models;
using TravelApp.Services;
using TravelApp.Views;
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
            Comment = new Comment {CommentUserId = Setting.UserId};
            GetCommentCommand.Execute(null);
        }

        private ObservableCollection<Comment> _comments;
        private Comment _comment;
        private int _placeId;
        private Comment _selectComment;
        private bool _listIsVisible;
        private bool _labelIsVisible;

        public bool LabelIsVisible
        {
            get => _labelIsVisible;
            set
            {
                if (value == _labelIsVisible) return;
                _labelIsVisible = value;
                OnPropertyChanged();
            }
        }

        public bool ListIsVisible
        {
            get => _listIsVisible;
            set
            {
                if (value == _listIsVisible) return;
                _listIsVisible = value;
                OnPropertyChanged();
            }
        }

        public Comment SelectComment
        {
            get => _selectComment;
            set
            {
                if (Equals(value, _selectComment)) return;
                _selectComment = value;
                OnPropertyChanged();
                ActionCommentCommand.Execute(null);
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

        public ObservableCollection<Comment> Comments
        {
            get => _comments;
            set
            {
                if (Equals(value, _comments)) return;
                _comments = value;
                OnPropertyChanged();
                if (!Comments.Any())
                {
                    LabelIsVisible = true;
                    ListIsVisible = false;
                }
                else
                {
                    ListIsVisible = true;
                    LabelIsVisible = false;
                }
            }
        }

        public ICommand PostCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (string.IsNullOrWhiteSpace(Comment.CommentDescription)) Toast.Show();
                    else if (await _apiService.PostCommentAsync(Comment)) GetCommentCommand.Execute(null);
                    else Toast.Error();
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
                    Comments = new ObservableCollection<Comment>(place.Comments.OrderByDescending(x => x.CommentId));
                    Comment = new Comment
                    {
                        CommentPlaceId = place.PlaceId,
                        CommentUserId = Setting.UserId,
                    };
                });
            }
        }

        public ICommand ActionCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!SelectComment.CommentUserId.Equals(Setting.UserId)) return;
                    var choice = await Current.MainPage.DisplayActionSheet("เลือกรายการ", "ปิด", null, "แก้ไข", "ลบ");
                    switch (choice)
                    {
                        case "ลบ":
                            {
                                try
                                {
                                    if (await _apiService.DeleteCommentAsync(SelectComment.CommentId))
                                    {
                                        Comments.Remove(SelectComment);
                                        Toast.Success("ลบความติดเห็นสำเร็จ");
                                        if (!Comments.Any())
                                        {
                                            LabelIsVisible = true;
                                            ListIsVisible = false;
                                        }
                                        else
                                        {
                                            ListIsVisible = true;
                                            LabelIsVisible = false;
                                        }
                                    }
                                    else Toast.Error();
                                }
                                catch
                                {
                                    Toast.Error();
                                }
                                break;
                            }

                        case "แก้ไข":
                            {
                                await Current.MainPage.Navigation.PushAsync(new EditOrDeleteCommentPage(SelectComment));
                                break;
                            }
                        default: return;
                    }
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
