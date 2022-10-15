using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Clients
{
    /// <summary>
    /// Interface that manages client operations.
    /// </summary>
    public interface IServiceClient : IServiceBase<ClientDb, IAnomalyTrackingUnitOfWork>, IServiceContext<IAnomalyTrackingUnitOfWork>
    {
        /// <summary>
        /// Create the given client
        /// </summary>
        /// <param name="clientDb">Client to create</param>
        /// <returns>Create a new client</returns>
        ClientDb Create(ClientDb clientDb);
        /// <summary>
        /// Update the given client
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientDb"> Client to update</param>
        /// <returns>Modified client info</returns>
        ClientDb Update(ClientDb clientDb, bool commit = true);

        /// <summary>
        /// Get specific client by id
        /// </summary>
        /// <param name="id"> client on retrieved data </param>
        /// <returns> get the existant client</returns>
        ClientDb Get(int id, string includeProperties = "");

        /// <summary>
        ///  Delete specific client by id
        /// </summary>
        /// <param name="id">identifier of the client to delete</param>
        /// <returns> Deleted client identifier</returns>
        int Delete(int id);

        /// <summary>
        /// Get all client
        /// </summary>
        /// <returns> get all existant client </returns>
        IEnumerable<ClientDb> GetAll(Expression<Func<ClientDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true);
    }
}
