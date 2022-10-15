using AnomalyTracking.Business.Mappers.Processs;
using AnomalyTracking.Business.Service.Processs;
using AnomalyTracking.Model.Processs;
using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Helpers;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.Repository.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.ServiceApp.Processs
{
    public class ServiceProcessApp : IServiceProcessApp
    {
        private readonly ProcessMapper processMapper;
        private readonly IServiceProcess serviceProcess;
        private readonly IAnomalyTrackingUnitOfWork unitOfWork;

        public ServiceProcessApp(IServiceProcess serviceProcess, IAnomalyTrackingUnitOfWork unitOfWork)
        {
            this.processMapper = new ProcessMapper();
            this.unitOfWork = unitOfWork;
            this.serviceProcess = serviceProcess;
            this.serviceProcess.SetContext(this.unitOfWork);
        }

        public Response<Process> Create(Process process)
        {
            try
            {
                CheckProcessInfo(process);

                ProcessDb processDb = this.processMapper.Map(process);

                processDb = this.serviceProcess.Create(processDb);

                process = this.processMapper.Map(processDb);

                return new Response<Process>(process, "app.shared.succeededcreate");
            }
            catch (ArgumentException aex)
            {
                return new Response<Process>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Process>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Process> Update(int id, Process process)
        {
            try
            {
                CheckProcessInfo(process, id);

                ProcessDb processDb = this.processMapper.Map(process);

                processDb = this.serviceProcess.Update(processDb);

                process = this.processMapper.Map(processDb);

                return new Response<Process>(process, "app.shared.succeededupdate");
            }
            catch (ArgumentException aex)
            {

                return new Response<Process>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Process>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Process> Get(int id)
        {
            try
            {
                ProcessDb processDb = this.serviceProcess.Get(id);

                Process process = this.processMapper.Map(processDb);

                return new Response<Process>(process, "app.shared.succeededget");
            }
            catch (ArgumentException aex)
            {
                return new Response<Process>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);
                return new Response<Process>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<IEnumerable<Process>> GetAll(SearchFilterBase filter)
        {
            try
            {
                IEnumerable<ProcessDb> processDbs = this.serviceProcess.GetAll(this.ApplyFilter(filter), "", filter.Page, filter.Paginate).ToList();

                IEnumerable<Process> processs = this.processMapper.Map(processDbs);

                return new Response<IEnumerable<Process>>(processs, "app.shared.succeededget", this.serviceProcess.GetTotalCount());
            }
            catch (ArgumentException aex)
            {
                return new Response<IEnumerable<Process>>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<IEnumerable<Process>>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<int> Delete(int processId)
        {
            try
            {
                this.unitOfWork.BeginTransaction();
                List<string> paths = new List<string>();

                this.unitOfWork.CommitTransaction();

                return new Response<int>(processId, "app.shared.succeededdelete");
            }
            catch (ArgumentException aex)
            {
                this.unitOfWork.RollbackTransaction();
                return new Response<int>(aex);
            }
            catch (Exception ex)
            {
                this.unitOfWork.RollbackTransaction();
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);
                return new Response<int>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<IEnumerable<int>> DeleteAll(string[] entitiesIds)
        {
            try
            {
                this.unitOfWork.BeginTransaction();

                List<int> processsIds = entitiesIds.Select(id => int.Parse(id)).ToList();
                List<string> paths = new List<string>();

                foreach (int processId in processsIds)
                {
                    this.DeleteProcessById(processId, paths);
                }

                this.unitOfWork.CommitTransaction();

                return new Response<IEnumerable<int>>(processsIds, "app.shared.succeededdelete");
            }
            catch (ArgumentException aex)
            {
                this.unitOfWork.RollbackTransaction();

                return new Response<IEnumerable<int>>(aex);
            }
            catch (Exception ex)
            {
                this.unitOfWork.RollbackTransaction();

                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<IEnumerable<int>>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        private Expression<Func<ProcessDb, bool>> ApplyFilter(SearchFilterBase filter)
        {
            if (filter == null)
            {
                throw new ArgumentException("app.error.invalidfilter");
            }

            bool hasSearchInput = !string.IsNullOrEmpty(filter.SearchInput);

            return process =>

                 (!hasSearchInput ||
                    process.Label.Contains(filter.SearchInput) 

                );
        }

        private void DeleteProcessById(int processId, List<string> paths)
        {
            Process process = this.processMapper.Map(this.serviceProcess.Get(processId));

            ProcessDb processDb = this.serviceProcess.GetAll(c => c.Id == processId).Single();

            this.serviceProcess.Delete(processId);
        }

        private static void CheckProcessInfo(Process process, int processId = 0)
        {
            ManagementRuleHelper.CheckRequestParameters(process, processId);
        }
    }
}
