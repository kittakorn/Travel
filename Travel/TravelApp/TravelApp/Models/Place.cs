using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Models
{
    public class Place
    {
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
        public decimal? PlaceRating { get; set; }
        public int PlaceVisitor { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
