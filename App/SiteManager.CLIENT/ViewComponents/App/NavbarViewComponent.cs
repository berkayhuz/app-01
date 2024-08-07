using Microsoft.AspNetCore.Mvc;

namespace SiteManager.CLIENT.ViewComponents.App
{
	public class NavbarViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View("/Views/Shared/Components/Navbar/Navbar.cshtml");
		}
	}
}