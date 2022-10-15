using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Faces
{
    /// <summary>
    /// Class that manages face.
    /// </summary>
    public class ServiceFace : ServiceBase<FaceDb, IAnomalyTrackingUnitOfWork>, IServiceFace
    {
        public ServiceFace(IAnomalyTrackingUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public FaceDb Create(FaceDb faceDb)
        {
            faceDb.LastModificationDate = DateTime.Now;
            faceDb = this.unitOfWork.FaceRepo.Add(faceDb, false);
            return this.Get(faceDb.Id);
        }

        public FaceDb Update(FaceDb face, bool commit = true)
        {
            FaceDb faceDb = this.Get(face.Id);

            faceDb.Label = face.Label;
            faceDb.LastModificationDate = DateTime.Now;
        

            this.unitOfWork.FaceRepo.Update(faceDb, true);

            return this.Get(faceDb.Id);
        }

        public int Delete(int id)
        {
            FaceDb faceDb = this.unitOfWork.FaceRepo.GetById(id);
            this.unitOfWork.FaceRepo.Update(faceDb, true);
            this.unitOfWork.FaceRepo.Delete(id, true);
            return id;
        }

        public FaceDb Get(int id, string includeProperties = "")
        {
            return this.GetAll(c => c.Id == id, includeProperties).Single();
        }

        public IEnumerable<FaceDb> GetAll(Expression<Func<FaceDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true)
        {
            this.filter.IncludeProperties = string.IsNullOrWhiteSpace(includeProperties) ? "" : includeProperties;
            this.filter.EntityFilter = filter;
            this.filter.Page = page;
            this.filter.Paginate = paginate;
            this.filter.OrderBy = p => p.OrderByDescending(e => e.LastModificationDate);

            return this.unitOfWork.FaceRepo.GetAll(this.filter).ToList();
        }
    }
}
