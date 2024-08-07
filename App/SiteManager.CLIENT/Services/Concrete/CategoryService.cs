using SiteManager.CLIENT.Models;

public class CategoryService : ICategoryService
{
	private readonly HttpClient _httpClient;

	public CategoryService(IHttpClientFactory httpClientFactory)
	{
		_httpClient = httpClientFactory.CreateClient("CatalogAPI");
	}
	public async Task<List<CategoryViewModel>> GetCategoriesAsync()
	{
		return await _httpClient.GetFromJsonAsync<List<CategoryViewModel>>("category");
	}
    public async Task<CategoryViewModel> GetCategoryBySlugAsync(string slug)
    {
        return await _httpClient.GetFromJsonAsync<CategoryViewModel>($"category/slug/{slug}");
    }
    public async Task<CategoryViewModel> GetCategoryByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<CategoryViewModel>($"category/{id}");
    }
    public async Task<PaginatedList<ProductViewModel>> GetProductsByCategorySlugAsync(string slug, int pageIndex, int pageSize, string sortOrder)
    {
        var response = await _httpClient.GetFromJsonAsync<PaginatedList<ProductViewModel>>(
            $"category/products/slug/{slug}?pageIndex={pageIndex}&pageSize={pageSize}&sortOrder={sortOrder}");
        return response;
    }

}
