using Xamarin.Forms;

namespace TravelApp.Controls
{
    public class FlatNavigationPage : NavigationPage
    {
        public static BindableProperty LogoProperty = BindableProperty.Create(nameof(Logo), typeof(string), typeof(FlatNavigationPage), null);

        public string Logo
        {
            get => (string)GetValue(LogoProperty);
            set => SetValue(LogoProperty, value);
        }

        public FlatNavigationPage(Page root)
            : base(root)
        {
            this.BarBackgroundColor = Color.FromHex("#2196F3");
            this.BarTextColor = Color.White;
        }
    }
}