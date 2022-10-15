using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Anomalies
{
    /// <summary>
    /// Interface that manages anomaly operations.
    /// </summary>
    public interface IServiceAnomaly : IServiceBase<AnomalyDb, IAnomalyTrackingUnitOfWork>, IServiceContext<IAnomalyTrackingUnitOfWork>
    {
        /// <summary>
        /// Create the given anomaly
        /// </summary>
        /// <param name="anomalyDb">Anomaly to create</param>
        /// <returns>Create a new anomaly</returns>
        AnomalyDb Create(AnomalyDb anomalyDb);
        /// <summary>
        /// Update the given anomaly
        /// </summary>
        /// <param name="id"></param>
        /// <param name="anomalyDb"> Anomaly to update</param>
        /// <returns>Modified anomaly info</returns>
        AnomalyDb Update(AnomalyDb anomalyDb, bool commit = true);

        /// <summary>
        /// Get specific anomaly by id
        /// </summary>
        /// <param name="id"> anomaly on retrieved data </param>
        /// <returns> get the existant anomaly</returns>
        AnomalyDb Get(int id, string includeProperties = "");

        /// <summary>
        ///  Delete specific anomaly by id
        /// </summary>
        /// <param name="id">identifier of the anomaly to delete</param>
        /// <returns> Deleted anomaly identifier</returns>
        int Delete(int id);

        /// <summary>
        /// Get all anomaly
        /// </summary>
        /// <returns> get all existant anomaly </returns>z
        IEnumerable<AnomalyDb> GetAll(Expression<Func<AnomalyDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true);
    }
}
