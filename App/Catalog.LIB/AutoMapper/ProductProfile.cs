using AutoMapper;
using Catalog.LIB.DTOs.Product;
using Catalog.LIB.Entities;
using System.Linq;

namespace Catalog.LIB.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src => src.ProductColors))
                .ForMember(dest => dest.ProductSizes, opt => opt.MapFrom(src => src.ProductSizes))
                .ReverseMap();

            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<ProductDetailDto, Product>().ReverseMap();
        }

        private string GetCategoryPath(Category category)
        {
            if (category == null) return string.Empty;

            var path = category.Name;
            while (category.ParentCategory != null)
            {
                category = category.ParentCategory;
                path = category.Name + " > " + path;
            }

            return path;
        }
    }
}
