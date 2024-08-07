namespace Catalog.LIB.Entities
{
	public class ProductColor
	{
		public Guid ProductId { get; set; }
		public Product Product { get; set; }

		public Guid ColorId { get; set; }
		public Color Color { get; set; }
		public bool IsDeleted { get; set; }
		public string Slug { get; set; }
	}
}