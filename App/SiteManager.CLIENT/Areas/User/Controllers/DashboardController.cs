using Microsoft.AspNetCore.Mvc;

namespace SiteManager.CLIENT.Areas.User.Controllers
{
	[Area("User")]
	public class DashboardController : Controller
	{
		private readonly HttpClient _httpClient;
		public DashboardController(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://localhost:7142");
		}
		[Route("account")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
