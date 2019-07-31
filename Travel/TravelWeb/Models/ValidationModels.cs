using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelWeb.Models
{
    public class PlaceValidation
    {
        [Display(Name = "ชื่อสถาณที่")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string PlaceName { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "รายละเอียด")]
        public string PlaceDescription { get; set; }

        [Display(Name = "รูปภาพ")]
        public string PlaceImage { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "Latitude")]
        public string PlaceLatitude { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "Longitude")]
        public string PlaceLongitude { get; set; }

        [Display(Name = "ผู้เข้าชม")]
        public int PlaceVisitor { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "ที่อยู่")]
        public string PlaceAddress { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "เบอร์โทรศัพท์")]
        public string PlacePhone { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "วันเปิดทำการ")]
        public string PlaceOpenDate { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "จังหวัด")]
        public int PlaceProvinceId { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "ประเภท")]
        public int PlaceCategoryId { get; set; }

    }

    [MetadataType(typeof(PlaceValidation))]
    public partial class Place { }
}