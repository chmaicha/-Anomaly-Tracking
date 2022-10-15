using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.AnomalyDeclarations
{
    /// <summary>
    /// InteranomalyDeclaration that manages anomalyDeclaration operations.
    /// </summary>
    public interface IServiceAnomalyDeclaration : IServiceBase<AnomalyDeclarationDb, IAnomalyTrackingUnitOfWork>, IServiceContext<IAnomalyTrackingUnitOfWork>
    {
        /// <summary>
        /// Create the given anomalyDeclaration
        /// </summary>
        /// <param name="anomalyDeclarationDb">AnomalyDeclaration to create</param>
        /// <returns>Create a new anomalyDeclaration</returns>
        AnomalyDeclarationDb Create(AnomalyDeclarationDb anomalyDeclarationDb);
        /// <summary>
        /// Update the given anomalyDeclaration
        /// </summary>
        /// <param name="id"></param>
        /// <param name="anomalyDeclarationDb"> AnomalyDeclaration to update</param>
        /// <returns>Modified anomalyDeclaration info</returns>
        AnomalyDeclarationDb Update(AnomalyDeclarationDb anomalyDeclarationDb, bool commit = true);

        /// <summary>
        /// Get specific anomalyDeclaration by id
        /// </summary>
        /// <param name="id"> anomalyDeclaration on retrieved data </param>
        /// <returns> get the existant anomalyDeclaration</returns>
        AnomalyDeclarationDb Get(int id, string includeProperties = "");

        /// <summary>
        ///  Delete specific anomalyDeclaration by id
        /// </summary>
        /// <param name="id">identifier of the anomalyDeclaration to delete</param>
        /// <returns> Deleted anomalyDeclaration identifier</returns>
        int Delete(int id);

        /// <summary>
        /// Get all anomalyDeclaration
        /// </summary>
        /// <returns> get all existant anomalyDeclaration </returns>
        IEnumerable<AnomalyDeclarationDb> GetAll(Expression<Func<AnomalyDeclarationDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true);
    }
}
