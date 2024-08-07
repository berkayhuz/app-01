using Catalog.LIB.DTOs.Product;

namespace Catalog.LIB.DTOs.Category
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string ImageUrl { get; set; }
        public List<SubCategoryDto> SubCategories { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}