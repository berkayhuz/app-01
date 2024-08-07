using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SiteManager.CLIENT.Models;

namespace SiteManager.CLIENT.Areas.Authentication.Controllers
{
	[Area("Authentication")]
	public class ForgotPasswordController : Controller
	{
		private readonly HttpClient _httpClient;
		public ForgotPasswordController(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://localhost:7142");
		}


		[Route("account/forgotpassword")]
		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}
		[Route("account/forgotpassword")]
		[HttpPost]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("/api/Account/forgotpassword", content);

			if (response.IsSuccessStatusCode)
			{
				TempData["Message"] = "Şifre sıfırlama emaili gönderildi. Lütfen emailinizi kontrol edin.";
				return RedirectToAction("ForgotPasswordConfirmation");
			}

			var responseContent = await response.Content.ReadAsStringAsync();
			try
			{
				using (JsonDocument doc = JsonDocument.Parse(responseContent))
				{
					if (doc.RootElement.TryGetProperty("errors", out JsonElement errors))
					{
						foreach (var error in errors.EnumerateObject())
						{
							foreach (var errorMsg in error.Value.EnumerateArray())
							{
								ModelState.AddModelError(string.Empty, errorMsg.GetString());
							}
						}
					}
					else if (doc.RootElement.TryGetProperty("title", out JsonElement titleElement))
					{
						var errorMessage = titleElement.GetString();
						ModelState.AddModelError(string.Empty, $"Şifre sıfırlama işlemi başarısız: {errorMessage}");
					}
					else
					{
						ModelState.AddModelError(string.Empty, $"Şifre sıfırlama işlemi başarısız: {responseContent}");
					}
				}
			}
			catch (JsonException)
			{
				ModelState.AddModelError(string.Empty, $"Şifre sıfırlama işlemi başarısız: {responseContent}");
			}

			return View(model);
		}
		[Route("account/forgotpasswordconfirmation")]
		[HttpGet]
		public IActionResult ForgotPasswordConfirmation()
		{
			return View();
		}
	}
}
