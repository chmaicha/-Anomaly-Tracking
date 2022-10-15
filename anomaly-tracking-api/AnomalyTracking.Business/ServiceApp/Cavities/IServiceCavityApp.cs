using AnomalyTracking.Model.Cavities;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using System.Collections.Generic;

namespace AnomalyTracking.Business.ServiceApp.Cavities
{ /// <summary>
  /// Intercavity that manages cavity operations.
  /// </summary>
    public interface IServiceCavityApp
    {
        /// <summary>
        /// Create the given cavity
        /// </summary>
        /// <param name="cavity">Cavity to create</param>
        /// <returns>Create a new cavity</returns>
        Response<Cavity> Create(Cavity cavity);

        /// <summary>
        /// Update the given cavity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cavity"> Cavity to update</param>
        /// <returns>Modified cavity info</returns>
        Response<Cavity> Update(int id, Cavity cavity);


        /// <summary>
        /// Get specific cavity by id
        /// </summary>
        /// <param name="id"> cavity on retrieved data </param>
        /// <returns> get the existant cavity</returns>
        Response<Cavity> Get(int id);


        /// <summary>
        ///  Delete specific cavity by id
        /// </summary>
        /// <param name="id">identifier of the cavity to delete</param>
        /// <returns> Deleted cavity identifier</returns>
        Response<int> Delete(int id);

        /// <summary>
        /// Deletes a list of cavities based on identifiers.
        /// </summary>
        /// <param name="entitiesIds"> The list of identifiers of cavities to be deleted</param>
        /// <returns> The list of cavities identifiers deleted</returns>
        Response<IEnumerable<int>> DeleteAll(string[] entitiesIds);

        /// <summary>
        /// Get all cavity
        /// </summary>
        /// <returns> get all existant cavity </returns>
        Response<IEnumerable<Cavity>> GetAll(SearchFilterBase filter);

    }
}