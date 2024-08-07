using Catalog.API.Data.Repositories.Abstractions;
using Catalog.API.Data.Repositories.Concretes;
using Catalog.LIB.Entities;

namespace Catalog.API.Data.UnitOfWorks
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly CatalogDbContext _dbContext;

		public UnitOfWork(CatalogDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async ValueTask DisposeAsync()
		{
			await _dbContext.DisposeAsync();
		}

		public int Save()
		{
			return _dbContext.SaveChanges();
		}

		public async Task<int> SaveAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}

		public IRepository<T> GetRepository<T>() where T : class, IBaseEntity, new()
		{
			return new Repository<T>(_dbContext);
		}
	}
}