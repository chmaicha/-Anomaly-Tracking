using AnomalyTracking.Business.Mappers.Faces;
using AnomalyTracking.Business.Service.Faces;
using AnomalyTracking.Model.Faces;
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

namespace AnomalyTracking.Business.ServiceApp.Faces
{
    public class ServiceFaceApp : IServiceFaceApp
    {
        private readonly FaceMapper faceMapper;
        private readonly IServiceFace serviceFace;
        private readonly IAnomalyTrackingUnitOfWork unitOfWork;

        public ServiceFaceApp(IServiceFace serviceFace, IAnomalyTrackingUnitOfWork unitOfWork)
        {
            this.faceMapper = new FaceMapper();
            this.unitOfWork = unitOfWork;
            this.serviceFace = serviceFace;
            this.serviceFace.SetContext(this.unitOfWork);
        }

        public Response<Face> Create(Face face)
        {
            try
            {
                CheckFaceInfo(face);

                FaceDb faceDb = this.faceMapper.Map(face);

                faceDb = this.serviceFace.Create(faceDb);

                face = this.faceMapper.Map(faceDb);

                return new Response<Face>(face, "app.shared.succeededcreate");
            }
            catch (ArgumentException aex)
            {
                return new Response<Face>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Face>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Face> Update(int id, Face face)
        {
            try
            {
                CheckFaceInfo(face, id);

                FaceDb faceDb = this.faceMapper.Map(face);

                faceDb = this.serviceFace.Update(faceDb);

                face = this.faceMapper.Map(faceDb);

                return new Response<Face>(face, "app.shared.succeededupdate");
            }
            catch (ArgumentException aex)
            {

                return new Response<Face>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Face>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Face> Get(int id)
        {
            try
            {
                FaceDb faceDb = this.serviceFace.Get(id);

                Face face = this.faceMapper.Map(faceDb);

                return new Response<Face>(face, "app.shared.succeededget");
            }
            catch (ArgumentException aex)
            {
                return new Response<Face>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);
                return new Response<Face>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<IEnumerable<Face>> GetAll(SearchFilterBase filter)
        {
            try
            {
                IEnumerable<FaceDb> faceDbs = this.serviceFace.GetAll(this.ApplyFilter(filter), "", filter.Page, filter.Paginate).ToList();

                IEnumerable<Face> faces = this.faceMapper.Map(faceDbs);

                return new Response<IEnumerable<Face>>(faces, "app.shared.succeededget", this.serviceFace.GetTotalCount());
            }
            catch (ArgumentException aex)
            {
                return new Response<IEnumerable<Face>>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<IEnumerable<Face>>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<int> Delete(int faceId)
        {
            try
            {
                this.unitOfWork.BeginTransaction();
                List<string> paths = new List<string>();

                this.unitOfWork.CommitTransaction();

                return new Response<int>(faceId, "app.shared.succeededdelete");
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

                List<int> facesIds = entitiesIds.Select(id => int.Parse(id)).ToList();
                List<string> paths = new List<string>();

                foreach (int faceId in facesIds)
                {
                    this.DeleteFaceById(faceId, paths);
                }

                this.unitOfWork.CommitTransaction();

                return new Response<IEnumerable<int>>(facesIds, "app.shared.succeededdelete");
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

        private Expression<Func<FaceDb, bool>> ApplyFilter(SearchFilterBase filter)
        {
            if (filter == null)
            {
                throw new ArgumentException("app.error.invalidfilter");
            }

            bool hasGlobalSearchInput = !string.IsNullOrEmpty(filter.GlobalSearchInput);

            return face =>

                 (!hasGlobalSearchInput ||
                    face.Label.Contains(filter.GlobalSearchInput)
                );
        }

        private void DeleteFaceById(int faceId, List<string> paths)
        {
            Face face = this.faceMapper.Map(this.serviceFace.Get(faceId));

            FaceDb faceDb = this.serviceFace.GetAll(c => c.Id == faceId).Single();

            this.serviceFace.Delete(faceId);
        }

        private static void CheckFaceInfo(Face face, int faceId = 0)
        {
            ManagementRuleHelper.CheckRequestParameters(face, faceId);
        }
    }
}
