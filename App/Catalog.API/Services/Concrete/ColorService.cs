using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.Data.UnitOfWorks;
using Catalog.LIB.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class ColorService : BaseService<Color>, IColorService
{
	public ColorService(
		IUnitOfWork unitOfWork,
		IMapper mapper,
		IHttpContextAccessor httpContextAccessor,
		ILogger<ColorService> logger)
		: base(unitOfWork, mapper, httpContextAccessor, logger)
	{
	}

	public async Task<Color> CreateColorAsync(string name, string code)
	{
		var color = new Color { Name = name, Code = code };
		await AddEntityAsync(color);
		return color;
	}

	public async Task<bool> UpdateColorAsync(Guid id, string name, string code)
	{
		var color = await GetEntityAsync(c => c.Id == id);
		if (color == null) return false;

		color.Name = name;
		color.Code = code;
		await UpdateEntityAsync(color);
		return true;
	}

	public async Task<bool> DeleteColorAsync(Guid id)
	{
		var color = await GetEntityAsync(c => c.Id == id);
		if (color == null) return false;

		color.IsDeleted = true;
		await UpdateEntityAsync(color);
		return true;
	}

	public async Task<bool> RestoreColorAsync(Guid id)
	{
		var color = await GetEntityAsync(c => c.Id == id);
		if (color == null) return false;

		color.IsDeleted = false;
		await UpdateEntityAsync(color);
		return true;
	}

	public async Task<List<Color>> GetColorsAsync()
	{
		return await GetEntitiesAsync<Color>(c => !c.IsDeleted);
	}
}