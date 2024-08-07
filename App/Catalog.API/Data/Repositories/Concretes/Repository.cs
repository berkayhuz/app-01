using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Catalog.API.Data.Repositories.Abstractions;
using Catalog.API.Helpers;
using Catalog.LIB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : class, IBaseEntity, new()
    {
        private readonly CatalogDbContext _dbContext;

        public Repository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        private DbSet<T> Table => _dbContext.Set<T>();

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;

            if (predicate != null) query = query.Where(predicate);

            foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public IQueryable<T> GetAll()
        {
            return Table.AsQueryable();
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await Table.AddAsync(entity).ConfigureAwait(false);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var query = Table.Where(predicate);

            foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);

            return await query.SingleOrDefaultAsync().ConfigureAwait(false);
        }

        public T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var query = Table.Where(predicate);

            foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);

            return query.SingleOrDefault();
        }

        public async Task<T> GetByGuidAsync(Guid id)
        {
            return await Table.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<PaginatedList<T>> GetAllPaginatedAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = Table;

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var count = await query.CountAsync().ConfigureAwait(false);
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Table.Update(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Table.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return await Table.FirstOrDefaultAsync(predicate).ConfigureAwait(false);
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.IsDeleted = true;
            Table.Update(entity);
        }
    }
}
