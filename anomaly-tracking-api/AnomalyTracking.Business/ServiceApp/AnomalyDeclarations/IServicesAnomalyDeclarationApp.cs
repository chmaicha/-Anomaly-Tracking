using AnomalyTracking.Model.AnomalyDeclarations;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using System.Collections.Generic;

namespace AnomalyTracking.Business.ServiceApp.AnomalyDeclarations
{ /// <summary>
  /// Interface that manages anomalyDeclaration operations.
  /// </summary>
    public interface IServiceAnomalyDeclarationApp
    {
        /// <summary>
        /// Create the given anomalyDeclaration
        /// </summary>
        /// <param name="anomalyDeclaration">AnomalyDeclaration to create</param>
        /// <returns>Create a new anomalyDeclaration</returns>
        Response<AnomalyDeclaration> Create(AnomalyDeclaration anomalyDeclaration);

        /// <summary>
        /// Update the given anomalyDeclaration
        /// </summary>
        /// <param name="id"></param>
        /// <param name="anomalyDeclaration"> AnomalyDeclaration to update</param>
        /// <returns>Modified anomalyDeclaration info</returns>
        Response<AnomalyDeclaration> Update(int id, AnomalyDeclaration anomalyDeclaration);


        /// <summary>
        /// Get specific anomalyDeclaration by id
        /// </summary>
        /// <param name="id"> anomalyDeclaration on retrieved data </param>
        /// <returns> get the existant anomalyDeclaration</returns>
        Response<AnomalyDeclaration> Get(int id);


        /// <summary>
        ///  Delete specific anomalyDeclaration by id
        /// </summary>
        /// <param name="id">identifier of the anomalyDeclaration to delete</param>
        /// <returns> Deleted anomalyDeclaration identifier</returns>
        Response<int> Delete(int id);

        /// <summary>
        /// Deletes a list of anomalyDeclarations based on identifiers.
        /// </summary>
        /// <param name="entitiesIds"> The list of identifiers of anomalyDeclarations to be deleted</param>
        /// <returns> The list of anomalyDeclarations identifiers deleted</returns>
        Response<IEnumerable<int>> DeleteAll(string[] entitiesIds);

        /// <summary>
        /// Get all anomalyDeclaration
        /// </summary>
        /// <returns> get all existant anomalyDeclaration </returns>
        Response<IEnumerable<AnomalyDeclaration>> GetAll(SearchFilterBase filter);

    }
}