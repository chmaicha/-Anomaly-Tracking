using AnomalyTracking.Model.AnomalyDeclarations;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AnomalyTracking.WebServices.API.AnomalyDeclarations
{
    [ServiceContract]
    public interface IServiceAnomalyDeclarationWeb : IServiceBaseWeb
    {
        /// <summary>
        /// Create the given anomalyDeclaration
        /// </summary>
        /// <param name="anomalyDeclaration">AnomalyDeclaration to create</param>
        /// <returns>Create a new anomalyDeclaration</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/anomalyDeclarations", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<AnomalyDeclaration> Create(AnomalyDeclaration anomalyDeclaration);

        /// <summary>
        /// Update the given anomalyDeclaration
        /// <param name="id"></param>
        /// </summary>
        /// <param name="anomalyDeclaration"> AnomalyDeclaration to update</param>
        /// <returns>Modified anomalyDeclaration info</returns>
        [WebInvoke(Method = "PUT", UriTemplate = "/api/anomalyDeclarations/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<AnomalyDeclaration> Update(string id, AnomalyDeclaration anomalyDeclaration);

        /// <summary>
        ///  Delete specific anomalyDeclaration by id
        /// </summary>
        /// <param name="id">identifier of the anomalyDeclaration to delete</param>
        /// <returns> Deleted anomalyDeclaration identifier</returns>
        [WebInvoke(Method = "DELETE", UriTemplate = "/api/anomalyDeclarations/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<int> Delete(string id);

        /// <summary>
        /// Deletes a list of anomalyDeclarations based on identifiers.
        /// </summary>
        /// <param name="anomalyDeclarationsIds"> The list of identifiers of anomalyDeclarations to be deleted</param>
        /// <returns> The list of anomalyDeclarations identifiers deleted</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/anomalyDeclarations/_delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<int>> DeleteALL(string[] anomalyDeclarationsIds);

        /// <summary>
        /// Get specific anomalyDeclaration by id
        /// </summary>
        /// <param name="id"> anomalyDeclaration on retrieved data </param>
        /// <returns> get the existant anomalyDeclaration</returns>
        [WebInvoke(Method = "GET", UriTemplate = "/api/anomalyDeclarations/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response<AnomalyDeclaration> Get(string id);

        /// <summary>
        /// Get all anomalyDeclaration
        /// </summary>
        /// <returns> get all existant anomalyDeclaration </returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/anomalyDeclarations/_filter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<AnomalyDeclaration>> GetAll(SearchFilterBase filter);


    }
}
