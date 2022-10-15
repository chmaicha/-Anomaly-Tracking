using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Processs
{
    /// <summary>
    /// Interface that manages process operations.
    /// </summary>
    public interface IServiceProcess : IServiceBase<ProcessDb, IAnomalyTrackingUnitOfWork>, IServiceContext<IAnomalyTrackingUnitOfWork>
    {
        /// <summary>
        /// Create the given process
        /// </summary>
        /// <param name="processDb">Process to create</param>
        /// <returns>Create a new process</returns>
        ProcessDb Create(ProcessDb processDb);
        /// <summary>
        /// Update the given process
        /// </summary>
        /// <param name="id"></param>
        /// <param name="processDb"> Process to update</param>
        /// <returns>Modified process info</returns>
        ProcessDb Update(ProcessDb processDb, bool commit = true);

        /// <summary>
        /// Get specific process by id
        /// </summary>
        /// <param name="id"> process on retrieved data </param>
        /// <returns> get the existant process</returns>
        ProcessDb Get(int id, string includeProperties = "");

        /// <summary>
        ///  Delete specific process by id
        /// </summary>
        /// <param name="id">identifier of the process to delete</param>
        /// <returns> Deleted process identifier</returns>
        int Delete(int id);

        /// <summary>
        /// Get all process
        /// </summary>
        /// <returns> get all existant process </returns>
        IEnumerable<ProcessDb> GetAll(Expression<Func<ProcessDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true);
    }
}
