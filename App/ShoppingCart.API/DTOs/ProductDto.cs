namespace ShoppingCart.API.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountRate { get; set; }
        public string Category { get; set; }
        public string Slug { get; set; }
        public Guid? ImageId { get; set; }
        public DateTime Date { get; set; }
    }

}
