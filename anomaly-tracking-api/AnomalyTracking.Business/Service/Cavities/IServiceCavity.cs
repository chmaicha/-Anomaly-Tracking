using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Cavities
{
    /// <summary>
    /// Interface that manages cavity operations.
    /// </summary>
    public interface IServiceCavity : IServiceBase<CavityDb, IAnomalyTrackingUnitOfWork>, IServiceContext<IAnomalyTrackingUnitOfWork>
    {
        /// <summary>
        /// Create the given cavity
        /// </summary>
        /// <param name="cavityDb">Cavity to create</param>
        /// <returns>Create a new cavity</returns>
        CavityDb Create(CavityDb cavityDb);
        /// <summary>
        /// Update the given cavity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cavityDb"> Cavity to update</param>
        /// <returns>Modified cavity info</returns>
        CavityDb Update(CavityDb cavityDb, bool commit = true);

        /// <summary>
        /// Get specific cavity by id
        /// </summary>
        /// <param name="id"> cavity on retrieved data </param>
        /// <returns> get the existant cavity</returns>
        CavityDb Get(int id, string includeProperties = "");

        /// <summary>
        ///  Delete specific cavity by id
        /// </summary>
        /// <param name="id">identifier of the cavity to delete</param>
        /// <returns> Deleted cavity identifier</returns>
        int Delete(int id);

        /// <summary>
        /// Get all cavity
        /// </summary>
        /// <returns> get all existant cavity </returns>
        IEnumerable<CavityDb> GetAll(Expression<Func<CavityDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true);
    }
}
