using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelWeb.Models
{
    public class PlaceValidation
    {
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        [DataType(DataType.MultilineText)]
        public string PlaceDescription { get; set; }
        public string PlaceImage { get; set; }
        public Nullable<decimal> PlaceRating { get; set; }
        public string PlaceLatitude { get; set; }
        public string PlaceLongitude { get; set; }
        public int PlaceVisitor { get; set; }
        public string PlaceAddress { get; set; }
        public string PlacePhone { get; set; }
        [DataType(DataType.MultilineText)]
        public string PlaceOpenDate { get; set; }
        public int PlaceProvinceId { get; set; }
        public int PlaceCategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual Province Province { get; set; }
    }

    [MetadataType(typeof(PlaceValidation))]
    public partial class Place { }
}