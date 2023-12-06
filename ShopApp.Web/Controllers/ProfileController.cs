using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Web.Identity;
using ShopApp.Web.Models;
using System.Linq;

namespace ShopApp.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            var user = _userManager.Users.First(x => x.Id == _userManager.GetUserId(User));
            ProfileModel model = new ProfileModel()
            {
                UserId = user.Id,
                Fullname = user.FullName,
                Username = user.UserName,
                Email = user.Email
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword(string id)
        {
            ChangePasswordModel model = new ChangePasswordModel()
            {
                UserId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.ChangePasswordAsync(_userManager.Users.First(x => x.Id == _userManager.GetUserId(User)), model.PastPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }


            }

            return View(model);


        }
    }
}
