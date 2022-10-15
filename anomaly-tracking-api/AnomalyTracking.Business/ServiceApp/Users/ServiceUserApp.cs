using AnomalyTracking.Business.Mappers.Users;
using AnomalyTracking.Business.Service.Users;
using AnomalyTracking.Model.Users;
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

namespace AnomalyTracking.Business.ServiceApp.Users
{
    public class ServiceUserApp : IServiceUserApp
    {
        private readonly UserMapper userMapper;
        private readonly IServiceUser serviceUser;
        private readonly IAnomalyTrackingUnitOfWork unitOfWork;

        public ServiceUserApp(IServiceUser serviceUser, IAnomalyTrackingUnitOfWork unitOfWork)
        {
            this.userMapper = new UserMapper();
            this.unitOfWork = unitOfWork;
            this.serviceUser = serviceUser;
            this.serviceUser.SetContext(this.unitOfWork);
        }

        public Response<User> Create(User user)
        {
            try
            {
                CheckUserInfo(user);

                UserDb userDb = this.userMapper.Map(user);

                userDb = this.serviceUser.Create(userDb);

                user = this.userMapper.Map(userDb);

                return new Response<User>(user, "app.shared.succeededcreate");
            }
            catch (ArgumentException aex)
            {
                return new Response<User>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<User>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<User> Update(int id, User user)
        {
            try
            {
                CheckUserInfo(user, id);

                UserDb userDb = this.userMapper.Map(user);

                userDb = this.serviceUser.Update(userDb);

                user = this.userMapper.Map(userDb);

                return new Response<User>(user, "app.shared.succeededupdate");
            }
            catch (ArgumentException aex)
            {

                return new Response<User>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<User>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<User> Get(int id)
        {
            try
            {
                UserDb userDb = this.serviceUser.Get(id);

                User user = this.userMapper.Map(userDb);

                return new Response<User>(user, "app.shared.succeededget");
            }
            catch (ArgumentException aex)
            {
                return new Response<User>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);
                return new Response<User>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<IEnumerable<User>> GetAll(SearchFilterBase filter)
        {
            try
            {
                IEnumerable<UserDb> userDbs = this.serviceUser.GetAll(this.ApplyFilter(filter), "", filter.Page, filter.Paginate).ToList();

                IEnumerable<User> users = this.userMapper.Map(userDbs);

                return new Response<IEnumerable<User>>(users, "app.shared.succeededget", this.serviceUser.GetTotalCount());
            }
            catch (ArgumentException aex)
            {
                return new Response<IEnumerable<User>>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<IEnumerable<User>>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<int> Delete(int userId)
        {
            try
            {
                this.unitOfWork.BeginTransaction();
                List<string> paths = new List<string>();

                this.unitOfWork.CommitTransaction();

                return new Response<int>(userId, "app.shared.succeededdelete");
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

                List<int> usersIds = entitiesIds.Select(id => int.Parse(id)).ToList();
                List<string> paths = new List<string>();

                foreach (int userId in usersIds)
                {
                    this.DeleteUserById(userId, paths);
                }

                this.unitOfWork.CommitTransaction();

                return new Response<IEnumerable<int>>(usersIds, "app.shared.succeededdelete");
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

        private Expression<Func<UserDb, bool>> ApplyFilter(SearchFilterBase filter)
        {
            if (filter == null)
            {
                throw new ArgumentException("app.error.invalidfilter");
            }

            bool hasSearchInput = !string.IsNullOrEmpty(filter.SearchInput);

            return user =>

                 (!hasSearchInput ||
                    user.FirstName.Contains(filter.SearchInput) ||
                    user.LastName.Contains(filter.SearchInput) ||
                    user.Code.Contains(filter.SearchInput)

                );
        }

        private void DeleteUserById(int userId, List<string> paths)
        {
            User user = this.userMapper.Map(this.serviceUser.Get(userId));

            UserDb userDb = this.serviceUser.GetAll(c => c.Id == userId).Single();

            this.serviceUser.Delete(userId);
        }

        private static void CheckUserInfo(User user, int userId = 0)
        {
            ManagementRuleHelper.CheckRequestParameters(user, userId);
        }
    }
}
