using AnomalyTracking.Business.Mappers.AnomalyTypes;
using AnomalyTracking.Business.Service.AnomalyTypes;
using AnomalyTracking.Model.AnomalyTypes;
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

namespace AnomalyTracking.Business.ServiceApp.AnomalyTypes
{
    public class ServiceAnomalyTypeApp : IServiceAnomalyTypeApp
    {
        private readonly AnomalyTypeMapper anomalyTypeMapper;
        private readonly IServiceAnomalyType serviceAnomalyType;
        private readonly IAnomalyTrackingUnitOfWork unitOfWork;

        public ServiceAnomalyTypeApp(IServiceAnomalyType serviceAnomalyType, IAnomalyTrackingUnitOfWork unitOfWork)
        {
            this.anomalyTypeMapper = new AnomalyTypeMapper();
            this.unitOfWork = unitOfWork;
            this.serviceAnomalyType = serviceAnomalyType;
            this.serviceAnomalyType.SetContext(this.unitOfWork);
        }

        public Response<AnomalyType> Create(AnomalyType anomalyType)
        {
            try
            {
                CheckAnomalyTypeInfo(anomalyType);

                AnomalyTypeDb anomalyTypeDb = this.anomalyTypeMapper.Map(anomalyType);

                anomalyTypeDb = this.serviceAnomalyType.Create(anomalyTypeDb);

                anomalyType = this.anomalyTypeMapper.Map(anomalyTypeDb);

                return new Response<AnomalyType>(anomalyType, "app.shared.succeededcreate");
            }
            catch (ArgumentException aex)
            {
                return new Response<AnomalyType>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<AnomalyType>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<AnomalyType> Update(int id, AnomalyType anomalyType)
        {
            try
            {
                CheckAnomalyTypeInfo(anomalyType, id);

                AnomalyTypeDb anomalyTypeDb = this.anomalyTypeMapper.Map(anomalyType);

                anomalyTypeDb = this.serviceAnomalyType.Update(anomalyTypeDb);

                anomalyType = this.anomalyTypeMapper.Map(anomalyTypeDb);

                return new Response<AnomalyType>(anomalyType, "app.shared.succeededupdate");
            }
            catch (ArgumentException aex)
            {

                return new Response<AnomalyType>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<AnomalyType>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<AnomalyType> Get(int id)
        {
            try
            {
                AnomalyTypeDb anomalyTypeDb = this.serviceAnomalyType.Get(id);

                AnomalyType anomalyType = this.anomalyTypeMapper.Map(anomalyTypeDb);

                return new Response<AnomalyType>(anomalyType, "app.shared.succeededget");
            }
            catch (ArgumentException aex)
            {
                return new Response<AnomalyType>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);
                return new Response<AnomalyType>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<IEnumerable<AnomalyType>> GetAll(SearchFilterBase filter)
        {
            try
            {
                IEnumerable<AnomalyTypeDb> anomalyTypeDbs = this.serviceAnomalyType.GetAll(this.ApplyFilter(filter), "", filter.Page, filter.Paginate).ToList();

                IEnumerable<AnomalyType> anomalyTypes = this.anomalyTypeMapper.Map(anomalyTypeDbs);

                return new Response<IEnumerable<AnomalyType>>(anomalyTypes, "app.shared.succeededget", this.serviceAnomalyType.GetTotalCount());
            }
            catch (ArgumentException aex)
            {
                return new Response<IEnumerable<AnomalyType>>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<IEnumerable<AnomalyType>>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<int> Delete(int anomalyTypeId)
        {
            try
            {
                this.unitOfWork.BeginTransaction();
                List<string> paths = new List<string>();

                this.unitOfWork.CommitTransaction();

                return new Response<int>(anomalyTypeId, "app.shared.succeededdelete");
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

                List<int> anomalyTypesIds = entitiesIds.Select(id => int.Parse(id)).ToList();
                List<string> paths = new List<string>();

                foreach (int anomalyTypeId in anomalyTypesIds)
                {
                    this.DeleteAnomalyTypeById(anomalyTypeId, paths);
                }

                this.unitOfWork.CommitTransaction();

                return new Response<IEnumerable<int>>(anomalyTypesIds, "app.shared.succeededdelete");
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

        private Expression<Func<AnomalyTypeDb, bool>> ApplyFilter(SearchFilterBase filter)
        {
            if (filter == null)
            {
                throw new ArgumentException("app.error.invalidfilter");
            }

            bool hasGlobalSearchInput = !string.IsNullOrEmpty(filter.GlobalSearchInput);

            return anomalyType =>

                 (!hasGlobalSearchInput ||
                    anomalyType.Label.Contains(filter.GlobalSearchInput)
                );
        }

        private void DeleteAnomalyTypeById(int anomalyTypeId, List<string> paths)
        {
            AnomalyType anomalyType = this.anomalyTypeMapper.Map(this.serviceAnomalyType.Get(anomalyTypeId));

            AnomalyTypeDb anomalyTypeDb = this.serviceAnomalyType.GetAll(c => c.Id == anomalyTypeId).Single();

            this.serviceAnomalyType.Delete(anomalyTypeId);
        }

        private static void CheckAnomalyTypeInfo(AnomalyType anomalyType, int anomalyTypeId = 0)
        {
            ManagementRuleHelper.CheckRequestParameters(anomalyType, anomalyTypeId);
        }
    }
}
