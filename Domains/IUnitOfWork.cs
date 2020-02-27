using System;
using System.Data;
using System.Threading.Tasks;

namespace Domains
{
    public interface IUnitOfWork
    {
        IDisposable BeginTransaction(IsolationLevel isolationLevel);
        void CommitTransaction();
        void RollbackTransaction();
        void CommitChanges();
        Task CommitChangesAsync();
        void Dispose();
    }
}