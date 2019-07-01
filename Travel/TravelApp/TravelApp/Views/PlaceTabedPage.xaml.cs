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
    public partial class PlaceTabedPage : TabbedPage
    {
        internal PlaceTabedPage(int id)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Children.Add(new PlaceDetailPage(id));
            Children.Add(new CommentPage(id));
        }
    }
}