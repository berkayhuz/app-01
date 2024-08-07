using Microsoft.AspNetCore.Mvc;
using SiteManager.CLIENT.Models;
using SiteManager.CLIENT.Services.Abstractions;

namespace SiteManager.CLIENT.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IAuthenticationService authenticationService, ILogger<LoginController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [Route("account/login")]
        [HttpGet]
        public IActionResult Login()
        {
            _logger.LogDebug("GET /account/login accessed");
            return View();
        }

        [Route("account/login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var token = await _authenticationService.LoginAsync(model);
                    if (!string.IsNullOrEmpty(token))
                    {
                        _logger.LogInformation("User logged in successfully.");
						return RedirectToAction("Index", "User", new { area = "User" });
					}
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred during login.");
                    ModelState.AddModelError(string.Empty, "An error occurred. Please try again.");
                }
            }
            return View(model);
        }
    }
}