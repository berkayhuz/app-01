using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.LIB.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            ProductColors = new List<ProductColor>();
            ProductSizes = new List<ProductSize>();
        }

        public Product(Guid categoryId,
            Guid? imageId, string createdBy,
            string slug, DateTime date,
            string name, string description,
            decimal price, decimal discountRate,
            int installmentCount, string details)
        {
            CategoryId = categoryId;
            ImageId = imageId;
            CreatedBy = createdBy;
            Slug = slug;
            Date = date;
            Name = name;
            Description = description;
            Price = price;
            DiscountRate = discountRate;
            InstallmentCount = installmentCount;
            Details = details;
            ProductColors = new List<ProductColor>();
            ProductSizes = new List<ProductSize>();
        }

        public DateTime Date { get; set; }
        public string Slug { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid? ImageId { get; set; }
        public Image Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal DiscountRate { get; set; }
        public int InstallmentCount { get; set; }
        public string Details { get; set; }

        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
    }
}