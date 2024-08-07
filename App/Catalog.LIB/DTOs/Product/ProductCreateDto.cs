using Microsoft.AspNetCore.Http;

namespace Catalog.LIB.DTOs.Product
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public IFormFile ImageFile { get; set; }
        public Guid CategoryId { get; set; }
        public List<Guid> ColorIds { get; set; }
        public List<Guid> Sizes { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountRate { get; set; }
        public int InstallmentCount { get; set; }
        public string Details { get; set; }
    }
}