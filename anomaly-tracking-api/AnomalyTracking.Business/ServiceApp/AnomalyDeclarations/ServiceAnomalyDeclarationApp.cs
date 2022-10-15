using AnomalyTracking.Business.Mappers.AnomalyDeclarations;
using AnomalyTracking.Business.Service.AnomalyDeclarations;
using AnomalyTracking.Model.AnomalyDeclarations;
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

namespace AnomalyTracking.Business.ServiceApp.AnomalyDeclarations
{
    public class ServiceAnomalyDeclarationApp : IServiceAnomalyDeclarationApp
    {
        private readonly AnomalyDeclarationMapper anomalyDeclarationMapper;
        private readonly IServiceAnomalyDeclaration serviceAnomalyDeclaration;
        private readonly IAnomalyTrackingUnitOfWork unitOfWork;

        public ServiceAnomalyDeclarationApp(IServiceAnomalyDeclaration serviceAnomalyDeclaration, IAnomalyTrackingUnitOfWork unitOfWork)
        {
            this.anomalyDeclarationMapper = new AnomalyDeclarationMapper();
            this.unitOfWork = unitOfWork;
            this.serviceAnomalyDeclaration = serviceAnomalyDeclaration;
            this.serviceAnomalyDeclaration.SetContext(this.unitOfWork);
        }

        public Response<AnomalyDeclaration> Create(AnomalyDeclaration anomalyDeclaration)
        {
            try
            {
                CheckAnomalyDeclarationInfo(anomalyDeclaration);

                AnomalyDeclarationDb anomalyDeclarationDb = this.anomalyDeclarationMapper.Map(anomalyDeclaration);

                anomalyDeclarationDb = this.serviceAnomalyDeclaration.Create(anomalyDeclarationDb);

                anomalyDeclaration = this.anomalyDeclarationMapper.Map(anomalyDeclarationDb);

                return new Response<AnomalyDeclaration>(anomalyDeclaration, "app.shared.succeededcreate");
            }
            catch (ArgumentException aex)
            {
                return new Response<AnomalyDeclaration>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<AnomalyDeclaration>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<AnomalyDeclaration> Update(int id, AnomalyDeclaration anomalyDeclaration)
        {
            try
            {
                CheckAnomalyDeclarationInfo(anomalyDeclaration, id);

                AnomalyDeclarationDb anomalyDeclarationDb = this.anomalyDeclarationMapper.Map(anomalyDeclaration);

                anomalyDeclarationDb = this.serviceAnomalyDeclaration.Update(anomalyDeclarationDb);

                anomalyDeclaration = this.anomalyDeclarationMapper.Map(anomalyDeclarationDb);

                return new Response<AnomalyDeclaration>(anomalyDeclaration, "app.shared.succeededupdate");
            }
            catch (ArgumentException aex)
            {

                return new Response<AnomalyDeclaration>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<AnomalyDeclaration>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<AnomalyDeclaration> Get(int id)
        {
            try
            {
                AnomalyDeclarationDb anomalyDeclarationDb = this.serviceAnomalyDeclaration.Get(id);

                AnomalyDeclaration anomalyDeclaration = this.anomalyDeclarationMapper.Map(anomalyDeclarationDb);

                return new Response<AnomalyDeclaration>(anomalyDeclaration, "app.shared.succeededget");
            }
            catch (ArgumentException aex)
            {
                return new Response<AnomalyDeclaration>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);
                return new Response<AnomalyDeclaration>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<IEnumerable<AnomalyDeclaration>> GetAll(SearchFilterBase filter)
        {
            try
            {
                IEnumerable<AnomalyDeclarationDb> anomalyDeclarationDbs = this.serviceAnomalyDeclaration.GetAll(this.ApplyFilter(filter), "", filter.Page, filter.Paginate).ToList();

                IEnumerable<AnomalyDeclaration> anomalyDeclarations = this.anomalyDeclarationMapper.Map(anomalyDeclarationDbs);

                return new Response<IEnumerable<AnomalyDeclaration>>(anomalyDeclarations, "app.shared.succeededget", this.serviceAnomalyDeclaration.GetTotalCount());
            }
            catch (ArgumentException aex)
            {
                return new Response<IEnumerable<AnomalyDeclaration>>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<IEnumerable<AnomalyDeclaration>>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<int> Delete(int anomalyDeclarationId)
        {
            try
            {
                this.unitOfWork.BeginTransaction();
                List<string> paths = new List<string>();

                this.unitOfWork.CommitTransaction();

                return new Response<int>(anomalyDeclarationId, "app.shared.succeededdelete");
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

                List<int> anomalyDeclarationsIds = entitiesIds.Select(id => int.Parse(id)).ToList();
                List<string> paths = new List<string>();

                foreach (int anomalyDeclarationId in anomalyDeclarationsIds)
                {
                    this.DeleteAnomalyDeclarationById(anomalyDeclarationId, paths);
                }

                this.unitOfWork.CommitTransaction();

                return new Response<IEnumerable<int>>(anomalyDeclarationsIds, "app.shared.succeededdelete");
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

        private Expression<Func<AnomalyDeclarationDb, bool>> ApplyFilter(SearchFilterBase filter)
        {
            if (filter == null)
            {
                throw new ArgumentException("app.error.invalidfilter");
            }

            bool hasGlobalSearchInput = !string.IsNullOrEmpty(filter.GlobalSearchInput);

            return anomalyDeclaration =>

                 (!hasGlobalSearchInput ||
                    anomalyDeclaration.UserDb.FirstName.Contains(filter.GlobalSearchInput)
                );
        }

        private void DeleteAnomalyDeclarationById(int anomalyDeclarationId, List<string> paths)
        {
            AnomalyDeclaration anomalyDeclaration = this.anomalyDeclarationMapper.Map(this.serviceAnomalyDeclaration.Get(anomalyDeclarationId));

            AnomalyDeclarationDb anomalyDeclarationDb = this.serviceAnomalyDeclaration.GetAll(c => c.Id == anomalyDeclarationId).Single();

            this.serviceAnomalyDeclaration.Delete(anomalyDeclarationId);
        }

        private static void CheckAnomalyDeclarationInfo(AnomalyDeclaration anomalyDeclaration, int anomalyDeclarationId = 0)
        {
            ManagementRuleHelper.CheckRequestParameters(anomalyDeclaration, anomalyDeclarationId);
        }
    }
}
