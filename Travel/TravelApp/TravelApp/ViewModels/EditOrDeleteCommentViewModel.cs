using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TravelApp.Annotations;
using TravelApp.Models;

namespace TravelApp.ViewModels
{
    class EditOrDeleteCommentViewModel : INotifyPropertyChanged
    {
        public EditOrDeleteCommentViewModel() { }

        public EditOrDeleteCommentViewModel(Comment comment)
        {
            Comment = comment;
        }

        public Comment Comment { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
