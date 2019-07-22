using TravelApp.Models;

namespace TravelApp.ViewModels
{
    public class PlaceDetailViewModel : BaseViewModel
    {
        public PlaceDetailViewModel() { }

        public PlaceDetailViewModel(Place place)
        {
            Title = place?.PlaceName;
            Place = place;
        }

        public Place Place { get; set; }
        
    }
}