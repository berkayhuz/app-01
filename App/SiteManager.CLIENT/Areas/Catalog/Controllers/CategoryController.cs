using Microsoft.AspNetCore.Mvc;
using SiteManager.CLIENT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Area("Catalog")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public CategoryController(ICategoryService categoryService, IProductService productService)
    {
        _categoryService = categoryService;
        _productService = productService;
    }
    [Route("{slug}")]
    public async Task<IActionResult> Products(string slug, int pageIndex = 1, int pageSize = 24, string sortOrder = "best")
    {
        if (pageSize == 0)
        {
            pageSize = 24;
        }

        var category = await _categoryService.GetCategoryBySlugAsync(slug);
        if (category == null)
        {
            return NotFound();
        }

        var products = await _productService.GetProductsByCategorySlugAsync(slug, pageIndex, pageSize, sortOrder);

        var breadcrumb = await GetBreadcrumbAsync(category);

        ViewBag.Slug = slug;
        ViewBag.SortOrder = sortOrder;
        ViewBag.TotalProducts = products.TotalCount;
        ViewBag.CategoryName = category.Name;
        ViewBag.Breadcrumb = breadcrumb;

        return View(products);
    }

    private async Task<List<BreadcrumbViewModel>> GetBreadcrumbAsync(CategoryViewModel category)
    {
        var breadcrumb = new List<BreadcrumbViewModel>();
        while (category != null)
        {
            breadcrumb.Insert(0, new BreadcrumbViewModel { Name = category.Name, Slug = category.Slug });
            if (category.ParentCategoryId != null)
            {
                category = await _categoryService.GetCategoryByIdAsync(category.ParentCategoryId.Value);
            }
            else
            {
                category = null;
            }
        }

        breadcrumb.Insert(0, new BreadcrumbViewModel { Name = "Ana sayfa", Slug = "" });
        return breadcrumb;
    }
}
