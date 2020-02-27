using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Domains
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;
        private bool _disposed = false;

        public UnitOfWork(CompanyContext companyContext)
        {
            _dbContext = companyContext;
        }

        private DbConnection DbConnection
        {
            get => _dbContext.Database.GetDbConnection();
        }
        
        public virtual IDisposable BeginTransaction(IsolationLevel isolationLevel)
        {
            var transaction = DbTransaction;
            if (transaction != null)
            {
                throw new ApplicationException("Cannot begin a new transaction while an existing transaction is still running. " +
                                                "Please commit or rollback the existing transaction before starting a new one.");
            }

            OpenConnection();
            transaction = DbConnection.BeginTransaction(isolationLevel);
            _dbContext.Database.UseTransaction(transaction);
            DbTransaction = transaction;
            return transaction;
        }

        public virtual void CommitTransaction()
        {
            var transaction = DbTransaction;
            if (transaction == null)
            {
                throw new ApplicationException("Cannot roll back a transaction while there is no transaction running.");
            }

            try
            {
                _dbContext.SaveChanges();
                transaction.Commit();
                ReleaseTransaction(transaction);
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
        }

        public virtual void RollbackTransaction()
        {
            var transaction = DbTransaction;
            if (transaction == null)
            {
                throw new ApplicationException("Cannot roll back a transaction while there is no transaction running.");
            }

            transaction.Rollback();
            ReleaseTransaction(transaction);
        }

        public virtual void CommitChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var databaseValues = ex.Entries.FirstOrDefault().GetDatabaseValues().Properties.ToDictionary(x => x.Name, x => x.PropertyInfo);
            }
        }

        public virtual async Task CommitChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            _disposed = true;
        }

        private void OpenConnection()
        {
            if (DbConnection.State != System.Data.ConnectionState.Open)
            {
                DbConnection.Open();
            }
        }

        private DbTransaction DbTransaction { get; set; }

        /// <summary>
        /// Releases the current transaction
        /// </summary>
        private void ReleaseTransaction(DbTransaction transaction)
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
        }
    }
}