using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWeb.Models
{
    public class PlaceViewModel
    {
        public PlaceViewModel(Place place)
        {
            PlaceId = place.PlaceId;
            PlaceName = place.PlaceName;
            PlaceImage = place.PlaceImage.Split(',').ToList();
            CategoryName = place.Category.CategoryName;
            PlaceAddress = place.PlaceAddress;
            ProvinceName = place.Province.ProvinceName;
            PlaceLatitude = place.PlaceLatitude;
            PlaceLongitude = place.PlaceLongitude;
            PlaceOpenDate = place.PlaceOpenDate;
            PlacePhone = place.PlacePhone;
            PlaceDescription = place.PlaceDescription;
            PlaceRating = place.PlaceRating;
            PlaceVisitor = place.PlaceVisitor;
            Comments = place.Comments.ToList().Select(x => new CommentViewModel(x)).ToList();
        }
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public List<string> PlaceImage { get; set; }
        public string CategoryName { get; set; }
        public string PlaceAddress { get; set; }
        public string ProvinceName { get; set; }
        public string PlaceLatitude { get; set; }
        public string PlaceLongitude { get; set; }
        public string PlaceOpenDate { get; set; }
        public string PlacePhone { get; set; }
        public string PlaceDescription { get; set; }
        public decimal? PlaceRating { get; set; }
        public int PlaceVisitor { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}