using Catalog.LIB.Entities;

namespace Catalog.LIB.DTOs.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ImageId { get; set; }
        public string CreatedBy { get; set; }
        public string Slug { get; set; }
        public DateTime Date { get; set; }
        public decimal DiscountRate { get; set; }
        public int InstallmentCount { get; set; }
        public string Details { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<ProductColorDto> ProductColors { get; set; }
        public ICollection<ProductSizeDto> ProductSizes { get; set; }
    }
}