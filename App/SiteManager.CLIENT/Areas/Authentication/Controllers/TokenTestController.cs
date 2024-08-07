using Microsoft.AspNetCore.Mvc;

namespace SiteManager.CLIENT.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class TokenTestController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            ViewBag.Token = token;
            return View();
        }
    }


}
