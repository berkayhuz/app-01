using Catalog.LIB.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ColorController : ControllerBase
	{
		private readonly IColorService _colorService;

		public ColorController(IColorService colorService)
		{
			_colorService = colorService;
		}

		[HttpPost]
		public async Task<IActionResult> CreateColor([FromBody] Color color)
		{
			var createdColor = await _colorService.CreateColorAsync(color.Name, color.Code);
			return Ok(createdColor);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateColor(Guid id, [FromBody] Color color)
		{
			var result = await _colorService.UpdateColorAsync(id, color.Name, color.Code);
			if (!result)
			{
				return NotFound();
			}

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteColor(Guid id)
		{
			var result = await _colorService.DeleteColorAsync(id);
			if (!result)
			{
				return NotFound();
			}

			return Ok();
		}

		[HttpPost("restore/{id}")]
		public async Task<IActionResult> RestoreColor(Guid id)
		{
			var result = await _colorService.RestoreColorAsync(id);
			if (!result)
			{
				return NotFound();
			}

			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetColors()
		{
			var colors = await _colorService.GetColorsAsync();
			return Ok(colors);
		}
	}
}