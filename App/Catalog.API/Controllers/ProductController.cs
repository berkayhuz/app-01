using Catalog.API.Services.Abstractions;
using Catalog.LIB.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ICategoryService categoryService, IProductService productService, ILogger<ProductController> logger)
    {
        _categoryService = categoryService;
        _productService = productService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDto productCreateDto)
    {
        if (productCreateDto.ImageFile == null || productCreateDto.ImageFile.Length == 0)
            return BadRequest("Image file is required.");

        var product = await _productService.CreateProductAsync(productCreateDto);

        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] ProductUpdateDto productUpdateDto)
    {
        var result = await _productService.UpdateProductAsync(id, productUpdateDto);
        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var result = await _productService.DeleteProductAsync(id);
        if (!result) return NotFound();

        return Ok();
    }

    [HttpPost("restore/{id}")]
    public async Task<IActionResult> RestoreProduct(Guid id)
    {
        var result = await _productService.RestoreProductAsync(id);
        if (!result) return NotFound();

        return Ok();
    }
    [HttpGet("{categoryPathWithSlug}/{slug}")]
    public async Task<IActionResult> GetProductBySlug(string categoryPathWithSlug, string slug)
    {
        try
        {
            var product = await _productService.GetProductBySlugAsync(categoryPathWithSlug, slug);
            if (product == null)
            {
                return NotFound(new { Message = "Böyle bir ürün bulunamadı" });
            }

            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Sunucu hatası, lütfen daha sonra tekrar deneyin", Details = ex.Message });
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

}