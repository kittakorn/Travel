using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace TravelApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Title = "สถานที่ท่องเที่ยว";
            Children.Add(new PlacePage() { Icon = "ic_action_map.png" });
            Children.Add(new FavoritePage() { Icon = "ic_action_love.png" });
            Children.Add(new AccountPage() { Icon = "ic_action_perm_identity.png" });
            On<Android>().SetBarSelectedItemColor(Color.White);
            this.CurrentPageChanged += CurrentPageHasChanged;
            BarBackgroundColor = Color.FromHex("#148F77");
        }

        protected void CurrentPageHasChanged(object sender, EventArgs e)
        {
            var index = this.Children.IndexOf(this.CurrentPage);
            switch (index)
            {
                case 0:
                    this.Title = "สถานที่ท่องเที่ยว";
                    break;
                case 1:
                    this.Title = "สถานที่ชื่นชอบ";
                    break;
                case 2:
                    this.Title = "ข้อมูลส่วนตัว";
                    break;
            }
        }

    }
}
