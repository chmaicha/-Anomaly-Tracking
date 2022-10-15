using AnomalyTracking.Model.AnomalyTypes;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using System.Collections.Generic;

namespace AnomalyTracking.Business.ServiceApp.AnomalyTypes
{ /// <summary>
  /// Interface that manages anomalyType operations.
  /// </summary>
    public interface IServiceAnomalyTypeApp
    {
        /// <summary>
        /// Create the given anomalyType
        /// </summary>
        /// <param name="anomalyType">AnomalyType to create</param>
        /// <returns>Create a new anomalyType</returns>
        Response<AnomalyType> Create(AnomalyType anomalyType);

        /// <summary>
        /// Update the given anomalyType
        /// </summary>
        /// <param name="id"></param>
        /// <param name="anomalyType"> AnomalyType to update</param>
        /// <returns>Modified anomalyType info</returns>
        Response<AnomalyType> Update(int id, AnomalyType anomalyType);


        /// <summary>
        /// Get specific anomalyType by id
        /// </summary>
        /// <param name="id"> anomalyType on retrieved data </param>
        /// <returns> get the existant anomalyType</returns>
        Response<AnomalyType> Get(int id);


        /// <summary>
        ///  Delete specific anomalyType by id
        /// </summary>
        /// <param name="id">identifier of the anomalyType to delete</param>
        /// <returns> Deleted anomalyType identifier</returns>
        Response<int> Delete(int id);

        /// <summary>
        /// Deletes a list of anomalyTypes based on identifiers.
        /// </summary>
        /// <param name="entitiesIds"> The list of identifiers of anomalyTypes to be deleted</param>
        /// <returns> The list of anomalyTypes identifiers deleted</returns>
        Response<IEnumerable<int>> DeleteAll(string[] entitiesIds);

        /// <summary>
        /// Get all anomalyType
        /// </summary>
        /// <returns> get all existant anomalyType </returns>
        Response<IEnumerable<AnomalyType>> GetAll(SearchFilterBase filter);

    }
}