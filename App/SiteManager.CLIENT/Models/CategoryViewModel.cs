namespace SiteManager.CLIENT.Models
{
	public class CategoryViewModel
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
		public string? ImageUrl { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public CategoryViewModel ParentCategory { get; set; }
        public List<CategoryViewModel> SubCategories { get; set; } = new List<CategoryViewModel>();
    }
}
