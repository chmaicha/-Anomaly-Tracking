using Shared.Core.Repository.Filters;
using System;
using System.Linq;

namespace Shared.Core.Business.Services
{
    public abstract class ServiceBase<TEntity, TUnitOfWork> : IServiceBase<TEntity, TUnitOfWork> where TEntity : class
    {
        protected TUnitOfWork unitOfWork;
        protected readonly RepositoryFilter<TEntity> filter;

        /// <summary>
        /// Creates a new instance of ServiceBase<TEntity>.
        /// </summary>
        /// <param name="unitOfWork">Unit of work instance</param>
        public ServiceBase(TUnitOfWork unitOfWork)
            : this()
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceBase(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            this.filter = new RepositoryFilter<TEntity>(null, orderBy);
        }

        public void SetContext(TUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int? GetTotalCount()
        {
            return filter.TotalCount;
        }
    }
}