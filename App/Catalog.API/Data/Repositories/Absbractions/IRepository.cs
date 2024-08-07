using Catalog.API.Helpers;
using Catalog.LIB.Entities;
using System.Linq.Expressions;

namespace Catalog.API.Data.Repositories.Abstractions
{
	public interface IRepository<T> where T : class, IBaseEntity, new()
	{
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
		Task AddAsync(T entity);
		Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
		T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
		Task<T> GetByGuidAsync(Guid id);
		Task<T> UpdateAsync(T entity);
		T Update(T entity);
		Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
		void Delete(T entity);
        IQueryable<T> GetAll();

        Task<PaginatedList<T>> GetAllPaginatedAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

    }
}