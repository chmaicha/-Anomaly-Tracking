using AnomalyTracking.Model.Molds;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AnomalyTracking.WebServices.API.Molds
{
    [ServiceContract]
    public interface IServiceMoldWeb : IServiceBaseWeb
    {
        /// <summary>
        /// Create the given mold
        /// </summary>
        /// <param name="mold">Mold to create</param>
        /// <returns>Create a new mold</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/molds", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Mold> Create(Mold mold);

        /// <summary>
        /// Update the given mold
        /// <param name="id"></param>
        /// </summary>
        /// <param name="mold"> Mold to update</param>
        /// <returns>Modified mold info</returns>
        [WebInvoke(Method = "PUT", UriTemplate = "/api/molds/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Mold> Update(string id, Mold mold);

        /// <summary>
        ///  Delete specific mold by id
        /// </summary>
        /// <param name="id">identifier of the mold to delete</param>
        /// <returns> Deleted mold identifier</returns>
        [WebInvoke(Method = "DELETE", UriTemplate = "/api/molds/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<int> Delete(string id);

        /// <summary>
        /// Deletes a list of molds based on identifiers.
        /// </summary>
        /// <param name="moldsIds"> The list of identifiers of molds to be deleted</param>
        /// <returns> The list of molds identifiers deleted</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/molds/_delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<int>> DeleteALL(string[] moldsIds);

        /// <summary>
        /// Get specific mold by id
        /// </summary>
        /// <param name="id"> mold on retrieved data </param>
        /// <returns> get the existant mold</returns>
        [WebInvoke(Method = "GET", UriTemplate = "/api/molds/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response<Mold> Get(string id);

        /// <summary>
        /// Get all mold
        /// </summary>
        /// <returns> get all existant mold </returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/molds/_filter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<Mold>> GetAll(SearchFilterBase filter);


    }
}
