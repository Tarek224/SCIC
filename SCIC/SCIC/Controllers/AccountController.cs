using SCIC.Data;
using SCIC.Models;
using SCIC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SCIC.Controllers
{
    public class AccountController : Controller
    {
        #region Ctor
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly IConfiguration config;

        public AccountController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IStringLocalizer<SharedResource> localizer,
            IConfiguration config)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.localizer = localizer;
            this.config = config;
        }
        #endregion

        #region Validation Action
        [AcceptVerbs("Get", "Post")]
        public ActionResult CheckName(string Name)
        {
            if (Name == "")
                return Json(false);
            else
                return Json(true);
        }

        [AcceptVerbs("Get", "Post")]
        public ActionResult CheckDate(DateTime DateOfBirth)
        {
            var result = DateOfBirth.Date > DateTime.Now.Date;

            if (result)
                return Json(false);
            else
                return Json(true);
        }
        #endregion

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Title"] = @localizer["Sign Up"];
            return View(new RegisterViewModel { Gender = Gender.Male, UserType = UserType.Doctor });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Name = model.Name,
                    Email = model.Email,
                    Gender = model.Gender,
                    EmailConfirmed = true,
                    UserName = model.Email.Split('@')[0],
                    UserType = model.UserType,
                    DateOfBirth = model.DateOfBirth
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("MainPage", "SCIC");
                }
                else
                {
                    ViewData["Title"] = @localizer["Sign Up"];
                    ViewBag.error = @localizer["Incorrect Data"];
                    ModelState.AddModelError("Username", @localizer["Incorrect Data"]);
                    return View(model);
                }
                
            }
            else
            {
                string str = "";
                foreach (ModelStateEntry modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        str += error.ErrorMessage + Environment.NewLine;
                    }
                }
                ViewData["Title"] = @localizer["Sign Up"];
                return View(model);
            }
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = @localizer["Sign In"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email.Split('@')[0], model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("MainPage", "SCIC");
                }
                else
                {
                    ModelState.AddModelError("Username", @localizer["Incorrect Username or Password.!!"]);
                    ViewBag.error = @localizer["Incorrect Username or Password.!!"];
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("Username", @localizer["Incorrect Username or Password.!!"]);
                ViewBag.error = @localizer["Incorrect Username or Password.!!"];
                return View(model);
            }
        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region AccessDenied
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            ViewData["Title"] = localizer["Access Denied"];
            return View();
        }
        #endregion
    }
}
