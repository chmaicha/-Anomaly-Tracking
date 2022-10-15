using Shared.Core.Repository.Context;
using System;

namespace Shared.Core.Repository.UnitOfWork
{
    /// <summary>
    /// Implementation of <code>IBaseUnitOfWork</code> interface.
    /// </summary>
    public class BaseUnitOfWork : IBaseUnitOfWork, IDisposable
    {
        private bool disposed = false;
        protected ICoreDbContext context;

        /// <summary>
        /// Create a new instance of <code>BaseUnitOfWork</code>.
        /// </summary>
        /// <param name="context">Database context to use</param>
        public BaseUnitOfWork(ICoreDbContext context)
        {
            this.context = context;
        }

        /// </inheritdoc>
        public int Save()
        {
            return context.SaveChanges();
        }

        /// </inheritdoc>
        public void BeginTransaction()
        {
            context.BeginTransaction();
        }

        /// </inheritdoc>
        public void CommitTransaction()
        {
            context.SaveChanges();
            context.CommitTransaction();
        }

        /// </inheritdoc>
        public void RollbackTransaction()
        {
            context.RollbackTransaction();
        }

        /// </inheritdoc>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// </inheritdoc>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// </inheritdoc>
        public void SetContext(ICoreDbContext context)
        {
            this.context = context;
        }
    }
}