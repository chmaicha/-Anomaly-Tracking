using System.Data.Entity;

namespace Shared.Core.Repository.Context
{
    /// <summary>
    /// Provides a decorator for the entity framework implementation <code>DbContext</code>.
    /// </summary>
    public class CoreDbContext : DbContext, ICoreDbContext
    {
        private DbContextTransaction transaction;

        /// <summary>
        /// Create a new instance of <code>CoreDbContext</code>.
        /// </summary>
        /// <param name="nameOrConnectionString">Name or connection string</param>
        public CoreDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        /// </inheritdoc>
        public Database DbInstance()
        {
            return this.Database;
        }

        /// </inheritdoc>
        public IDbSet<T> GetIDbSet<T>() where T : class
        {
            return this.Set<T>();
        }

        /// </inheritdoc>
        public void SetModified(object entity)
        {
            this.Entry(entity).State = EntityState.Modified;
        }

        /// </inheritdoc>
        public bool IsInState(object entity, EntityState state)
        {
            return this.Entry(entity).State == state;
        }

        /// </inheritdoc>
        public void BeginTransaction()
        {
            this.transaction = this.Database.BeginTransaction();
        }

        /// </inheritdoc>
        public void CommitTransaction()
        {
            this.transaction.Commit();
        }

        /// </inheritdoc>
        public void RollbackTransaction()
        {
            this.transaction.Rollback();
        }
    }
}