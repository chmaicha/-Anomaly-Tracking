using AnomalyTracking.Model.Anomalies;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using System.Collections.Generic;

namespace AnomalyTracking.Business.ServiceApp.Anomalies
{ /// <summary>
  /// Interface that manages anomaly operations.
  /// </summary>
    public interface IServiceAnomalyApp
    {
        /// <summary>
        /// Create the given anomaly
        /// </summary>
        /// <param name="anomaly">Anomaly to create</param>
        /// <returns>Create a new anomaly</returns>
        Response<Anomaly> Create(Anomaly anomaly);

        /// <summary>
        /// Update the given anomaly
        /// </summary>
        /// <param name="id"></param>
        /// <param name="anomaly"> Anomaly to update</param>
        /// <returns>Modified anomaly info</returns>
        Response<Anomaly> Update(int id, Anomaly anomaly);


        /// <summary>
        /// Get specific anomaly by id
        /// </summary>
        /// <param name="id"> anomaly on retrieved data </param>
        /// <returns> get the existant anomaly</returns>
        Response<Anomaly> Get(int id);


        /// <summary>
        ///  Delete specific anomaly by id
        /// </summary>
        /// <param name="id">identifier of the anomaly to delete</param>
        /// <returns> Deleted anomaly identifier</returns>
        Response<int> Delete(int id);

        /// <summary>
        /// Deletes a list of anomalies based on identifiers.
        /// </summary>
        /// <param name="entitiesIds"> The list of identifiers of anomalies to be deleted</param>
        /// <returns> The list of anomalies identifiers deleted</returns>
        Response<IEnumerable<int>> DeleteAll(string[] entitiesIds);

        /// <summary>
        /// Get all anomaly
        /// </summary>
        /// <returns> get all existant anomaly </returns>
        Response<IEnumerable<Anomaly>> GetAll(SearchFilterBase filter);

    }
}