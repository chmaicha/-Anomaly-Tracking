using AnomalyTracking.Model.Faces;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AnomalyTracking.WebServices.API.Faces
{
    [ServiceContract]
    public interface IServiceFaceWeb : IServiceBaseWeb
    {
        /// <summary>
        /// Create the given face
        /// </summary>
        /// <param name="face">Face to create</param>
        /// <returns>Create a new face</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/faces", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Face> Create(Face face);

        /// <summary>
        /// Update the given face
        /// <param name="id"></param>
        /// </summary>
        /// <param name="face"> Face to update</param>
        /// <returns>Modified face info</returns>
        [WebInvoke(Method = "PUT", UriTemplate = "/api/faces/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Face> Update(string id, Face face);

        /// <summary>
        ///  Delete specific face by id
        /// </summary>
        /// <param name="id">identifier of the face to delete</param>
        /// <returns> Deleted face identifier</returns>
        [WebInvoke(Method = "DELETE", UriTemplate = "/api/faces/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<int> Delete(string id);

        /// <summary>
        /// Deletes a list of faces based on identifiers.
        /// </summary>
        /// <param name="facesIds"> The list of identifiers of faces to be deleted</param>
        /// <returns> The list of faces identifiers deleted</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/faces/_delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<int>> DeleteALL(string[] facesIds);

        /// <summary>
        /// Get specific face by id
        /// </summary>
        /// <param name="id"> face on retrieved data </param>
        /// <returns> get the existant face</returns>
        [WebInvoke(Method = "GET", UriTemplate = "/api/faces/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response<Face> Get(string id);

        /// <summary>
        /// Get all face
        /// </summary>
        /// <returns> get all existant face </returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/faces/_filter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<Face>> GetAll(SearchFilterBase filter);


    }
}
