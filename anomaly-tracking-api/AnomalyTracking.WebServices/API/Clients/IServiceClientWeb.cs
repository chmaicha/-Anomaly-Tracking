using AnomalyTracking.Model.Clients;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AnomalyTracking.WebServices.API.Clients
{
    [ServiceContract]
    public interface IServiceClientWeb:IServiceBaseWeb
    {
        /// <summary>
        /// Create the given client
        /// </summary>
        /// <param name="client">Client to create</param>
        /// <returns>Create a new client</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/clients", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Client> Create(Client client);

        /// <summary>
        /// Update the given client
        /// <param name="id"></param>
        /// </summary>
        /// <param name="client"> Client to update</param>
        /// <returns>Modified client info</returns>
        [WebInvoke(Method = "PUT", UriTemplate = "/api/clients/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Client> Update(string id, Client client);

        /// <summary>
        ///  Delete specific user by id
        /// </summary>
        /// <param name="id">identifier of the user to delete</param>
        /// <returns> Deleted user identifier</returns>
        [WebInvoke(Method = "DELETE", UriTemplate = "/api/clients/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<int> Delete(string id);

        /// <summary>
        /// Deletes a list of clients based on identifiers.
        /// </summary>
        /// <param name="clientsIds"> The list of identifiers of clients to be deleted</param>
        /// <returns> The list of clients identifiers deleted</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/clients/_delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<int>> DeleteALL(string[] clientsIds);

        /// <summary>
        /// Get specific client by id
        /// </summary>
        /// <param name="id"> client on retrieved data </param>
        /// <returns> get the existant client</returns>
        [WebInvoke(Method = "GET", UriTemplate = "/api/clients/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response<Client> Get(string id);

        /// <summary>
        /// Get all client
        /// </summary>
        /// <returns> get all existant client </returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/clients/_filter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<Client>> GetAll(SearchFilterBase filter);

     
    }
}
