using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Faces
{
    /// <summary>
    /// Interface that manages face operations.
    /// </summary>
    public interface IServiceFace : IServiceBase<FaceDb, IAnomalyTrackingUnitOfWork>, IServiceContext<IAnomalyTrackingUnitOfWork>
    {
        /// <summary>
        /// Create the given face
        /// </summary>
        /// <param name="faceDb">Face to create</param>
        /// <returns>Create a new face</returns>
        FaceDb Create(FaceDb faceDb);
        /// <summary>
        /// Update the given face
        /// </summary>
        /// <param name="id"></param>
        /// <param name="faceDb"> Face to update</param>
        /// <returns>Modified face info</returns>
        FaceDb Update(FaceDb faceDb, bool commit = true);

        /// <summary>
        /// Get specific face by id
        /// </summary>
        /// <param name="id"> face on retrieved data </param>
        /// <returns> get the existant face</returns>
        FaceDb Get(int id, string includeProperties = "");

        /// <summary>
        ///  Delete specific face by id
        /// </summary>
        /// <param name="id">identifier of the face to delete</param>
        /// <returns> Deleted face identifier</returns>
        int Delete(int id);

        /// <summary>
        /// Get all face
        /// </summary>
        /// <returns> get all existant face </returns>
        IEnumerable<FaceDb> GetAll(Expression<Func<FaceDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true);
    }
}
