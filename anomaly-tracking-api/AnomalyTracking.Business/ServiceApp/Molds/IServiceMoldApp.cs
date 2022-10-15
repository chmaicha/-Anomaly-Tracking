using AnomalyTracking.Model.Molds;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using System.Collections.Generic;

namespace AnomalyTracking.Business.ServiceApp.Molds
{ /// <summary>
  /// Interface that manages mold operations.
  /// </summary>
    public interface IServiceMoldApp
    {
        /// <summary>
        /// Create the given mold
        /// </summary>
        /// <param name="mold">Mold to create</param>
        /// <returns>Create a new mold</returns>
        Response<Mold> Create(Mold mold);

        /// <summary>
        /// Update the given mold
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mold"> Mold to update</param>
        /// <returns>Modified mold info</returns>
        Response<Mold> Update(int id, Mold mold);


        /// <summary>
        /// Get specific mold by id
        /// </summary>
        /// <param name="id"> mold on retrieved data </param>
        /// <returns> get the existant mold</returns>
        Response<Mold> Get(int id);


        /// <summary>
        ///  Delete specific mold by id
        /// </summary>
        /// <param name="id">identifier of the mold to delete</param>
        /// <returns> Deleted mold identifier</returns>
        Response<int> Delete(int id);

        /// <summary>
        /// Deletes a list of molds based on identifiers.
        /// </summary>
        /// <param name="entitiesIds"> The list of identifiers of molds to be deleted</param>
        /// <returns> The list of molds identifiers deleted</returns>
        Response<IEnumerable<int>> DeleteAll(string[] entitiesIds);

        /// <summary>
        /// Get all mold
        /// </summary>
        /// <returns> get all existant mold </returns>
        Response<IEnumerable<Mold>> GetAll(SearchFilterBase filter);

    }
}