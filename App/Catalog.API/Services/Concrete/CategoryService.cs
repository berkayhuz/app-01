using AutoMapper;
using Catalog.API.Data.Repositories.Abstractions;
using Catalog.API.Data.UnitOfWorks;
using Catalog.API.Helpers;
using Catalog.API.Services.Abstractions;
using Catalog.LIB.DTOs.Category;
using Catalog.LIB.Entities;
using SharedLibrary.Entities.Enums;
using SharedLibrary.Helpers.Images;
using System.Linq.Expressions;

namespace Catalog.API.Services.Concrete
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly IRepository<Image> _imageRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IImageHelper _imageHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IImageHelper imageHelper,
            ILogger<CategoryService> logger)
            : base(unitOfWork, mapper, httpContextAccessor, logger)
        {
            _imageRepository = unitOfWork.GetRepository<Image>();
            _productRepository = unitOfWork.GetRepository<Product>();
            _imageHelper = imageHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        // CRUD
        public async Task<Category> CreateCategoryAsync(string name, IFormFile imageFile, string createdBy, Guid? parentCategoryId)
        {
            _logger.LogInformation("Creating category: {CategoryName}", name);

            var slug = SlugHelper.GenerateSlug(name);
            var imageUploadResult = await _imageHelper.UploadAsync(name, imageFile, ImageType.Category);

            var image = new Image
            {
                FileName = imageUploadResult.FullName,
                FileType = imageFile.ContentType,
                CreatedBy = createdBy
            };

            await _imageRepository.AddAsync(image);
            await _unitOfWork.SaveAsync();

            var category = new Category
            {
                Name = name,
                Slug = slug,
                CreatedBy = createdBy,
                ImageId = image.Id,
                ParentCategoryId = parentCategoryId
            };

            await _unitOfWork.GetRepository<Category>().AddAsync(category);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Category created: {CategoryName}", name);

            return category;
        }

        public async Task<bool> UpdateCategoryAsync(Guid id, CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetAsync(c => c.Id == id, c => c.Image);

            if (category == null)
            {
                _logger.LogWarning("Category not found: {CategoryId}", id);
                return false;
            }

            var slug = SlugHelper.GenerateSlug(categoryUpdateDto.Name ?? category.Name);

            if (categoryUpdateDto.ImageFile != null)
            {
                var imageUploadResult = await _imageHelper.UploadAsync(categoryUpdateDto.Name ?? category.Name, categoryUpdateDto.ImageFile, ImageType.Category);

                var image = new Image
                {
                    FileName = imageUploadResult.FullName,
                    FileType = categoryUpdateDto.ImageFile.ContentType,
                    CreatedBy = categoryUpdateDto.ModifiedBy ?? category.ModifiedBy
                };

                await _imageRepository.AddAsync(image);
                await _unitOfWork.SaveAsync();

                category.ImageId = image.Id;
            }

            UpdateCategoryDetails(category, categoryUpdateDto, slug);

            await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Category updated: {CategoryName}", category.Name);

            return true;
        }

        public async Task<List<Category>> GetLeafCategoriesAsync()
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();
            var leafCategories = categories.Where(c => !categories.Any(sub => sub.ParentCategoryId == c.Id)).ToList();
            return leafCategories;
        }

        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetAsync(c => c.Id == categoryId, c => c.SubCategories);

            if (category == null)
            {
                _logger.LogWarning("Category not found: {CategoryId}", categoryId);
                return false;
            }

            await MarkCategoryAsDeleted(category);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Category deleted: {CategoryName}", category.Name);

            return true;
        }

        private async Task MarkCategoryAsDeleted(Category category)
        {
            category.IsDeleted = true;
            await _unitOfWork.GetRepository<Category>().UpdateAsync(category);

            // Kategoriye bağlı ürünleri sil
            var products = await _unitOfWork.GetRepository<Product>().GetAllAsync(p => p.CategoryId == category.Id);
            foreach (var product in products)
            {
                product.IsDeleted = true;
                await _unitOfWork.GetRepository<Product>().UpdateAsync(product);
            }

            var subCategories = await _unitOfWork.GetRepository<Category>().GetAllAsync(c => c.ParentCategoryId == category.Id);
            foreach (var subCategory in subCategories)
            {
                await MarkCategoryAsDeleted(subCategory);
            }
        }

        public async Task<bool> RestoreCategoryAsync(Guid categoryId)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetAsync(c => c.Id == categoryId, c => c.ParentCategory);

            if (category == null)
            {
                _logger.LogWarning("Category not found: {CategoryId}", categoryId);
                return false;
            }

            await MarkCategoryAsRestored(category);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Category restored: {CategoryName}", category.Name);

            return true;
        }

        private async Task MarkCategoryAsRestored(Category category)
        {
            category.IsDeleted = false;
            await _unitOfWork.GetRepository<Category>().UpdateAsync(category);

            // Kategoriye bağlı ürünleri geri yükle
            var products = await _unitOfWork.GetRepository<Product>().GetAllAsync(p => p.CategoryId == category.Id && p.IsDeleted);
            foreach (var product in products)
            {
                product.IsDeleted = false;
                await _unitOfWork.GetRepository<Product>().UpdateAsync(product);
            }

            // Üst kategorileri restore et
            await RestoreParentCategories(category);
        }

        private async Task RestoreParentCategories(Category category)
        {
            var parentCategory = category.ParentCategory;
            while (parentCategory != null && parentCategory.IsDeleted)
            {
                parentCategory.IsDeleted = false;
                await _unitOfWork.GetRepository<Category>().UpdateAsync(parentCategory);
                parentCategory = await _unitOfWork.GetRepository<Category>().GetAsync(c => c.Id == parentCategory.ParentCategoryId, c => c.ParentCategory);
            }
        }

        private void UpdateCategoryDetails(Category category, CategoryUpdateDto categoryUpdateDto, string slug)
        {
            category.Name = categoryUpdateDto.Name ?? category.Name;
            category.Slug = slug;
            category.ModifiedBy = categoryUpdateDto.ModifiedBy ?? category.ModifiedBy;
            category.ParentCategoryId = categoryUpdateDto.ParentCategoryId ?? category.ParentCategoryId;
        }

        // CRUD

        public async Task<List<CategoryListDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(c => !c.IsDeleted, c => c.ParentCategory);
            var categoryListDtos = categories.Select(c => new CategoryListDto
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug,
                ParentCategoryId = c.ParentCategoryId,
                ParentCategoryName = c.ParentCategory?.Name
            }).ToList();

            return categoryListDtos;
        }

        public async Task<PaginatedList<CategoryWithProductsDto>> GetProductsByCategorySlugAsync(string slug, int pageIndex, int pageSize, string sortOrder)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetAsync(c => c.Slug == slug && !c.IsDeleted, c => c.SubCategories);
            if (category == null)
            {
                _logger.LogWarning($"Category with slug '{slug}' not found.");
                return null;
            }

            var categoryIds = new List<Guid>();
            GetAllCategoryIds(category, categoryIds);

            Expression<Func<Product, bool>> predicate = p => categoryIds.Contains(p.CategoryId) && !p.IsDeleted;
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = query =>
            {
                switch (sortOrder)
                {
                    case "price_asc":
                        return query.OrderBy(p => p.Price);
                    case "price_desc":
                        return query.OrderByDescending(p => p.Price);
                    case "name_asc":
                        return query.OrderBy(p => p.Name);
                    case "name_desc":
                        return query.OrderByDescending(p => p.Name);
                    case "newest":
                        return query.OrderByDescending(p => p.CreatedDate);
                    default:
                        return query.OrderBy(p => p.Id);
                }
            };

            var products = await _unitOfWork.GetRepository<Product>().GetAllPaginatedAsync(predicate, pageIndex, pageSize, orderBy);
            var productDtos = products.Items.Select(p => new CategoryWithProductsDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToList();

            return new PaginatedList<CategoryWithProductsDto>(productDtos, products.TotalCount, pageIndex, pageSize);
        }

        private void GetAllCategoryIds(Category category, List<Guid> categoryIds)
        {
            categoryIds.Add(category.Id);

            if (category.SubCategories != null)
            {
                foreach (var subCategory in category.SubCategories)
                {
                    GetAllCategoryIds(subCategory, categoryIds);
                }
            }
        }

        public async Task<Category> GetCategoryBySlugAsync(string slug)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetAsync(c => c.Slug == slug && !c.IsDeleted);
            if (category == null)
            {
                _logger.LogWarning("Category with slug '{Slug}' not found", slug);
                return null;
            }

            return category;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetAsync(c => c.Id == id && !c.IsDeleted);
            if (category == null)
            {
                _logger.LogWarning("Category with ID '{CategoryId}' not found", id);
                return null;
            }

            return category;
        }
    }
}
