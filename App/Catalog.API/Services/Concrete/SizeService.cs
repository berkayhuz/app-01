using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.Data.UnitOfWorks;
using Catalog.LIB.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class SizeService : BaseService<Size>, ISizeService
{
	public SizeService(
		IUnitOfWork unitOfWork,
		IMapper mapper,
		IHttpContextAccessor httpContextAccessor,
		ILogger<SizeService> logger)
		: base(unitOfWork, mapper, httpContextAccessor, logger)
	{
	}

	public async Task<Size> CreateSizeAsync(string name, string code)
	{
		var size = new Size { Name = name, Code = code };
		await AddEntityAsync(size);
		return size;
	}

	public async Task<bool> UpdateSizeAsync(Guid id, string name, string code)
	{
		var size = await GetEntityAsync(s => s.Id == id);
		if (size == null) return false;

		size.Name = name;
		size.Code = code;
		await UpdateEntityAsync(size);
		return true;
	}

	public async Task<bool> DeleteSizeAsync(Guid id)
	{
		var size = await GetEntityAsync(s => s.Id == id);
		if (size == null) return false;

		size.IsDeleted = true;
		await UpdateEntityAsync(size);
		return true;
	}

	public async Task<bool> RestoreSizeAsync(Guid id)
	{
		var size = await GetEntityAsync(s => s.Id == id);
		if (size == null) return false;

		size.IsDeleted = false;
		await UpdateEntityAsync(size);
		return true;
	}

	public async Task<List<Size>> GetSizesAsync()
	{
		return await GetEntitiesAsync<Size>(s => !s.IsDeleted);
	}
}