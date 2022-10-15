using AnomalyTracking.Model.Users;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AnomalyTracking.WebServices.API.Users
{
    [ServiceContract]
    public interface IServiceUserWeb : IServiceBaseWeb
    {
        /// <summary>
        /// Create the given user
        /// </summary>
        /// <param name="user">User to create</param>
        /// <returns>Create a new user</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/users", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<User> Create(User user);

        /// <summary>
        /// Update the given user
        /// <param name="id"></param>
        /// </summary>
        /// <param name="user"> User to update</param>
        /// <returns>Modified user info</returns>
        [WebInvoke(Method = "PUT", UriTemplate = "/api/users/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<User> Update(string id, User user);

        /// <summary>
        ///  Delete specific user by id
        /// </summary>
        /// <param name="id">identifier of the user to delete</param>
        /// <returns> Deleted user identifier</returns>
        [WebInvoke(Method = "DELETE", UriTemplate = "/api/users/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<int> Delete(string id);

        /// <summary>
        /// Deletes a list of users based on identifiers.
        /// </summary>
        /// <param name="usersIds"> The list of identifiers of users to be deleted</param>
        /// <returns> The list of users identifiers deleted</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/users/_delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<int>> DeleteALL(string[] usersIds);

        /// <summary>
        /// Get specific user by id
        /// </summary>
        /// <param name="id"> user on retrieved data </param>
        /// <returns> get the existant user</returns>
        [WebInvoke(Method = "GET", UriTemplate = "/api/users/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response<User> Get(string id);

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns> get all existant user </returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/users/_filter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<User>> GetAll(SearchFilterBase filter);


    }
}
