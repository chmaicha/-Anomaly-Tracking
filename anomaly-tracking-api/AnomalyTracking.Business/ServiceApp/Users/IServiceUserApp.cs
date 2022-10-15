using AnomalyTracking.Model.Users;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using System.Collections.Generic;

namespace AnomalyTracking.Business.ServiceApp.Users
{ /// <summary>
  /// Interface that manages user operations.
  /// </summary>
    public interface IServiceUserApp
    {
        /// <summary>
        /// Create the given user
        /// </summary>
        /// <param name="user">User to create</param>
        /// <returns>Create a new user</returns>
        Response<User> Create(User user);

        /// <summary>
        /// Update the given user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"> User to update</param>
        /// <returns>Modified user info</returns>
        Response<User> Update(int id, User user);


        /// <summary>
        /// Get specific user by id
        /// </summary>
        /// <param name="id"> user on retrieved data </param>
        /// <returns> get the existant user</returns>
        Response<User> Get(int id);


        /// <summary>
        ///  Delete specific user by id
        /// </summary>
        /// <param name="id">identifier of the user to delete</param>
        /// <returns> Deleted user identifier</returns>
        Response<int> Delete(int id);

        /// <summary>
        /// Deletes a list of users based on identifiers.
        /// </summary>
        /// <param name="entitiesIds"> The list of identifiers of users to be deleted</param>
        /// <returns> The list of users identifiers deleted</returns>
        Response<IEnumerable<int>> DeleteAll(string[] entitiesIds);

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns> get all existant user </returns>
        Response<IEnumerable<User>> GetAll(SearchFilterBase filter);

    }
}