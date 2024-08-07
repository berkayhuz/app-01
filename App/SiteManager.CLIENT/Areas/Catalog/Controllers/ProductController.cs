using Microsoft.AspNetCore.Mvc;

public class ProductController : Controller
{
	private readonly IProductService _productService;

	public ProductController(IProductService productService)
	{
		_productService = productService;
	}

	
}
