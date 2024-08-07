using Microsoft.AspNetCore.Mvc;
using SiteManager.CLIENT.Models;

namespace SiteManager.CLIENT.ViewComponents.App
{
    public class NavListCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public NavListCategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var rootCategories = categories.Where(c => c.ParentCategoryId == null).ToList();
            BuildCategoryHierarchy(rootCategories, categories);
            return View("/Views/Shared/Components/Navbar/NavListCategory/NavListCategory.cshtml", rootCategories);
		}

        private void BuildCategoryHierarchy(List<CategoryViewModel> rootCategories, List<CategoryViewModel> allCategories)
        {
            foreach (var category in rootCategories)
            {
                var subCategories = allCategories.Where(c => c.ParentCategoryId == category.Id).ToList();
                category.SubCategories.AddRange(subCategories);
                BuildCategoryHierarchy(subCategories, allCategories);
            }
        }
    }
}