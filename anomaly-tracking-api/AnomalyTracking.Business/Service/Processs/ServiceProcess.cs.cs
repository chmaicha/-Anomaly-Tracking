using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Processs
{
    /// <summary>
    /// Class that manages process.
    /// </summary>
    public class ServiceProcess : ServiceBase<ProcessDb, IAnomalyTrackingUnitOfWork>, IServiceProcess
    {
        public ServiceProcess(IAnomalyTrackingUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ProcessDb Create(ProcessDb processDb)
        {
            processDb.LastModificationDate = DateTime.Now;
            processDb = this.unitOfWork.ProcessRepo.Add(processDb, true);

            return this.Get(processDb.Id);
        }

        public ProcessDb Update(ProcessDb process, bool commit = true)
        {
            ProcessDb processDb = this.Get(process.Id);
            processDb.Label = process.Label;
            processDb.LastModificationDate = DateTime.Now;
            this.unitOfWork.ProcessRepo.Update(processDb, true);

            return this.Get(processDb.Id);
        }

        public int Delete(int id)
        {
            ProcessDb processDb = this.unitOfWork.ProcessRepo.GetById(id);
            this.unitOfWork.ProcessRepo.Update(processDb, true);
            this.unitOfWork.ProcessRepo.Delete(id, true);
            return id;
        }

        public ProcessDb Get(int id, string includeProperties = "")
        {
            return this.GetAll(c => c.Id == id, includeProperties).Single();
        }

        public IEnumerable<ProcessDb> GetAll(Expression<Func<ProcessDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true)
        {
            this.filter.IncludeProperties = string.IsNullOrWhiteSpace(includeProperties) ? "AnomalyTypeDbs,AnomalydeclarationDbs" : includeProperties;
            this.filter.EntityFilter = filter;
            this.filter.Page = page;
            this.filter.Paginate = paginate;
            this.filter.OrderBy = p => p.OrderByDescending(e => e.LastModificationDate);

            return this.unitOfWork.ProcessRepo.GetAll(this.filter).ToList();
        }
    }
}
