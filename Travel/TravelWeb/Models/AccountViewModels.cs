using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelWeb.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "อีเมล")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DataType(DataType.Password)]
        [Display(Name = "รหัสผ่าาน")]
        public string Password { get; set; }

        [Display(Name = "จำรหัสผ่าน")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [EmailAddress(ErrorMessage = "อีเมลไม่ถูกต้อง")]
        [Display(Name = "อีเมล")]
        public string Email { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [StringLength(20,MinimumLength = 5,ErrorMessage = "ความยาวระหว่าง 5 - 20 ตัวอักษร")]
        [Display(Name = "ชื่อที่แสดง")]
        public string Name { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "เบอร์โทรศัพท์")]
        [RegularExpression(@"^(^\+62\s?|^0)(\d{3,4}-?){2}\d{3,4}$",ErrorMessage = "เบอร์โทรศัพท์ไม่ถูกต้อง")]
        [DataType(DataType.PhoneNumber,ErrorMessage = "เบอร์โทรศัพท์ไม่ถูกต้อง")]
        public string Phone { get; set; }


    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
