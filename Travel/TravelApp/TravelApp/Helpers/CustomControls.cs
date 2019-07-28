using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TravelApp.CustomControls
{
    public class CustomViewCell : ViewCell
    {
        public static readonly BindableProperty SelectedItemBackgroundColorProperty =
            BindableProperty.Create("SelectedItemBackgroundColor",
                typeof(Color),
                typeof(CustomViewCell),
                Color.Default);

        public Color SelectedItemBackgroundColor
        {
            get => (Color)GetValue(SelectedItemBackgroundColorProperty);
            set => SetValue(SelectedItemBackgroundColorProperty, value);
        }
    }
}
