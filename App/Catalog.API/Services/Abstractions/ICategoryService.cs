using Catalog.API.Helpers;
using Catalog.LIB.DTOs.Category;
using Catalog.LIB.Entities;

namespace Catalog.API.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(string name, IFormFile imageFile, string createdBy, Guid? parentCategoryId);
        Task<bool> UpdateCategoryAsync(Guid id, CategoryUpdateDto categoryUpdateDto);
        Task<List<Category>> GetLeafCategoriesAsync();
        Task<bool> DeleteCategoryAsync(Guid categoryId);
        Task<bool> RestoreCategoryAsync(Guid categoryId);
        Task<List<CategoryListDto>> GetAllCategoriesAsync();

        Task<PaginatedList<CategoryWithProductsDto>> GetProductsByCategorySlugAsync(string slug, int pageIndex,
            int pageSize, string sortOrder);
        Task<Category> GetCategoryBySlugAsync(string slug);
        Task<Category> GetCategoryByIdAsync(Guid id);
    }
}