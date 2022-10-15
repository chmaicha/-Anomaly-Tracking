using AnomalyTracking.Model.Anomalies;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AnomalyTracking.WebServices.API.Anomalies
{
    [ServiceContract]
    public interface IServiceAnomalyWeb : IServiceBaseWeb
    {
        /// <summary>
        /// Create the given anomaly
        /// </summary>
        /// <param name="anomaly">Anomaly to create</param>
        /// <returns>Create a new anomaly</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/anomalies", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Anomaly> Create(Anomaly anomaly);

        /// <summary>
        /// Update the given anomaly
        /// <param name="id"></param>
        /// </summary>
        /// <param name="anomaly"> Anomaly to update</param>
        /// <returns>Modified anomaly info</returns>
        [WebInvoke(Method = "PUT", UriTemplate = "/api/anomalies/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Anomaly> Update(string id, Anomaly anomaly);

        /// <summary>
        ///  Delete specific anomaly by id
        /// </summary>
        /// <param name="id">identifier of the anomaly to delete</param>
        /// <returns> Deleted anomaly identifier</returns>
        [WebInvoke(Method = "DELETE", UriTemplate = "/api/anomalies/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<int> Delete(string id);

        /// <summary>
        /// Deletes a list of anomalies based on identifiers.
        /// </summary>
        /// <param name="anomaliesIds"> The list of identifiers of anomalies to be deleted</param>
        /// <returns> The list of anomalies identifiers deleted</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/anomalies/_delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<int>> DeleteALL(string[] anomaliesIds);

        /// <summary>
        /// Get specific anomaly by id
        /// </summary>
        /// <param name="id"> anomaly on retrieved data </param>
        /// <returns> get the existant anomaly</returns>
        [WebInvoke(Method = "GET", UriTemplate = "/api/anomalies/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response<Anomaly> Get(string id);

        /// <summary>
        /// Get all anomaly
        /// </summary>
        /// <returns> get all existant anomaly </returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/anomalies/_filter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<Anomaly>> GetAll(SearchFilterBase filter);


    }
}
