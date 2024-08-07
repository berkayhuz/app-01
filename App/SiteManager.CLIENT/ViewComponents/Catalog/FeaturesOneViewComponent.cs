using Microsoft.AspNetCore.Mvc;

namespace SiteManager.CLIENT.ViewComponents.Catalog
{
	public class FeaturesOneViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("/Views/Shared/Components/Catalog/Features/One.cshtml");
		}
	}
}