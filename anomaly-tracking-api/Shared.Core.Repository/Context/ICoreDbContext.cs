using System.Data.Entity;

namespace Shared.Core.Repository.Context
{
    /// <summary>
    /// Interface that encapsulates the entiy framework implementation.
    /// It allows to make unit testing easier and hide the fact that EF is used to be able to upgrade that in the future.
    /// </summary>
    public interface ICoreDbContext
    {
        /// <summary>
        /// Gets an entity framework <code>IDbSet</code> instance.
        /// </summary>
        /// <typeparam name="T"><code>IDbSet</code> type</typeparam>
        /// <returns><code>IDbSet</code> instance</returns>
        IDbSet<T> GetIDbSet<T>() where T : class;

        /// <summary>
        /// Modifies an entity instance status.
        /// </summary>
        /// <param name="entity">Entity to modify</param>
        void SetModified(object entity);

        /// <summary>
        /// Indicates whether or not the given entity has the provided status.
        /// </summary>k
        /// <param name="entity">Entity to check</param>
        /// <param name="state">Status to check</param>
        /// <returns>TRUE if the entity has the status, FALSE otherwise</returns>
        bool IsInState(object entity, EntityState state);

        /// <summary>
        /// Gets the data base instance.
        /// </summary>
        /// <returns>Database instance</returns>
        Database DbInstance();

        /// <summary>
        /// Serializes the <code>ICoreDbContext</code> changes.
        /// </summary>
        /// <returns>Le nombre d'entrées modifiées en base de données</returns>
        int SaveChanges();

        /// <summary>
        /// Calls by GC to clean memory.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Begins the transaction accross all the business services.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits the current transaction accross all the business services.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rollbacks the transaction accross all the business services.
        /// </summary>
        void RollbackTransaction();
    }
}