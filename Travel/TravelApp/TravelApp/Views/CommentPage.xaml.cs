using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentPage : ContentPage
    {
        internal CommentPage(Place place)
        {
            InitializeComponent();
            Title = "ความคิดเห็น";
        }
    }
}