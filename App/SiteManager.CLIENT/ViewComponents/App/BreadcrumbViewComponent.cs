using Microsoft.AspNetCore.Mvc;
using SiteManager.CLIENT.Models;

namespace SiteManager.CLIENT.ViewComponents.App
{
	public class BreadcrumbViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}

	}
}
