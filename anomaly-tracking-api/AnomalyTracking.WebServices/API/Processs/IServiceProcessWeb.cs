using AnomalyTracking.Model.Processs;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AnomalyTracking.WebServices.API.Processs
{
    [ServiceContract]
    public interface IServiceProcessWeb : IServiceBaseWeb
    {
        /// <summary>
        /// Create the given process
        /// </summary>
        /// <param name="process">Process to create</param>
        /// <returns>Create a new process</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/processs", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Process> Create(Process process);

        /// <summary>
        /// Update the given process
        /// <param name="id"></param>
        /// </summary>
        /// <param name="process"> Process to update</param>
        /// <returns>Modified process info</returns>
        [WebInvoke(Method = "PUT", UriTemplate = "/api/processs/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Process> Update(string id, Process process);

        /// <summary>
        ///  Delete specific process by id
        /// </summary>
        /// <param name="id">identifier of the process to delete</param>
        /// <returns> Deleted process identifier</returns>
        [WebInvoke(Method = "DELETE", UriTemplate = "/api/processs/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<int> Delete(string id);

        /// <summary>
        /// Deletes a list of processs based on identifiers.
        /// </summary>
        /// <param name="processsIds"> The list of identifiers of processs to be deleted</param>
        /// <returns> The list of processs identifiers deleted</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/processs/_delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<int>> DeleteALL(string[] processsIds);

        /// <summary>
        /// Get specific process by id
        /// </summary>
        /// <param name="id"> process on retrieved data </param>
        /// <returns> get the existant process</returns>
        [WebInvoke(Method = "GET", UriTemplate = "/api/processs/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response<Process> Get(string id);

        /// <summary>
        /// Get all process
        /// </summary>
        /// <returns> get all existant process </returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/processs/_filter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<Process>> GetAll(SearchFilterBase filter);


    }
}
