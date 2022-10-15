using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Molds
{
    /// <summary>
    /// Interface that manages mold operations.
    /// </summary>
    public interface IServiceMold : IServiceBase<MoldDb, IAnomalyTrackingUnitOfWork>, IServiceContext<IAnomalyTrackingUnitOfWork>
    {
        /// <summary>
        /// Create the given mold
        /// </summary>
        /// <param name="moldDb">Mold to create</param>
        /// <returns>Create a new mold</returns>
        MoldDb Create(MoldDb moldDb);
        /// <summary>
        /// Update the given mold
        /// </summary>
        /// <param name="id"></param>
        /// <param name="moldDb"> Mold to update</param>
        /// <returns>Modified mold info</returns>
        MoldDb Update(MoldDb moldDb, bool commit = true);

        /// <summary>
        /// Get specific mold by id
        /// </summary>
        /// <param name="id"> mold on retrieved data </param>
        /// <returns> get the existant mold</returns>
        MoldDb Get(int id, string includeProperties = "");

        /// <summary>
        ///  Delete specific mold by id
        /// </summary>
        /// <param name="id">identifier of the mold to delete</param>
        /// <returns> Deleted mold identifier</returns>
        int Delete(int id);

        /// <summary>
        /// Get all mold
        /// </summary>
        /// <returns> get all existant mold </returns>z
        IEnumerable<MoldDb> GetAll(Expression<Func<MoldDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true);
    }
}
