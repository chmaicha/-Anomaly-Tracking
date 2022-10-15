using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Molds
{
    /// <summary>
    /// Class that manages mold.
    /// </summary>
    public class ServiceMold : ServiceBase<MoldDb, IAnomalyTrackingUnitOfWork>, IServiceMold
    {
        public ServiceMold(IAnomalyTrackingUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public MoldDb Create(MoldDb moldDb)
        {
            moldDb.LastModificationDate = DateTime.Now;
            moldDb = this.unitOfWork.MoldRepo.Add(moldDb, true);

            return this.Get(moldDb.Id);
        }

        public MoldDb Update(MoldDb mold, bool commit = true)
        {
            MoldDb moldDb = this.Get(mold.Id);

            moldDb.Label = mold.Label;
            moldDb.LastModificationDate = DateTime.Now;
            this.unitOfWork.MoldRepo.Update(moldDb, true);

            return this.Get(moldDb.Id);
        }

        public int Delete(int id)
        {
            MoldDb moldDb = this.unitOfWork.MoldRepo.GetById(id);
            this.unitOfWork.MoldRepo.Update(moldDb, true);
            this.unitOfWork.MoldRepo.Delete(id, true);
            return id;
        }

        public MoldDb Get(int id, string includeProperties = "")
        {
            return this.GetAll(c => c.Id == id, includeProperties).Single();
        }

        public IEnumerable<MoldDb> GetAll(Expression<Func<MoldDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true)
        {
            this.filter.IncludeProperties = string.IsNullOrWhiteSpace(includeProperties) ? "CavityDbs" : includeProperties;
            this.filter.EntityFilter = filter;
            this.filter.Page = page;
            this.filter.Paginate = paginate;
            this.filter.OrderBy = p => p.OrderByDescending(e => e.LastModificationDate);

            return this.unitOfWork.MoldRepo.GetAll(this.filter).ToList();
        }
    }
}
