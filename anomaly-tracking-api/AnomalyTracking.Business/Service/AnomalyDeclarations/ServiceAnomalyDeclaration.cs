using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.AnomalyDeclarations
{
    /// <summary>
    /// Class that manages anomalyDeclaration.
    /// </summary>
    public class ServiceAnomalyDeclaration : ServiceBase<AnomalyDeclarationDb, IAnomalyTrackingUnitOfWork>, IServiceAnomalyDeclaration
    {
        public ServiceAnomalyDeclaration(IAnomalyTrackingUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public AnomalyDeclarationDb Create(AnomalyDeclarationDb anomalyDeclarationDb)
        {
            anomalyDeclarationDb.LastModificationDate = DateTime.Now;
            anomalyDeclarationDb = this.unitOfWork.AnomalyDeclarationRepo.Add(anomalyDeclarationDb, true);

            return this.Get(anomalyDeclarationDb.Id);
        }

        public AnomalyDeclarationDb Update(AnomalyDeclarationDb anomalyDeclaration, bool commit = true)
        {
            AnomalyDeclarationDb anomalyDeclarationDb = this.Get(anomalyDeclaration.Id);
            //ajouter les attributs
             anomalyDeclarationDb.LastModificationDate = DateTime.Now;
            this.unitOfWork.AnomalyDeclarationRepo.Update(anomalyDeclarationDb, true);

            return this.Get(anomalyDeclarationDb.Id);
        }

        public int Delete(int id)
        {
            AnomalyDeclarationDb anomalyDeclarationDb = this.unitOfWork.AnomalyDeclarationRepo.GetById(id);
            this.unitOfWork.AnomalyDeclarationRepo.Update(anomalyDeclarationDb, true);
            this.unitOfWork.AnomalyDeclarationRepo.Delete(id, true);
            return id;
        }

        public AnomalyDeclarationDb Get(int id, string includeProperties = "")
        {
            return this.GetAll(c => c.Id == id, includeProperties).Single();
        }

        public IEnumerable<AnomalyDeclarationDb> GetAll(Expression<Func<AnomalyDeclarationDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true)
        {
            this.filter.IncludeProperties = string.IsNullOrWhiteSpace(includeProperties) ? "" : includeProperties;
            this.filter.EntityFilter = filter;
            this.filter.Page = page;
            this.filter.Paginate = paginate;
            this.filter.OrderBy = p => p.OrderByDescending(e => e.LastModificationDate);

            return this.unitOfWork.AnomalyDeclarationRepo.GetAll(this.filter).ToList();
        }
    }
}
