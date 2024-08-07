using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.Data.UnitOfWorks;
using Catalog.LIB.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public abstract class BaseService<TEntity> where TEntity : class, IBaseEntity, new()
{
	protected readonly IUnitOfWork _unitOfWork;
	protected readonly IMapper _mapper;
	protected readonly IHttpContextAccessor _httpContextAccessor;
	protected readonly ILogger _logger;
	protected readonly ClaimsPrincipal _user;

	protected BaseService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger logger)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
		_logger = logger;
		_user = httpContextAccessor.HttpContext.User;
	}

	protected string GetLoggedInEmail()
	{
		return _user.FindFirstValue(ClaimTypes.Email);
	}

	protected async Task<List<TDto>> GetEntitiesAsync<TDto>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
	{
		var entities = await _unitOfWork.GetRepository<TEntity>().GetAllAsync(predicate, includeProperties);
		return _mapper.Map<List<TDto>>(entities);
	}

	protected async Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
	{
		return await _unitOfWork.GetRepository<TEntity>().GetAsync(predicate, includeProperties);
	}

	protected async Task AddEntityAsync(TEntity entity)
	{
		await _unitOfWork.GetRepository<TEntity>().AddAsync(entity);
		await _unitOfWork.SaveAsync();
	}

	protected async Task UpdateEntityAsync(TEntity entity)
	{
		await _unitOfWork.GetRepository<TEntity>().UpdateAsync(entity);
		await _unitOfWork.SaveAsync();
	}

	protected async Task DeleteEntityAsync(TEntity entity)
	{
		_unitOfWork.GetRepository<TEntity>().Delete(entity);
		await _unitOfWork.SaveAsync();
	}
}
