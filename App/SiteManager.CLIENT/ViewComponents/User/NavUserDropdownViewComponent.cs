using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiteManager.CLIENT.ViewComponents.User
{
	[Authorize]
	public class NavUserDropdownViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("/Views/Shared/Components/Navbar/NavUserDropdown/NavUserDropdown.cshtml");
		}
	}
}