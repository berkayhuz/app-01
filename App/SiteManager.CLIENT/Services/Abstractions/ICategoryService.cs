using SiteManager.CLIENT.Models;

public interface ICategoryService
{
    Task<List<CategoryViewModel>> GetCategoriesAsync();
    Task<CategoryViewModel> GetCategoryBySlugAsync(string slug);
    Task<CategoryViewModel> GetCategoryByIdAsync(Guid id);

    Task<PaginatedList<ProductViewModel>> GetProductsByCategorySlugAsync(string slug, int pageIndex, int pageSize,
        string sortOrder);

}