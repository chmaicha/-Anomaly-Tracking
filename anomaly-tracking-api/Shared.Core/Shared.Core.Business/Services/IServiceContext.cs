namespace Shared.Core.Business.Services
{
    /// <summary>
    /// Interface that exposes support to define a given instance of unit of work.
    /// This is specially used when it is mandatory to make a transaction for multiple services.
    /// </summary>
    public interface IServiceContext<TUnitOfWork>
    {
        /// <summary>
        /// Set a unit of work to use for request.
        /// </summary>
        /// <param name="unitOfWork">Unit of work to use</param>
        void SetContext(TUnitOfWork unitOfWork);
    }
}