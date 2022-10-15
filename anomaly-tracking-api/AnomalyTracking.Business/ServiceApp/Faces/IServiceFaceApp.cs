using AnomalyTracking.Model.Faces;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using System.Collections.Generic;

namespace AnomalyTracking.Business.ServiceApp.Faces
{ /// <summary>
  /// Interface that manages face operations.
  /// </summary>
    public interface IServiceFaceApp
    {
        /// <summary>
        /// Create the given face
        /// </summary>
        /// <param name="face">Face to create</param>
        /// <returns>Create a new face</returns>
        Response<Face> Create(Face face);

        /// <summary>
        /// Update the given face
        /// </summary>
        /// <param name="id"></param>
        /// <param name="face"> Face to update</param>
        /// <returns>Modified face info</returns>
        Response<Face> Update(int id, Face face);


        /// <summary>
        /// Get specific face by id
        /// </summary>
        /// <param name="id"> face on retrieved data </param>
        /// <returns> get the existant face</returns>
        Response<Face> Get(int id);


        /// <summary>
        ///  Delete specific face by id
        /// </summary>
        /// <param name="id">identifier of the face to delete</param>
        /// <returns> Deleted face identifier</returns>
        Response<int> Delete(int id);

        /// <summary>
        /// Deletes a list of faces based on identifiers.
        /// </summary>
        /// <param name="entitiesIds"> The list of identifiers of faces to be deleted</param>
        /// <returns> The list of faces identifiers deleted</returns>
        Response<IEnumerable<int>> DeleteAll(string[] entitiesIds);

        /// <summary>
        /// Get all face
        /// </summary>
        /// <returns> get all existant face </returns>
        Response<IEnumerable<Face>> GetAll(SearchFilterBase filter);

    }
}