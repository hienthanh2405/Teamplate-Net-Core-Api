using Domains.Entities;
using System;

namespace Domains.Repositories
{
    public interface IRepository<TEntity> : IGenericRepository<TEntity, Guid>
        where TEntity : BaseEntity<Guid>
    {
    }
}