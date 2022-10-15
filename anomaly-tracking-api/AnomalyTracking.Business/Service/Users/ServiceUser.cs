using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Users
{
    /// <summary>
    /// Class that manages user.
    /// </summary>
    public class ServiceUser : ServiceBase<UserDb, IAnomalyTrackingUnitOfWork>, IServiceUser
    {
        public ServiceUser(IAnomalyTrackingUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public UserDb Create(UserDb userDb)
        {
            userDb.LastModificationDate = DateTime.Now;
            userDb = this.unitOfWork.UserRepo.Add(userDb, true);

            return this.Get(userDb.Id);
        }

        public UserDb Update(UserDb user, bool commit = true)
        {
            UserDb userDb = this.Get(user.Id);

            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Code = user.Code;
            userDb.Password = user.Password;
            userDb.LvfUserRole = user.LvfUserRole;
            userDb.LastModificationDate = DateTime.Now;
            this.unitOfWork.UserRepo.Update(userDb, true);

            return this.Get(userDb.Id);
        }

        public int Delete(int id)
        {
            UserDb userDb = this.unitOfWork.UserRepo.GetById(id);
            this.unitOfWork.UserRepo.Update(userDb, true);
            this.unitOfWork.UserRepo.Delete(id, true);
            return id;
        }

        public UserDb Get(int id, string includeProperties = "")
        {
            return this.GetAll(c => c.Id == id, includeProperties).Single();
        }

        public IEnumerable<UserDb> GetAll(Expression<Func<UserDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true)
        {
            this.filter.IncludeProperties = string.IsNullOrWhiteSpace(includeProperties) ? "" : includeProperties;
            this.filter.EntityFilter = filter;
            this.filter.Page = page;
            this.filter.Paginate = paginate;
            this.filter.OrderBy = p => p.OrderByDescending(e => e.LastModificationDate);

            return this.unitOfWork.UserRepo.GetAll(this.filter).ToList();
        }
    }
}
