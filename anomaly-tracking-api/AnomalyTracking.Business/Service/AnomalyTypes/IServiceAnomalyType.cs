using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.AnomalyTypes
{
    /// <summary>
    /// Interface that manages anomalyType operations.
    /// </summary>
    public interface IServiceAnomalyType : IServiceBase<AnomalyTypeDb, IAnomalyTrackingUnitOfWork>, IServiceContext<IAnomalyTrackingUnitOfWork>
    {
        /// <summary>
        /// Create the given anomalyType
        /// </summary>
        /// <param name="anomalyTypeDb">AnomalyType to create</param>
        /// <returns>Create a new anomalyType</returns>
        AnomalyTypeDb Create(AnomalyTypeDb anomalyTypeDb);
        /// <summary>
        /// Update the given anomalyType
        /// </summary>
        /// <param name="id"></param>
        /// <param name="anomalyTypeDb"> AnomalyType to update</param>
        /// <returns>Modified anomalyType info</returns>
        AnomalyTypeDb Update(AnomalyTypeDb anomalyTypeDb, bool commit = true);

        /// <summary>
        /// Get specific anomalyType by id
        /// </summary>
        /// <param name="id"> anomalyType on retrieved data </param>
        /// <returns> get the existant anomalyType</returns>
        AnomalyTypeDb Get(int id, string includeProperties = "");

        /// <summary>
        ///  Delete specific anomalyType by id
        /// </summary>
        /// <param name="id">identifier of the anomalyType to delete</param>
        /// <returns> Deleted anomalyType identifier</returns>
        int Delete(int id);

        /// <summary>
        /// Get all anomalyType
        /// </summary>
        /// <returns> get all existant anomalyType </returns>
        IEnumerable<AnomalyTypeDb> GetAll(Expression<Func<AnomalyTypeDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true);
    }
}
