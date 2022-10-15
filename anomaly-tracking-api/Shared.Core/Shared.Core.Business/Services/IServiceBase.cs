namespace Shared.Core.Business.Services
{
    /// <summary>
    /// Common interface should be implemented by all business classes.
    /// </summary>
    public interface IServiceBase<TEntity, TUnitOfWork> where TEntity : class
    {
        /// <summary>
        /// Gets the total number of elements existing into database for the last executed with and provided filters.
        /// </summary>
        /// <returns>Total number of elements existing into database</returns>
        int? GetTotalCount();
    }
}