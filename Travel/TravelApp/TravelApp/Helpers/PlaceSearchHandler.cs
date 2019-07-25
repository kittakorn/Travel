using System.Collections.Generic;
using System.Linq;
using TravelApp.Models;
using TravelApp.Services;
using TravelApp.ViewModels;
using Xamarin.Forms;

namespace TravelApp.Helpers
{
    public class PlaceSearchHandler : SearchHandler
    {
        ApiService _apiService = new ApiService();
        public List<Place> Places { get; set; }
        protected override async void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                Places = await _apiService.GetPlaceAsync();
                ItemsSource = Places.Where(x => x.PlaceName.Contains(newValue) ||
                                                x.CategoryName.Contains(newValue) ||
                                                x.ProvinceName.Contains(newValue)).ToList();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            // Note: strings will be URL encoded for navigation (e.g. "Blue Monkey" becomes "Blue%20Monkey"). Therefore, decode at the receiver.
            await Shell.Current.GoToAsync($"placedetail?name={((Place)item).PlaceName}");
        }

    }
}