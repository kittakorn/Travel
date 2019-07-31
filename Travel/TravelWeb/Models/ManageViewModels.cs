using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace TravelWeb.Models
{

    public class UpdateAccountViewModel
    {
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [StringLength(20, ErrorMessage = "ความยาวระหว่าง 5 - 20 ตัวอักษร", MinimumLength = 5)]
        [Display(Name = "ชื่อที่แสดง")]
        public string Name { get; set; }


        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "เบอร์โทรศัพท์")]
        [RegularExpression(@"^(^\+62\s?|^0)(\d{3,4}-?){2}\d{3,4}$", ErrorMessage = "เบอร์โทรศัพท์ไม่ถูกต้อง")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "เบอร์โทรศัพท์ไม่ถูกต้อง")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DataType(DataType.Password)]
        [Display(Name = "ยืนยันรหัสผ่าน")]
        public string Password { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DataType(DataType.Password)]
        [Display(Name = "รหัสผ่านเก่า")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [StringLength(100, ErrorMessage = "ความยาวไม่ตำกว่า 6 ตัวอักษร", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "รหัสผ่านใหม่")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DataType(DataType.Password)]
        [Display(Name = "ยืนยันรหัสผ่าน")]
        [Compare("NewPassword", ErrorMessage = "รหัสผ่านไม่ตรงกัน")]
        public string ConfirmPassword { get; set; }
    }

}