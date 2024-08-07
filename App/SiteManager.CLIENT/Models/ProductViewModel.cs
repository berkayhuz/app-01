namespace SiteManager.CLIENT.Models
{
	public class ProductViewModel
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
        public string CategoryPathWithName { get; set; }
        public string CategoryPathWithSlug { get; set; }
        public List<ColorViewModel> Colors { get; set; }
        public List<SizeViewModel> Sizes { get; set; }
    }
}
