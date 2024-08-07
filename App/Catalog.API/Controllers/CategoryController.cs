using Catalog.API.Helpers;
using Catalog.API.Services.Abstractions;
using Catalog.LIB.DTOs.Category;
using Catalog.LIB.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromForm] CategoryCreateDto categoryCreateDto)
    {
        if (categoryCreateDto.ImageFile == null || categoryCreateDto.ImageFile.Length == 0)
        {
            _logger.LogWarning("Image file is required");
            return BadRequest("Image file is required.");
        }

        var category = await _categoryService.CreateCategoryAsync(
            categoryCreateDto.Name,
            categoryCreateDto.ImageFile,
            categoryCreateDto.CreatedBy,
            categoryCreateDto.ParentCategoryId
        );

        return Ok(category);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);
        if (!result) return NotFound();

        return Ok();
    }

    [HttpPost("restore/{id}")]
    public async Task<IActionResult> RestoreCategory(Guid id)
    {
        var result = await _categoryService.RestoreCategoryAsync(id);
        if (!result) return NotFound();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromForm] CategoryUpdateDto categoryUpdateDto)
    {
        var result = await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto);
        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpGet("leaf-categories")]
    public async Task<IActionResult> GetLeafCategories()
    {
        var categories = await _categoryService.GetLeafCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("products/slug/{slug}")]
    public async Task<IActionResult> GetProductsByCategorySlug(string slug, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 24, [FromQuery] string sortOrder = "best")
    {
        var products = await _categoryService.GetProductsByCategorySlugAsync(slug, pageIndex, pageSize, sortOrder);
        if (products == null)
        {
            return NotFound();
        }
        return Ok(products);
    }
    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetCategoryBySlug(string slug)
    {
        var category = await _categoryService.GetCategoryBySlugAsync(slug);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }
}
