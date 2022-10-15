using AnomalyTracking.Model.AnomalyTypes;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AnomalyTracking.WebServices.API.AnomalyTypes
{
    [ServiceContract]
    public interface IServiceAnomalyTypeWeb : IServiceBaseWeb
    {
        /// <summary>
        /// Create the given anomalyType
        /// </summary>
        /// <param name="anomalyType">AnomalyType to create</param>
        /// <returns>Create a new anomalyType</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/anomalyTypes", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<AnomalyType> Create(AnomalyType anomalyType);

        /// <summary>
        /// Update the given anomalyType
        /// <param name="id"></param>
        /// </summary>
        /// <param name="anomalyType"> AnomalyType to update</param>
        /// <returns>Modified anomalyType info</returns>
        [WebInvoke(Method = "PUT", UriTemplate = "/api/anomalyTypes/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<AnomalyType> Update(string id, AnomalyType anomalyType);

        /// <summary>
        ///  Delete specific anomalyType by id
        /// </summary>
        /// <param name="id">identifier of the anomalyType to delete</param>
        /// <returns> Deleted anomalyType identifier</returns>
        [WebInvoke(Method = "DELETE", UriTemplate = "/api/anomalyTypes/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<int> Delete(string id);

        /// <summary>
        /// Deletes a list of anomalyTypes based on identifiers.
        /// </summary>
        /// <param name="anomalyTypesIds"> The list of identifiers of anomalyTypes to be deleted</param>
        /// <returns> The list of anomalyTypes identifiers deleted</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/anomalyTypes/_delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<int>> DeleteALL(string[] anomalyTypesIds);

        /// <summary>
        /// Get specific anomalyType by id
        /// </summary>
        /// <param name="id"> anomalyType on retrieved data </param>
        /// <returns> get the existant anomalyType</returns>
        [WebInvoke(Method = "GET", UriTemplate = "/api/anomalyTypes/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response<AnomalyType> Get(string id);

        /// <summary>
        /// Get all anomalyType
        /// </summary>
        /// <returns> get all existant anomalyType </returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/anomalyTypes/_filter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<AnomalyType>> GetAll(SearchFilterBase filter);


    }
}
