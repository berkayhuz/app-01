using Microsoft.AspNetCore.Mvc;

namespace SiteManager.CLIENT.ViewComponents.App
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
