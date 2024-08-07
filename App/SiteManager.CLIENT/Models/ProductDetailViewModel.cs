using System;
using System.Collections.Generic;

namespace SiteManager.CLIENT.Models
{
    public class ProductDetailViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string DiscountRate { get; set; }
        public string Slug { get; set; }
        public DateTime Date { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ImageId { get; set; }
        public string CreatedBy { get; set; }
        public int InstallmentCount { get; set; }
        public string Details { get; set; }
        public string CategoryHierarchy { get; set; }
        public List<ProductColorViewModel> ProductColors { get; set; }
        public List<ProductSizeViewModel> ProductSizes { get; set; }
    }

    public class ProductColorViewModel
    {
        public Guid ColorId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class ProductSizeViewModel
    {
        public Guid SizeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}