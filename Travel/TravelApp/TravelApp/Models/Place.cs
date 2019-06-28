using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Models
{
    class Place
    {
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string PlaceDescription { get; set; }
        public decimal? PlaceRating { get; set; }
        public string PlaceImage { get; set; }
        public string PlaceLatitude { get; set; }
        public string PlaceLongitude { get; set; }
        public int PlaceVisitor { get; set; }
        public string PlaceAddress { get; set; }
        public string PlacePhone { get; set; }
        public string PlaceOpenDate { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Province Province { get; set; }
    }
}
