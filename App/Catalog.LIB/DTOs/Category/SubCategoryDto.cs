using Catalog.LIB.DTOs.Product;

namespace Catalog.LIB.DTOs.Category
{
    public class SubCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public List<ProductDto> Products { get; set; }
        public List<SubCategoryDto> SubCategories { get; set; }
        public string Path { get; set; }
    }
}