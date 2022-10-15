using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Users
{
    /// <summary>
    /// Interface that manages user operations.
    /// </summary>
    public interface IServiceUser : IServiceBase<UserDb, IAnomalyTrackingUnitOfWork>, IServiceContext<IAnomalyTrackingUnitOfWork>
    {
        /// <summary>
        /// Create the given user
        /// </summary>
        /// <param name="userDb">User to create</param>
        /// <returns>Create a new user</returns>
        UserDb Create(UserDb userDb);
        /// <summary>
        /// Update the given user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userDb"> User to update</param>
        /// <returns>Modified user info</returns>
        UserDb Update(UserDb userDb, bool commit = true);

        /// <summary>
        /// Get specific user by id
        /// </summary>
        /// <param name="id"> user on retrieved data </param>
        /// <returns> get the existant user</returns>
        UserDb Get(int id, string includeProperties = "");

        /// <summary>
        ///  Delete specific user by id
        /// </summary>
        /// <param name="id">identifier of the user to delete</param>
        /// <returns> Deleted user identifier</returns>
        int Delete(int id);

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns> get all existant user </returns>
        IEnumerable<UserDb> GetAll(Expression<Func<UserDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true);
    }
}
