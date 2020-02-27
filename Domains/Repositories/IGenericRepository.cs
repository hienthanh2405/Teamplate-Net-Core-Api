using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domains.Repositories
{
    public interface IGenericRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        IQueryable<TEntity> DbSet { get; }
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> criteria);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> criteria);
        Task<TEntity> FirstAsync();
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> criteria);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> criteria);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(Expression<Func<TEntity, bool>> criteria);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteRangeAsync(Expression<Func<TEntity, bool>> criteria);
        Task SaveChangesAsync();
    }
}