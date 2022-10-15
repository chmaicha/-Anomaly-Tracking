using Shared.Core.Repository.Context;

namespace Shared.Core.Repository.UnitOfWork
{
    /// <summary>
    /// Interface that allows to modify the database in unit way.
    /// Unit Of Work Pattern
    /// </summary>
    public interface IBaseUnitOfWork
    {
        /// <summary>
        /// Serializes all the modifications on <code>ICoreDbContext</code> in one time.
        /// </summary>
        /// <returns>Status</returns>
        int Save();

        /// <summary>
        /// Begin transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commit transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Cancel transaction.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Overrides the actual <code>DbContext</code>.
        /// Uses for proceeding transaction at the application service level.
        /// </summary>
        /// <param name="context">Provided shared context</param>
        void SetContext(ICoreDbContext context);
    }
}