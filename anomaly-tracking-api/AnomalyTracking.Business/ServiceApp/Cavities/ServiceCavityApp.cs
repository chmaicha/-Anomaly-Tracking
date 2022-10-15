using AnomalyTracking.Business.Mappers.Cavities;
using AnomalyTracking.Business.Service.Cavities;
using AnomalyTracking.Model.Cavities;
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

namespace AnomalyTracking.Business.ServiceApp.Cavities
{
    public class ServiceCavityApp : IServiceCavityApp
    {
        private readonly CavityMapper cavityMapper;
        private readonly IServiceCavity serviceCavity;
        private readonly IAnomalyTrackingUnitOfWork unitOfWork;

        public ServiceCavityApp(IServiceCavity serviceCavity, IAnomalyTrackingUnitOfWork unitOfWork)
        {
            this.cavityMapper = new CavityMapper();
            this.unitOfWork = unitOfWork;
            this.serviceCavity = serviceCavity;
            this.serviceCavity.SetContext(this.unitOfWork);
        }

        public Response<Cavity> Create(Cavity cavity)
        {
            try
            {
                CheckCavityInfo(cavity);

                CavityDb cavityDb = this.cavityMapper.Map(cavity);

                cavityDb = this.serviceCavity.Create(cavityDb);

                cavity = this.cavityMapper.Map(cavityDb);

                return new Response<Cavity>(cavity, "app.shared.succeededcreate");
            }
            catch (ArgumentException aex)
            {
                return new Response<Cavity>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Cavity>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Cavity> Update(int id, Cavity cavity)
        {
            try
            {
                CheckCavityInfo(cavity, id);

                CavityDb cavityDb = this.cavityMapper.Map(cavity);

                cavityDb = this.serviceCavity.Update(cavityDb);

                cavity = this.cavityMapper.Map(cavityDb);

                return new Response<Cavity>(cavity, "app.shared.succeededupdate");
            }
            catch (ArgumentException aex)
            {

                return new Response<Cavity>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Cavity>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Cavity> Get(int id)
        {
            try
            {
                CavityDb cavityDb = this.serviceCavity.Get(id);

                Cavity cavity = this.cavityMapper.Map(cavityDb);

                return new Response<Cavity>(cavity, "app.shared.succeededget");
            }
            catch (ArgumentException aex)
            {
                return new Response<Cavity>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);
                return new Response<Cavity>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<IEnumerable<Cavity>> GetAll(SearchFilterBase filter)
        {
            try
            {
                IEnumerable<CavityDb> cavityDbs = this.serviceCavity.GetAll(this.ApplyFilter(filter), "", filter.Page, filter.Paginate).ToList();

                IEnumerable<Cavity> cavities = this.cavityMapper.Map(cavityDbs);

                return new Response<IEnumerable<Cavity>>(cavities, "app.shared.succeededget", this.serviceCavity.GetTotalCount());
            }
            catch (ArgumentException aex)
            {
                return new Response<IEnumerable<Cavity>>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<IEnumerable<Cavity>>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<int> Delete(int cavityId)
        {
            try
            {
                this.unitOfWork.BeginTransaction();
                List<string> paths = new List<string>();

                this.unitOfWork.CommitTransaction();

                return new Response<int>(cavityId, "app.shared.succeededdelete");
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

                List<int> cavitiesIds = entitiesIds.Select(id => int.Parse(id)).ToList();
                List<string> paths = new List<string>();

                foreach (int cavityId in cavitiesIds)
                {
                    this.DeleteCavityById(cavityId, paths);
                }

                this.unitOfWork.CommitTransaction();

                return new Response<IEnumerable<int>>(cavitiesIds, "app.shared.succeededdelete");
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

        private Expression<Func<CavityDb, bool>> ApplyFilter(SearchFilterBase filter)
        {
            if (filter == null)
            {
                throw new ArgumentException("app.error.invalidfilter");
            }

            bool hasGlobalSearchInput = !string.IsNullOrEmpty(filter.GlobalSearchInput);

            return cavity =>

                 (!hasGlobalSearchInput ||
                    cavity.Label.Contains(filter.GlobalSearchInput)
                );
        }

        private void DeleteCavityById(int cavityId, List<string> paths)
        {
            Cavity cavity = this.cavityMapper.Map(this.serviceCavity.Get(cavityId));

            CavityDb cavityDb = this.serviceCavity.GetAll(c => c.Id == cavityId).Single();

            this.serviceCavity.Delete(cavityId);
        }

        private static void CheckCavityInfo(Cavity cavity, int cavityId = 0)
        {
            ManagementRuleHelper.CheckRequestParameters(cavity, cavityId);
        }
    }
}
