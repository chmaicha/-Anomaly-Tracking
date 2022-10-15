using AnomalyTracking.Model.Clients;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using System.Collections.Generic;

namespace AnomalyTracking.Business.ServiceApp.Clients
{ /// <summary>
  /// Interface that manages client operations.
  /// </summary>
    public interface IServiceClientApp
    {
        /// <summary>
        /// Create the given client
        /// </summary>
        /// <param name="client">Client to create</param>
        /// <returns>Create a new client</returns>
        Response<Client> Create(Client client);

        /// <summary>
        /// Update the given client
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"> Client to update</param>
        /// <returns>Modified client info</returns>
        Response<Client> Update(int id, Client client);


        /// <summary>
        /// Get specific client by id
        /// </summary>
        /// <param name="id"> client on retrieved data </param>
        /// <returns> get the existant client</returns>
        Response<Client> Get(int id);


        /// <summary>
        ///  Delete specific client by id
        /// </summary>
        /// <param name="id">identifier of the client to delete</param>
        /// <returns> Deleted client identifier</returns>
        Response<int> Delete(int id);

        /// <summary>
        /// Deletes a list of clients based on identifiers.
        /// </summary>
        /// <param name="entitiesIds"> The list of identifiers of clients to be deleted</param>
        /// <returns> The list of clients identifiers deleted</returns>
        Response<IEnumerable<int>> DeleteAll(string[] entitiesIds);

        /// <summary>
        /// Get all client
        /// </summary>
        /// <returns> get all existant client </returns>
        Response<IEnumerable<Client>> GetAll(SearchFilterBase filter);

    }
}