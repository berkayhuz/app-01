using Catalog.LIB.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SizeController : ControllerBase
	{
		private readonly ISizeService _sizeService;

		public SizeController(ISizeService sizeService)
		{
			_sizeService = sizeService;
		}

		[HttpPost]
		public async Task<IActionResult> CreateSize([FromBody] Size size)
		{
			var createdSize = await _sizeService.CreateSizeAsync(size.Name, size.Code);
			return Ok(createdSize);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateSize(Guid id, [FromBody] Size size)
		{
			var result = await _sizeService.UpdateSizeAsync(id, size.Name, size.Code);
			if (!result)
			{
				return NotFound();
			}

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSize(Guid id)
		{
			var result = await _sizeService.DeleteSizeAsync(id);
			if (!result)
			{
				return NotFound();
			}

			return Ok();
		}

		[HttpPost("restore/{id}")]
		public async Task<IActionResult> RestoreSize(Guid id)
		{
			var result = await _sizeService.RestoreSizeAsync(id);
			if (!result)
			{
				return NotFound();
			}

			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetSizes()
		{
			var sizes = await _sizeService.GetSizesAsync();
			return Ok(sizes);
		}
	}
}