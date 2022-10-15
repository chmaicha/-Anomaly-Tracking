using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.AnomalyTypes
{
    /// <summary>
    /// Class that manages anomalyType.
    /// </summary>
    public class ServiceAnomalyType : ServiceBase<AnomalyTypeDb, IAnomalyTrackingUnitOfWork>, IServiceAnomalyType
    {
        public ServiceAnomalyType(IAnomalyTrackingUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public AnomalyTypeDb Create(AnomalyTypeDb anomalyTypeDb)
        {
            anomalyTypeDb.LastModificationDate = DateTime.Now;
            anomalyTypeDb = this.unitOfWork.AnomalyTypeRepo.Add(anomalyTypeDb, true);
            return this.Get(anomalyTypeDb.Id);
        }

        public AnomalyTypeDb Update(AnomalyTypeDb anomalyType, bool commit = true)
        {
            AnomalyTypeDb anomalyTypeDb = this.Get(anomalyType.Id);

            anomalyTypeDb.Label = anomalyType.Label;
            anomalyTypeDb.LastModificationDate = DateTime.Now;

            this.unitOfWork.AnomalyTypeRepo.Update(anomalyTypeDb, true);

            return this.Get(anomalyTypeDb.Id);
        }

        public int Delete(int id)
        {
            AnomalyTypeDb anomalyTypeDb = this.unitOfWork.AnomalyTypeRepo.GetById(id);
            this.unitOfWork.AnomalyTypeRepo.Update(anomalyTypeDb, true);
            this.unitOfWork.AnomalyTypeRepo.Delete(id, true);
            return id;
        }

        public AnomalyTypeDb Get(int id, string includeProperties = "")
        {
            return this.GetAll(c => c.Id == id, includeProperties).Single();
        }

        public IEnumerable<AnomalyTypeDb> GetAll(Expression<Func<AnomalyTypeDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true)
        {
            this.filter.IncludeProperties = string.IsNullOrWhiteSpace(includeProperties) ? "ProcessDb" : includeProperties;
            this.filter.EntityFilter = filter;
            this.filter.Page = page;
            this.filter.Paginate = paginate;
            this.filter.OrderBy = p => p.OrderByDescending(e => e.LastModificationDate);

            return this.unitOfWork.AnomalyTypeRepo.GetAll(this.filter).ToList();
        }
    }
}
