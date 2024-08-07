using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SiteManager.CLIENT.Models;

namespace SiteManager.CLIENT.Areas.Authentication.Controllers
{
	[Area("Authentication")]
	public class ResetPasswordController : Controller
	{
		private readonly HttpClient _httpClient;
		public ResetPasswordController(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://localhost:7142");
		}

		[Route("account/resetpassword")]
		[HttpGet]
		public IActionResult ResetPassword(string token, string email)
		{
			var model = new ResetPasswordModel { Token = token, Email = email };
			return View(model);
		}
		[Route("account/resetpassword")]
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("/api/Account/resetpassword", content);

			if (response.IsSuccessStatusCode)
			{
				TempData["Message"] = "Şifreniz başarıyla sıfırlandı.";
				return RedirectToAction("ResetPasswordConfirmation");
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
					else if (doc.RootElement.TryGetProperty("message", out JsonElement messageElement))
					{
						var errorMessage = messageElement.GetString();
						ModelState.AddModelError(string.Empty, errorMessage);
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
		[Route("account/resetpasswordconfirmation")]
		[HttpGet]
		public IActionResult ResetPasswordConfirmation()
		{
			return View();
		}
	}
}
