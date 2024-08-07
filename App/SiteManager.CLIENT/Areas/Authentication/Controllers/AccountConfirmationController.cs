using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace SiteManager.CLIENT.Areas.Authentication.Controllers
{
	[Area("Authentication")]
	public class AccountConfirmationController : Controller
	{
		private readonly HttpClient _httpClient;
		public AccountConfirmationController(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://localhost:7142");
		}


		[Route("account/resendconfirmationemail")]
		[HttpPost]
		public async Task<IActionResult> ResendConfirmationEmail(string email)
		{
			var content = new StringContent(JsonSerializer.Serialize(new { Email = email }), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("/api/Account/resendconfirmationemail", content);

			if (response.IsSuccessStatusCode)
			{
				TempData["Message"] = "Doğrulama emaili tekrar gönderildi. Lütfen emailinizi kontrol edin.";
			}
			else
			{
				TempData["Message"] = "Doğrulama emaili gönderilemedi. Lütfen tekrar deneyin.";
			}

			return RedirectToAction("Login");
		}
		[Route("account/confirmedsuccess")]
		[HttpGet]
		public IActionResult ConfirmedSuccess()
		{
			return View();
		}
	}
}
