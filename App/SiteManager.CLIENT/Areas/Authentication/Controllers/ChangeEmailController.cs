using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManager.CLIENT.Services.Abstractions;

namespace SiteManager.CLIENT.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    [Authorize]
    public class ChangeEmailController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public ChangeEmailController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Route("account/change-email")]
        [HttpGet]
        public IActionResult ChangeEmail()
        {
            return View();
        }

        [Route("account/change-email")]
        [HttpPost]
        public async Task<IActionResult> ChangeEmail(string newEmail)
        {
            if (string.IsNullOrEmpty(newEmail))
            {
                ModelState.AddModelError(string.Empty, "New email cannot be empty.");
                return View();
            }

            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login", new { area = "Authentication" });
            }

            var result = await _authenticationService.ChangeEmailAsync(newEmail, token);
            if (result)
            {
                ViewBag.Message = "A confirmation link has been sent to your current email address.";
                return View();
            }

            ModelState.AddModelError(string.Empty, "Failed to send confirmation link.");
            return View();
        }

        [Route("account/email-changed")]
        [HttpGet]
        public async Task<IActionResult> ConfirmChangeEmail(string token, string newEmail)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(newEmail))
            {
                return BadRequest("Invalid email confirmation request.");
            }

            var result = await _authenticationService.ConfirmChangeEmailAsync(token, newEmail);
            if (result)
            {
                ViewBag.Message = "Email address has been changed successfully.";
                return View();
            }

            ViewBag.Message = "Failed to change email address.";
            return View();
        }
    }
}
