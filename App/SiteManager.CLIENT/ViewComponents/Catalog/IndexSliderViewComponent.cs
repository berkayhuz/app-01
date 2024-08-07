using Microsoft.AspNetCore.Mvc;

namespace SiteManager.CLIENT.ViewComponents.Catalog
{
    public class IndexSliderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/Shared/Components/Catalog/Sliders/IndexSlider.cshtml");
        }
    }
}
