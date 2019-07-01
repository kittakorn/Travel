using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace TravelApp.Helpers
{
    static class Setting
    {
        public static string AccessToken
        {
            get => Preferences.Get("AccessToken", "");
            set => Preferences.Set("AccessToken", value);
        }
        public static string UserId
        {
            get => Preferences.Get("UserId", "");
            set => Preferences.Set("UserId", value);
        }

        
    }
}
