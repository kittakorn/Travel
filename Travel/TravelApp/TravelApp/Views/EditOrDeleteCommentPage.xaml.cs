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
        internal EditOrDeleteCommentPage(Comment comment)
        {
            InitializeComponent();
            EditOrDeleteCommentViewModel editOrDelete = new EditOrDeleteCommentViewModel(comment);
            BindingContext = editOrDelete;
        }
    }
}