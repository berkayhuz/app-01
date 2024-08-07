using Catalog.LIB.Entities;

namespace Catalog.LIB.DTOs.Product
{
    public class ProductDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountRate { get; set; }
        public int InstallmentCount { get; set; }
        public string Details { get; set; }
        public string ImageUrl { get; set; }
        public List<ProductColorDto> ProductColors { get; set; }
        public List<ProductSizeDto> ProductSizes { get; set; }
        public string CategoryPathWithName { get; set; }
        public string CategoryPathWithSlug { get; set; }
    }
}