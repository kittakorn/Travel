using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TravelApp.Annotations;
using TravelApp.Helpers;
using TravelApp.Models;
using TravelApp.Services;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    public class CommentDetailViewModel : BaseViewModel
    {
        private Comment _comment;
        public CommentDetailViewModel() { }

        public CommentDetailViewModel(Comment comment)
        {
            Comment = comment;
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
        public Command UpdateCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (await ApiService.PutCommentAsync(Comment))
                        {
                            Toast.Success("อัพเดตความคิดเห็นแล้ว");
                            MessagingCenter.Send(this, "UpdateComment");
                            await Application.Current.MainPage.Navigation.PopModalAsync();
                        }
                        else Toast.Show();
                    }
                    catch (Exception ex)
                    {
                        Toast.Error(ex.Message);
                    }
                });
            }
        }

        public Command DeleteCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (await ApiService.DeleteCommentAsync(Comment.CommentId))
                        {
                            Toast.Success("ลบความติดเห็นสำเร็จ");
                            MessagingCenter.Send(this, "UpdateComment");
                            await Application.Current.MainPage.Navigation.PopModalAsync();
                        }
                        else Toast.Error();
                    }
                    catch (Exception ex)
                    {
                        Toast.Error(ex.Message);
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
