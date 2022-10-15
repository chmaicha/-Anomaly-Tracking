using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Anomalies
{
    /// <summary>
    /// Class that manages anomaly.
    /// </summary>
    public class ServiceAnomaly : ServiceBase<AnomalyDb, IAnomalyTrackingUnitOfWork>, IServiceAnomaly
    {
        public ServiceAnomaly(IAnomalyTrackingUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public AnomalyDb Create(AnomalyDb anomalyDb)
        {
            anomalyDb = this.unitOfWork.AnomalyRepo.Add(anomalyDb, true);

            return this.Get(anomalyDb.Id);
        }

        public AnomalyDb Update(AnomalyDb anomaly, bool commit = true)
        {
            AnomalyDb anomalyDb = this.Get(anomaly.Id);

            anomalyDb.FaceId = anomaly.FaceId;
            anomalyDb.AnomalyTypeId= anomaly.AnomalyTypeId;


            this.unitOfWork.AnomalyRepo.Update(anomalyDb, true);

            return this.Get(anomalyDb.Id);
        }

        public int Delete(int id)
        {
            AnomalyDb anomalyDb = this.unitOfWork.AnomalyRepo.GetById(id);
            this.unitOfWork.AnomalyRepo.Update(anomalyDb, true);
            this.unitOfWork.AnomalyRepo.Delete(id, true);
            return id;
        }

        public AnomalyDb Get(int id, string includeProperties = "")
        {
            return this.GetAll(c => c.Id == id, includeProperties).Single();
        }

        public IEnumerable<AnomalyDb> GetAll(Expression<Func<AnomalyDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true)
        {
            this.filter.IncludeProperties = string.IsNullOrWhiteSpace(includeProperties) ? "" : includeProperties;
            this.filter.EntityFilter = filter;
            this.filter.Page = page;
            this.filter.Paginate = paginate;
            this.filter.OrderBy = p => p.OrderByDescending(e => e.FaceId);

            return this.unitOfWork.AnomalyRepo.GetAll(this.filter).ToList();
        }
    }
}
