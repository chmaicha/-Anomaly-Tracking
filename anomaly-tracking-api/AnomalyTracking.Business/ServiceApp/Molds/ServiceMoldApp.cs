using AnomalyTracking.Business.Mappers.Molds;
using AnomalyTracking.Business.Service.Molds;
using AnomalyTracking.Model.Molds;
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

namespace AnomalyTracking.Business.ServiceApp.Molds
{
    public class ServiceMoldApp : IServiceMoldApp
    {
        private readonly MoldMapper moldMapper;
        private readonly IServiceMold serviceMold;
        private readonly IAnomalyTrackingUnitOfWork unitOfWork;

        public ServiceMoldApp(IServiceMold serviceMold, IAnomalyTrackingUnitOfWork unitOfWork)
        {
            this.moldMapper = new MoldMapper();
            this.unitOfWork = unitOfWork;
            this.serviceMold = serviceMold;
            this.serviceMold.SetContext(this.unitOfWork);
        }

        public Response<Mold> Create(Mold mold)
        {
            try
            {
                CheckMoldInfo(mold);

                MoldDb moldDb = this.moldMapper.Map(mold);

                moldDb = this.serviceMold.Create(moldDb);

                mold = this.moldMapper.Map(moldDb);

                return new Response<Mold>(mold, "app.shared.succeededcreate");
            }
            catch (ArgumentException aex)
            {
                return new Response<Mold>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Mold>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Mold> Update(int id, Mold mold)
        {
            try
            {
                CheckMoldInfo(mold, id);

                MoldDb moldDb = this.moldMapper.Map(mold);
               
                moldDb = this.serviceMold.Update(moldDb);

                mold = this.moldMapper.Map(moldDb);

                return new Response<Mold>(mold, "app.shared.succeededupdate");
            }
            catch (ArgumentException aex)
            {

                return new Response<Mold>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Mold>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Mold> Get(int id)
        {
            try
            {
                MoldDb moldDb = this.serviceMold.Get(id);

                Mold mold = this.moldMapper.Map(moldDb);

                return new Response<Mold>(mold, "app.shared.succeededget");
            }
            catch (ArgumentException aex)
            {
                return new Response<Mold>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);
                return new Response<Mold>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<IEnumerable<Mold>> GetAll(SearchFilterBase filter)
        {
            try
            {
                IEnumerable<MoldDb> moldDbs = this.serviceMold.GetAll(this.ApplyFilter(filter), "", filter.Page, filter.Paginate).ToList();

                IEnumerable<Mold> molds = this.moldMapper.Map(moldDbs);

                return new Response<IEnumerable<Mold>>(molds, "app.shared.succeededget", this.serviceMold.GetTotalCount());
            }
            catch (ArgumentException aex)
            {
                return new Response<IEnumerable<Mold>>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<IEnumerable<Mold>>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<int> Delete(int moldId)
        {
            try
            {
                this.unitOfWork.BeginTransaction();
                List<string> paths = new List<string>();

                this.unitOfWork.CommitTransaction();

                return new Response<int>(moldId, "app.shared.succeededdelete");
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

                List<int> moldsIds = entitiesIds.Select(id => int.Parse(id)).ToList();
                List<string> paths = new List<string>();

                foreach (int moldId in moldsIds)
                {
                    this.DeleteMoldById(moldId, paths);
                }

                this.unitOfWork.CommitTransaction();

                return new Response<IEnumerable<int>>(moldsIds, "app.shared.succeededdelete");
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

        private Expression<Func<MoldDb, bool>> ApplyFilter(SearchFilterBase filter)
        {
            if (filter == null)
            {
                throw new ArgumentException("app.error.invalidfilter");
            }

            bool hasGlobalSearchInput = !string.IsNullOrEmpty(filter.GlobalSearchInput);

            return mold =>

                 (!hasGlobalSearchInput ||
                    mold.Label.Contains(filter.GlobalSearchInput)
                );
        }

        private void DeleteMoldById(int moldId, List<string> paths)
        {
            Mold mold = this.moldMapper.Map(this.serviceMold.Get(moldId));

            MoldDb moldDb = this.serviceMold.GetAll(c => c.Id == moldId).Single();

            this.serviceMold.Delete(moldId);
        }

        private static void CheckMoldInfo(Mold mold, int moldId = 0)
        {
            ManagementRuleHelper.CheckRequestParameters(mold, moldId);
        }
    }
}
