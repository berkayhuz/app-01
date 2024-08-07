using AutoMapper;
using Catalog.API.Data.UnitOfWorks;
using Catalog.API.Helpers;
using Catalog.API.Services.Abstractions;
using Catalog.LIB.DTOs.Product;
using Catalog.LIB.Entities;
using SharedLibrary.Entities.Enums;
using SharedLibrary.Helpers.Images;

public class ProductService : BaseService<Product>, IProductService
{
    private readonly IImageHelper _imageHelper;
    private readonly ICategoryService _categoryService;

    public ProductService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICategoryService categoryService,
        IHttpContextAccessor httpContextAccessor,
        IImageHelper imageHelper,
        ILogger<ProductService> logger)
        : base(unitOfWork, mapper, httpContextAccessor, logger)
    {
        _imageHelper = imageHelper;
        _categoryService = categoryService;
    }

    public async Task<ProductDto> CreateProductAsync(ProductCreateDto productCreateDto)
    {

        var leafCategories = await _categoryService.GetLeafCategoriesAsync();
        if (!leafCategories.Any(c => c.Id == productCreateDto.CategoryId))
        {
            throw new ArgumentException("Selected category is not a leaf category");
        }

        var slug = SlugHelper.GenerateSlug(productCreateDto.Name);

        var imageUploadResult = await _imageHelper.UploadAsync(productCreateDto.Name, productCreateDto.ImageFile, ImageType.Product);

        var image = new Image
        {
            FileName = imageUploadResult.FullName,
            FileType = productCreateDto.ImageFile.ContentType,
            CreatedBy = productCreateDto.CreatedBy
        };

        await _unitOfWork.GetRepository<Image>().AddAsync(image);
        await _unitOfWork.SaveAsync();

        var product = new Product
        {
            CategoryId = productCreateDto.CategoryId,
            ImageId = image.Id,
            CreatedBy = productCreateDto.CreatedBy,
            Slug = slug,
            Date = DateTime.UtcNow,
            Name = productCreateDto.Name,
            Description = productCreateDto.Description,
            Price = productCreateDto.Price,
            DiscountRate = productCreateDto.DiscountRate,
            InstallmentCount = productCreateDto.InstallmentCount,
            Details = productCreateDto.Details,
            ProductColors = new List<ProductColor>(),
            ProductSizes = new List<ProductSize>()
        };

        await _unitOfWork.GetRepository<Product>().AddAsync(product);
        await _unitOfWork.SaveAsync();

        foreach (var colorId in productCreateDto.ColorIds)
        {
            var color = await _unitOfWork.GetRepository<Color>().GetByGuidAsync(colorId);
            if (color != null)
            {
                product.ProductColors.Add(new ProductColor
                {
                    Product = product,
                    Color = color,
                    Slug = $"{slug}-{color.Name.ToLower()}"
                });
            }
        }

        foreach (var sizeId in productCreateDto.Sizes)
        {
            var size = await _unitOfWork.GetRepository<Size>().GetByGuidAsync(sizeId);
            if (size != null)
            {
                product.ProductSizes.Add(new ProductSize
                {
                    Product = product,
                    Size = size,
                    Slug = $"{slug}-{size.Code}"
                });
            }
        }

        _unitOfWork.GetRepository<Product>().Update(product);
        await _unitOfWork.SaveAsync();

        var productDto = _mapper.Map<ProductDto>(product);
        return productDto;
    }


    public async Task<bool> UpdateProductAsync(Guid id, ProductUpdateDto productUpdateDto)
    {
        var product = await _unitOfWork.GetRepository<Product>()
            .GetAsync(p => p.Id == id, p => p.ProductColors, p => p.ProductSizes);

        if (product == null) return false;

        var slug = SlugHelper.GenerateSlug(productUpdateDto.Name ?? product.Name);

        if (productUpdateDto.ImageFile != null)
        {
            var imageUploadResult = await _imageHelper.UploadAsync(productUpdateDto.Name ?? product.Name, productUpdateDto.ImageFile, ImageType.Product);

            var image = new Image
            {
                FileName = imageUploadResult.FullName,
                FileType = productUpdateDto.ImageFile.ContentType,
                CreatedBy = productUpdateDto.ModifiedBy ?? product.ModifiedBy
            };

            await _unitOfWork.GetRepository<Image>().AddAsync(image);
            await _unitOfWork.SaveAsync();

            product.ImageId = image.Id;
        }

        product.Name = productUpdateDto.Name ?? product.Name;
        product.Slug = slug;
        product.ModifiedBy = productUpdateDto.ModifiedBy ?? product.ModifiedBy;
        product.CategoryId = productUpdateDto.CategoryId ?? product.CategoryId;
        product.Description = productUpdateDto.Description ?? product.Description;
        product.Price = productUpdateDto.Price ?? product.Price;
        product.DiscountRate = productUpdateDto.DiscountRate ?? product.DiscountRate;
        product.InstallmentCount = productUpdateDto.InstallmentCount ?? product.InstallmentCount;
        product.Details = productUpdateDto.Details ?? product.Details;
        product.Date = DateTime.UtcNow;

        if (productUpdateDto.ColorIds != null && productUpdateDto.ColorIds.Any())
        {
            product.ProductColors.Clear();
            foreach (var colorId in productUpdateDto.ColorIds)
            {
                var color = await _unitOfWork.GetRepository<Color>().GetByGuidAsync(colorId);
                if (color != null)
                {
                    product.ProductColors.Add(new ProductColor
                    {
                        Product = product,
                        Color = color,
                        Slug = $"{slug}-{color.Name.ToLower()}"
                    });
                }
            }
        }

        if (productUpdateDto.Sizes != null && productUpdateDto.Sizes.Any())
        {
            product.ProductSizes.Clear();
            foreach (var sizeId in productUpdateDto.Sizes)
            {
                var size = await _unitOfWork.GetRepository<Size>().GetByGuidAsync(sizeId);
                if (size != null)
                {
                    product.ProductSizes.Add(new ProductSize
                    {
                        Product = product,
                        Size = size,
                        Slug = $"{slug}-{size.Code}"
                    });
                }
            }
        }

        _unitOfWork.GetRepository<Product>().Update(product);
        await _unitOfWork.SaveAsync();

        return true;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var product = await _unitOfWork.GetRepository<Product>().GetByGuidAsync(id);
        if (product == null) return false;

        product.IsDeleted = true;
        _unitOfWork.GetRepository<Product>().Update(product);
        await _unitOfWork.SaveAsync();

        return true;
    }

    public async Task<bool> RestoreProductAsync(Guid id)
    {
        var product = await _unitOfWork.GetRepository<Product>().GetByGuidAsync(id);
        if (product == null) return false;

        product.IsDeleted = false;
        _unitOfWork.GetRepository<Product>().Update(product);
        await _unitOfWork.SaveAsync();

        return true;
    }

	public async Task<ProductDetailDto> GetProductBySlugAsync(string categoryPathWithSlugParam, string slug)
	{
		try
		{
			var product = await _unitOfWork.GetRepository<Product>()
				.GetAsync(p => p.Slug == slug && !p.IsDeleted, p => p.Image, p => p.ProductColors, p => p.ProductSizes, p => p.Category);

			if (product == null) return null;

			var categoryPathWithName = await GetFullCategoryPathWithNameAsync(product.Category);
			var categoryPathWithSlugLocal = await GetFullCategoryPathWithSlugAsync(product.Category);

			var productDetailDto = new ProductDetailDto
	
			{
				Id = product.Id,
				Name = product.Name,
				Slug = product.Slug,
				Description = product.Description,
				Price = product.Price,
				DiscountRate = product.DiscountRate,
				InstallmentCount = product.InstallmentCount,
				Details = product.Details,
				ImageUrl = product.Image?.FileName,
				CategoryPathWithName = categoryPathWithName,
				CategoryPathWithSlug = categoryPathWithSlugLocal,
				ProductColors = product.ProductColors.Select(pc => new ProductColorDto
				{
                    ColorId = pc.ColorId,
                    ColorName = pc.Color.Name,
                    Slug = pc.Slug
                }).ToList(),
                ProductSizes = product.ProductSizes.Select(ps => new ProductSizeDto
                {
                    SizeId = ps.SizeId,
                    SizeCode = ps.Size.Code,
                    Slug = ps.Slug
                }).ToList()
            };

            return productDetailDto;
        }
        catch (Exception ex)
        {
            throw new Exception("Ürün getirilirken bir hata oluştu", ex);
        }
    }


    private async Task<string> GetFullCategoryPathWithNameAsync(Category category)
    {
        if (category == null) return string.Empty;

        var categories = new List<string>();
        while (category != null)
        {
            categories.Insert(0, category.Name);
            if (category.ParentCategoryId.HasValue)
            {
                category = await _unitOfWork.GetRepository<Category>()
                    .GetAsync(c => c.Id == category.ParentCategoryId);
            }
            else
            {
                category = null;
            }
        }

        return string.Join("/", categories);
    }
    private async Task<string> GetFullCategoryPathWithSlugAsync(Category category)
    {
	    if (category == null) return string.Empty;

	    var categories = new List<string>();
	    while (category != null)
	    {
		    categories.Insert(0, category.Slug);
		    if (category.ParentCategoryId.HasValue)
		    {
			    category = await _unitOfWork.GetRepository<Category>()
				    .GetAsync(c => c.Id == category.ParentCategoryId);
		    }
		    else
		    {
			    category = null;
		    }
	    }

	    return string.Join("/", categories);
    }
    public async Task<ProductDetailDto> GetProductByIdAsync(Guid id)
    {
        try
        {
            var product = await _unitOfWork.GetRepository<Product>()
                .GetAsync(p => p.Id == id && !p.IsDeleted, p => p.Image, p => p.ProductColors, p => p.ProductSizes, p => p.Category);

            if (product == null) return null;

            var categoryPathWithName = await GetFullCategoryPathWithNameAsync(product.Category);
            var categoryPathWithSlugLocal = await GetFullCategoryPathWithSlugAsync(product.Category);

            var productDetailDto = new ProductDetailDto
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,
                Price = product.Price,
                DiscountRate = product.DiscountRate,
                InstallmentCount = product.InstallmentCount,
                Details = product.Details,
                ImageUrl = product.Image?.FileName,
                CategoryPathWithName = categoryPathWithName,
                CategoryPathWithSlug = categoryPathWithSlugLocal,
                ProductColors = product.ProductColors.Select(pc => new ProductColorDto
                {
                    ColorId = pc.ColorId,
                    ColorName = pc.Color.Name,
                    Slug = pc.Slug
                }).ToList(),
                ProductSizes = product.ProductSizes.Select(ps => new ProductSizeDto
                {
                    SizeId = ps.SizeId,
                    SizeCode = ps.Size.Code,
                    Slug = ps.Slug
                }).ToList()
            };

            return productDetailDto;
        }
        catch (Exception ex)
        {
            throw new Exception("Ürün getirilirken bir hata oluştu", ex);
        }
    }






}
