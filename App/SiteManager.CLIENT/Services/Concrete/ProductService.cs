using SiteManager.CLIENT.Models;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CatalogAPI");
    }

    public async Task<PaginatedList<ProductViewModel>> GetProductsByCategorySlugAsync(string slug, int pageIndex, int pageSize, string sortOrder)
    {
        var response = await _httpClient.GetFromJsonAsync<PaginatedList<ProductViewModel>>(
            $"category/products/slug/{slug}?pageIndex={pageIndex}&pageSize={pageSize}&sortOrder={sortOrder}");
        return response;
    }
}
