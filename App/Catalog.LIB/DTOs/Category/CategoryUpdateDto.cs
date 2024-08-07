using Microsoft.AspNetCore.Http;

namespace Catalog.LIB.DTOs.Category
{
    public class CategoryUpdateDto
    {
        public string? Name { get; set; }
        public string? ModifiedBy { get; set; }
        public IFormFile? ImageFile { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}