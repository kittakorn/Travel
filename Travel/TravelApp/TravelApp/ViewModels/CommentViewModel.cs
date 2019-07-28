using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TravelApp.Helpers;
using TravelApp.Models;
using TravelApp.Views;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    public class CommentViewModel : BaseViewModel
    {
        private Comment _comment;

        public CommentViewModel(Place place)
        {
            Comments = new ObservableCollection<Comment>(place.Comments.OrderByDescending(x => x.CommentId));
            PlaceId = place.PlaceId;
            Comment = new Comment
            {
                CommentUserId = Setting.UserId,
                CommentPlaceId = place.PlaceId
            };
            MessagingCenter.Subscribe<CommentDetailViewModel>(this, "UpdateComment",
                (sender) =>
                {
                    LoadCommentCommand.Execute(null);
                    Comment = new Comment
                    {
                        CommentUserId = Setting.UserId,
                        CommentPlaceId = place.PlaceId
                    };
                });
            MessagingCenter.Subscribe<LoginViewModel>(this, "UpdateComment",
                (sender) =>
                {
                    Comment = new Comment
                    {
                        CommentUserId = Setting.UserId,
                        CommentPlaceId = place.PlaceId
                    };
                });
        }

        public CommentViewModel() { }

        public ObservableCollection<Comment> Comments { get; set; }

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

        public int PlaceId { get; set; }
        public Command LoadCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsBusy)
                        return;
                    IsBusy = true;

                    try
                    {
                        Comments.Clear();
                        var places = await ApiService.GetPlaceAsync(PlaceId);
                        foreach (var comment in places.Comments.OrderByDescending(x => x.CommentId))
                        {
                            Comments.Add(comment);
                        }
                    }
                    catch (Exception ex)
                    {
                        Toast.Error(ex.Message);
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
            }
        }

        public Command AddCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (string.IsNullOrEmpty(Setting.UserId))
                        {
                            Toast.Warning("กรุณาเข้าสู่ระบบก่อนแสดงความคิดเห็น");
                            await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                        }
                        else if (await ApiService.PostCommentAsync(Comment))
                        {
                            Toast.Success("แสดงความคิดเห็นสำเร็จ");
                            LoadCommentCommand.Execute(null);
                            Comment = new Comment
                            {
                                CommentPlaceId = Comment.CommentPlaceId,
                                CommentUserId = Setting.UserId,
                            };
                        }
                        else
                            Toast.Show();
                    }
                    catch (Exception ex)
                    {
                        Toast.Error(ex.Message);
                    }
                });
            }
        }
    }
}
