using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domains.Repositories
{
    public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        #region Constructor
        public GenericRepository(DbContext context)
        {
            DbContext = context;
            DbSet = context.Set<TEntity>();
        }
        #endregion

        #region Properties
        public DbContext DbContext { get; }
        public IQueryable<TEntity> DbSet { get; }
        #endregion

        #region Count
        public async Task<int> CountAsync()
            => await DbSet.CountAsync();

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria)
            => await DbSet.CountAsync(criteria);
        #endregion

        #region GetAll
        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await DbSet.ToListAsync();
        #endregion

        #region Find
        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> criteria)
            => await DbSet.FirstOrDefaultAsync(criteria);

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> criteria)
            => await DbSet.Where(criteria).ToListAsync();
        #endregion

        #region First
        public async Task<TEntity> FirstAsync()
            => await DbSet.FirstAsync();

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> criteria)
            => await DbSet.FirstAsync(criteria);
        #endregion

        #region Add
        public async Task AddAsync(TEntity entity)
        {
            await DbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbContext.Set<TEntity>().AddRangeAsync(entities);
        }
        #endregion

        #region Update
        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            });
        }

        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> criteria)
        {
            var original = await FindOneAsync(criteria);
            await Task.Run(() =>
            {
                DbContext.Entry(original).CurrentValues.SetValues(entity);
            });
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() =>
            {
                DbContext.UpdateRange(entities);
            });
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                DbContext.Set<TEntity>().Remove(entity);
            });
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> criteria)
        {
            var entity = await DbSet.FirstAsync(criteria);
            await Task.Run(() =>
            {
                DbContext.Set<TEntity>().Remove(entity);
            });
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() =>
            {
                DbContext.RemoveRange(entities);
            });
        }

        public async Task DeleteRangeAsync(Expression<Func<TEntity, bool>> criteria)
        {
            IEnumerable<TEntity> entities = DbSet.Where(criteria);
            foreach (TEntity entity in entities)
            {
                await DeleteAsync(entity);
            }
        }
        #endregion

        #region SaveChanges
        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
        #endregion
    }
}