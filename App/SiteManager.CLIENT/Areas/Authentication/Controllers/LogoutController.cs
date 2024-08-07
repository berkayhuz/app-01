using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiteManager.CLIENT.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class LogoutController : Controller
    {
        private readonly HttpClient _httpClient;
        public LogoutController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7142");
        }

        [Authorize]
        [HttpGet]
        [Route("account/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login", new { area = "Authentication" });
        }
    }
}