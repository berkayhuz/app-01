using Catalog.LIB.DTOs.Product;

namespace Catalog.LIB.DTOs.Category
{
    public class CategoryWithProductsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<ProductDto> Products { get; set; }
        public List<CategoryWithProductsDto> SubCategories { get; set; }
    }
}