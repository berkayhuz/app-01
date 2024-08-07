using System;
using System.Collections.Generic;

namespace Catalog.LIB.Entities
{
	public class Category : BaseEntity
	{
		public Category()
		{
			SubCategories = new List<Category>();
			Products = new List<Product>();
		}

		public Category(Guid imageId, string name, string createdBy, string slug, Category parentCategory = null)
			: this()
		{
			CreatedBy = createdBy;
			Name = name;
			Slug = slug;
			ImageId = imageId;
			ParentCategory = parentCategory;
			ParentCategoryId = parentCategory?.Id;
		}

		public string Name { get; set; }
		public string? Slug { get; set; }
		public Guid? ImageId { get; set; }
		public Image Image { get; set; }
		public ICollection<Product> Products { get; set; }
		public ICollection<Category> SubCategories { get; set; }
		public Category ParentCategory { get; set; }
		public Guid? ParentCategoryId { get; set; }
	}
}