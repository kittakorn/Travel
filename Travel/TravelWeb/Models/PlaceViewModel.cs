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
            PlaceImages = place.PlaceImage.Split(',').ToList().Select(x => "http://ccs.kru.ac.th/cs59/cs06/WebMVC/Images/" + x).ToList();
            PlaceImage = "http://ccs.kru.ac.th/cs59/cs06/WebMVC/Images/" + place.PlaceImage.Split(',')[0];
            CategoryName = place.Category.CategoryName;
            PlaceAddress = place.PlaceAddress;
            ProvinceName = place.Province.ProvinceName;
            PlaceLatitude = place.PlaceLatitude;
            PlaceLongitude = place.PlaceLongitude;
            PlaceOpenDate = place.PlaceOpenDate;
            PlacePhone = place.PlacePhone;
            PlaceDescription = place.PlaceDescription;
           
            PlaceVisitor = place.PlaceVisitor;
            Comments = place.Comments.ToList().Select(x => new CommentViewModel(x)).ToList();
        }
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public List<string> PlaceImages { get; set; }
        public string PlaceImage { get; set; }
        public string CategoryName { get; set; }
        public string PlaceAddress { get; set; }
        public string ProvinceName { get; set; }
        public string PlaceLatitude { get; set; }
        public string PlaceLongitude { get; set; }
        public string PlaceOpenDate { get; set; }
        public string PlacePhone { get; set; }
        public string PlaceDescription { get; set; }
        public int PlaceVisitor { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}