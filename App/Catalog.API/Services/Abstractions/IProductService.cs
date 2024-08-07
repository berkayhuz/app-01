using Catalog.LIB.DTOs.Product;
using Catalog.LIB.Entities;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(ProductCreateDto productCreateDto);
    Task<bool> UpdateProductAsync(Guid id, ProductUpdateDto productUpdateDto);
	Task<bool> DeleteProductAsync(Guid id);
	Task<bool> RestoreProductAsync(Guid id);
    Task<ProductDetailDto> GetProductBySlugAsync(string categoryPathWithSlug, string slug);
    Task<ProductDetailDto> GetProductByIdAsync(Guid id);

}