using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace TravelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceTabedPage : Xamarin.Forms.TabbedPage
    {
        internal PlaceTabedPage(int id)
        {
            InitializeComponent();
            Title = "รายละเอียด";
            Children.Add(new PlaceDetailPage(id) { Icon = "ic_action_description.png" });
            Children.Add(new CommentPage(id) { Icon = "ic_action_message.png" });
            On<Android>().SetBarSelectedItemColor(Color.White);
            BarBackgroundColor = Color.FromHex("#148F77");
            this.CurrentPageChanged += CurrentPageHasChanged;
        }

        protected void CurrentPageHasChanged(object sender, EventArgs e)
        {
            var index = this.Children.IndexOf(this.CurrentPage);
            switch (index)
            {
                case 0:
                    this.Title = "รายละเอียด";
                    break;
                case 1:
                    this.Title = "ความคิดเห็น";
                    break;
            }
        }
    }
}