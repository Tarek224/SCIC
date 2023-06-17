using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SCIC.Controllers
{
    public class SCICController : Controller
    {
        #region Ctor
        private readonly IStringLocalizer<SharedResource> localizer;

        public SCICController(IStringLocalizer<SharedResource> localizer)
        {
            this.localizer = localizer;
        }
        #endregion

        [HttpGet]
        public IActionResult MainPage()
        {
            ViewData["Title"] = localizer["Main Page"];
            return View();
        }

        [HttpGet]
        public IActionResult AboutSKinCare()
        {
            ViewData["Title"] = localizer["Main Page"];
            return View();
        }

    }
}
