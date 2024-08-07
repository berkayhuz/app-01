using SiteManager.CLIENT.Models;

public interface IProductService
{
    Task<PaginatedList<ProductViewModel>> GetProductsByCategorySlugAsync(string slug, int pageIndex, int pageSize, string sortOrder);
}