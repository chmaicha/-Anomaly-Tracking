using AnomalyTracking.Model.Cavities;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AnomalyTracking.WebServices.API.Cavities
{
    [ServiceContract]
    public interface IServiceCavityWeb : IServiceBaseWeb
    {
        /// <summary>
        /// Create the given cavity
        /// </summary>
        /// <param name="cavity">Cavity to create</param>
        /// <returns>Create a new cavity</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/cavities", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Cavity> Create(Cavity cavity);

        /// <summary>
        /// Update the given cavity
        /// <param name="id"></param>
        /// </summary>
        /// <param name="cavity"> Cavity to update</param>
        /// <returns>Modified cavity info</returns>
        [WebInvoke(Method = "PUT", UriTemplate = "/api/cavities/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Cavity> Update(string id, Cavity cavity);

        /// <summary>
        ///  Delete specific cavity by id
        /// </summary>
        /// <param name="id">identifier of the cavity to delete</param>
        /// <returns> Deleted cavity identifier</returns>
        [WebInvoke(Method = "DELETE", UriTemplate = "/api/cavities/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<int> Delete(string id);

        /// <summary>
        /// Deletes a list of cavities based on identifiers.
        /// </summary>
        /// <param name="cavitiesIds"> The list of identifiers of cavities to be deleted</param>
        /// <returns> The list of cavities identifiers deleted</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/cavities/_delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<int>> DeleteALL(string[] cavitiesIds);

        /// <summary>
        /// Get specific cavity by id
        /// </summary>
        /// <param name="id"> cavity on retrieved data </param>
        /// <returns> get the existant cavity</returns>
        [WebInvoke(Method = "GET", UriTemplate = "/api/cavities/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response<Cavity> Get(string id);

        /// <summary>
        /// Get all cavity
        /// </summary>
        /// <returns> get all existant cavity </returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/cavities/_filter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<Cavity>> GetAll(SearchFilterBase filter);


    }
}
