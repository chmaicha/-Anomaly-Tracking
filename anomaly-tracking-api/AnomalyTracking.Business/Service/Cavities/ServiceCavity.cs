using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Cavities
{
    /// <summary>
    /// Class that manages cavity.
    /// </summary>
    public class ServiceCavity : ServiceBase<CavityDb, IAnomalyTrackingUnitOfWork>, IServiceCavity
    {
        public ServiceCavity(IAnomalyTrackingUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public CavityDb Create(CavityDb cavityDb)
        {
            cavityDb.LastModificationDate = DateTime.Now;
            cavityDb = this.unitOfWork.CavityRepo.Add(cavityDb, false);
            return this.Get(cavityDb.Id);
        }

        public CavityDb Update(CavityDb cavity, bool commit = true)
        {
            CavityDb cavityDb = this.Get(cavity.Id);

            cavityDb.Label = cavity.Label;
            cavityDb.LastModificationDate = DateTime.Now;

            this.unitOfWork.CavityRepo.Update(cavityDb, true);

            return this.Get(cavityDb.Id);
        }

        public int Delete(int id)
        {
            CavityDb cavityDb = this.unitOfWork.CavityRepo.GetById(id);
            this.unitOfWork.CavityRepo.Update(cavityDb, true);
            this.unitOfWork.CavityRepo.Delete(id, true);
            return id;
        }

        public CavityDb Get(int id, string includeProperties = "")
        {
            return this.GetAll(c => c.Id == id, includeProperties).Single();
        }

        public IEnumerable<CavityDb> GetAll(Expression<Func<CavityDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true)
        {
            this.filter.IncludeProperties = string.IsNullOrWhiteSpace(includeProperties) ? "MoldDb" : includeProperties;
            this.filter.EntityFilter = filter;
            this.filter.Page = page;
            this.filter.Paginate = paginate;
            this.filter.OrderBy = p => p.OrderByDescending(e => e.LastModificationDate);

            return this.unitOfWork.CavityRepo.GetAll(this.filter).ToList();
        }
    }
}
