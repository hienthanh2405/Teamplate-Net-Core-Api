using Domains.Entities;
using System;

namespace Domains.Repositories
{
    public class Repository<TEntity> : GenericRepository<TEntity, Guid>, IRepository<TEntity>
        where TEntity : BaseEntity<Guid>
    {
        public Repository(ApplicationDBContext context) : base(context)
        {
        }
    }
}