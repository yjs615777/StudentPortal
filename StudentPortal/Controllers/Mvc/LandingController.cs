using Microsoft.AspNetCore.Mvc;

namespace StudentPortal.Controllers.Mvc
{
    public class LandingController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}
