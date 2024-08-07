using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SiteManager.CLIENT.Models;

namespace SiteManager.CLIENT.Areas.Authentication.Controllers
{
	[Area("Authentication")]
	public class RegisterController : Controller
	{
		private readonly HttpClient _httpClient;
		public RegisterController(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://localhost:7142");
		}
		[Route("account/register")]
		[HttpGet]
		public IActionResult Register()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}
			ViewBag.Message = TempData["Message"];
			return View();
		}
		[Route("account/register")]
		[HttpPost]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("/api/Account/register", content);
			if (response.IsSuccessStatusCode)
			{
				TempData["Message"] = "Üyelik başarılı! Lütfen emailinizi kontrol edin.";
				return RedirectToAction("Login", "Login");
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
						ModelState.AddModelError(string.Empty, $"Registration failed: {errorMessage}");
					}
					else
					{
						ModelState.AddModelError(string.Empty, $"Registration failed: {responseContent}");
					}
				}
			}
			catch (JsonException)
			{
				ModelState.AddModelError(string.Empty, $"Registration failed: {responseContent}");
			}

			return View(model);
		}

	}
}
