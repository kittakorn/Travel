using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public async Task<ActionResult> EditProfile()
        {
            var findUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            UpdateAccountViewModel model = new UpdateAccountViewModel()
            {
                Name = findUser.Name,
                Phone = findUser.PhoneNumber
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(UpdateAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var findUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var user = await UserManager.FindAsync(findUser.Email, model.Password);
            bool nameEquals = UserManager.Users.Any(x => x.Name.Equals(model.Name) && !x.Email.Equals(findUser.Email));
            bool phoneEquals = UserManager.Users.Any(x => x.PhoneNumber.Equals(model.Phone) && !x.Email.Equals(findUser.Email));
            if (user != null)
            {
                if (user.Name.Equals(model.Name) && user.Name.Equals(model.Phone))
                    return RedirectToAction("Index", "Places");
                if (nameEquals || phoneEquals)
                {
                    if (nameEquals)
                        ModelState.AddModelError("Name", "ชื่อนี้มีผู้ใช้แล้ว");
                    if (phoneEquals)
                        ModelState.AddModelError("Phone", "เบอร์โทรศัพท์นี้มีผู้ใช้แล้ว");
                    return View(model);
                }
                user.Name = model.Name;
                user.PhoneNumber = model.Phone;
                await UserManager.UpdateAsync(user);
                return RedirectToAction("Index", "Places");

            }
            ModelState.AddModelError("", "รหัสผ่านไม่ถูกต้อง");
            return View(model);
        }


        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", "Places");
            }
            ModelState.AddModelError("", "รหัสผ่านเก่าไม่ถูกต้อง");
            return View(model);
        }



    }
}