using Microsoft.AspNetCore.Http;

namespace Catalog.LIB.DTOs.Category
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public IFormFile ImageFile { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}