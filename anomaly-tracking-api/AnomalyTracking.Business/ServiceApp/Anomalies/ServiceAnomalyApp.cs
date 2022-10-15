using AnomalyTracking.Business.Mappers.Anomalies;
using AnomalyTracking.Business.Service.Anomalies;
using AnomalyTracking.Model.Anomalies;
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

namespace AnomalyTracking.Business.ServiceApp.Anomalies
{
    public class ServiceAnomalyApp : IServiceAnomalyApp
    {
        private readonly AnomalyMapper anomalyMapper;
        private readonly IServiceAnomaly serviceAnomaly;
        private readonly IAnomalyTrackingUnitOfWork unitOfWork;

        public ServiceAnomalyApp(IServiceAnomaly serviceAnomaly, IAnomalyTrackingUnitOfWork unitOfWork)
        {
            this.anomalyMapper = new AnomalyMapper();
            this.unitOfWork = unitOfWork;
            this.serviceAnomaly = serviceAnomaly;
            this.serviceAnomaly.SetContext(this.unitOfWork);
        }

        public Response<Anomaly> Create(Anomaly anomaly)
        {
            try
            {
                CheckAnomalyInfo(anomaly);

                AnomalyDb anomalyDb = this.anomalyMapper.Map(anomaly);

                anomalyDb = this.serviceAnomaly.Create(anomalyDb);

                anomaly = this.anomalyMapper.Map(anomalyDb);

                return new Response<Anomaly>(anomaly, "app.shared.succeededcreate");
            }
            catch (ArgumentException aex)
            {
                return new Response<Anomaly>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Anomaly>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Anomaly> Update(int id, Anomaly anomaly)
        {
            try
            {
                CheckAnomalyInfo(anomaly, id);

                AnomalyDb anomalyDb = this.anomalyMapper.Map(anomaly);

                anomalyDb = this.serviceAnomaly.Update(anomalyDb);

                anomaly = this.anomalyMapper.Map(anomalyDb);

                return new Response<Anomaly>(anomaly, "app.shared.succeededupdate");
            }
            catch (ArgumentException aex)
            {

                return new Response<Anomaly>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Anomaly>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Anomaly> Get(int id)
        {
            try
            {
                AnomalyDb anomalyDb = this.serviceAnomaly.Get(id);

                Anomaly anomaly = this.anomalyMapper.Map(anomalyDb);

                return new Response<Anomaly>(anomaly, "app.shared.succeededget");
            }
            catch (ArgumentException aex)
            {
                return new Response<Anomaly>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);
                return new Response<Anomaly>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<IEnumerable<Anomaly>> GetAll(SearchFilterBase filter)
        {
            try
            {
                IEnumerable<AnomalyDb> anomalyDbs = this.serviceAnomaly.GetAll(this.ApplyFilter(filter), "", filter.Page, filter.Paginate).ToList();

                IEnumerable<Anomaly> anomalies = this.anomalyMapper.Map(anomalyDbs);

                return new Response<IEnumerable<Anomaly>>(anomalies, "app.shared.succeededget", this.serviceAnomaly.GetTotalCount());
            }
            catch (ArgumentException aex)
            {
                return new Response<IEnumerable<Anomaly>>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<IEnumerable<Anomaly>>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<int> Delete(int anomalyId)
        {
            try
            {
                this.unitOfWork.BeginTransaction();
                List<string> paths = new List<string>();

                this.unitOfWork.CommitTransaction();

                return new Response<int>(anomalyId, "app.shared.succeededdelete");
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

                List<int> anomaliesIds = entitiesIds.Select(id => int.Parse(id)).ToList();
                List<string> paths = new List<string>();

                foreach (int anomalyId in anomaliesIds)
                {
                    this.DeleteAnomalyById(anomalyId, paths);
                }

                this.unitOfWork.CommitTransaction();

                return new Response<IEnumerable<int>>(anomaliesIds, "app.shared.succeededdelete");
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

        private Expression<Func<AnomalyDb, bool>> ApplyFilter(SearchFilterBase filter)
        {
            if (filter == null)
            {
                throw new ArgumentException("app.error.invalidfilter");
            }

            bool hasGlobalSearchInput = !string.IsNullOrEmpty(filter.GlobalSearchInput);

            return anomaly =>

                 (!hasGlobalSearchInput   );
        }

        private void DeleteAnomalyById(int anomalyId, List<string> paths)
        {
            Anomaly anomaly = this.anomalyMapper.Map(this.serviceAnomaly.Get(anomalyId));

            AnomalyDb anomalyDb = this.serviceAnomaly.GetAll(c => c.Id == anomalyId).Single();

            this.serviceAnomaly.Delete(anomalyId);
        }

        private static void CheckAnomalyInfo(Anomaly anomaly, int anomalyId = 0)
        {
            ManagementRuleHelper.CheckRequestParameters(anomaly, anomalyId);
        }
    }
}
