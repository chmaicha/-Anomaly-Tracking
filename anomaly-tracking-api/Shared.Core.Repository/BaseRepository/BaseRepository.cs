using Shared.Core.Repository.Context;
using Shared.Core.Repository.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Shared.Core.Repository.Base
{
    /// <summary>
    /// Implementation of <code>IBaseRepository</code> interfance based on entity frameword <code>DbContext</code> decorated by the internal <code>ICoreDbContext</code>.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity to manipulate</typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly IDbSet<TEntity> dbSet;
        private readonly ICoreDbContext dbContext;

        /// <summary>
        /// Create a new instance of <code>BaseRepository</code>.
        /// </summary>
        /// <param name="dbContext">Context de la base de données</param>
        public BaseRepository(ICoreDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.GetIDbSet<TEntity>();
        }

        /// </summary>
        public virtual IEnumerable<TEntity> GetAll(RepositoryFilter<TEntity> filter)
        {
            if (filter == null) throw new ArgumentNullException("app.error.invalidfilter");

            IQueryable<TEntity> query = this.dbSet;

            if (filter.EntityFilter != null)
            {
                query = query.Where(filter.EntityFilter);
            }

            if (!string.IsNullOrWhiteSpace(filter.IncludeProperties) &&
                !filter.IncludeProperties.Equals("none", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var includeProperty in filter.IncludeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            // DO NOT REMOVE THIS
            //filter.TotalCount = this.dbContext.DbInstance().SqlQuery<int>($"SELECT COUNT(*) FROM {typeof(TEntity).Name}").FirstOrDefault();

            filter.TotalCount = !filter.IsLargeTable ?
                query.Count() :
                this.dbContext.DbInstance().SqlQuery<int>($"SELECT COUNT(*) FROM {typeof(TEntity).Name}").FirstOrDefault();

            if (filter.ItemsCount.HasValue)
            {
                return query.Take(filter.ItemsCount.Value).ToList();
            }

            if (filter.Paginate && filter.OrderBy != null)
            {
                int skip = (filter.Page - 1) * filter.PageSize;

                return filter.NoCaching ?
                    filter.OrderBy(query).Skip(skip).Take(filter.PageSize).AsNoTracking().ToList() :
                    filter.OrderBy(query).Skip(skip).Take(filter.PageSize).ToList();
            }

            if (filter.NoCaching)
            {
                return query.AsNoTracking().ToList();
            }

            return query.ToList();
        }

        /// </summary>
        public virtual TEntity GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        /// </summary>
        public virtual TEntity Add(TEntity entity, bool commit)
        {
            TEntity created = this.dbSet.Add(entity);
            if (commit)
            {
                this.dbContext.SaveChanges();
            }
            return created;
        }

        /// </summary>
        public virtual void Update(TEntity entityToUpdate, bool commit)
        {
            this.dbSet.Attach(entityToUpdate);
            this.dbContext.SetModified(entityToUpdate);
            if (commit)
            {
                this.dbContext.SaveChanges();
            }
        }

        /// </summary>
        public virtual TEntity Delete(TEntity entityToDelete, bool commit)
        {
            if (this.dbContext.IsInState(entityToDelete, EntityState.Detached))
            {
                this.dbSet.Attach(entityToDelete);
            }

            TEntity removed = this.dbSet.Remove(entityToDelete);
            if (commit)
            {
                this.dbContext.SaveChanges();
            }
            return removed;
        }

        /// </summary>
        public virtual TEntity Delete(int id, bool commit)
        {
            TEntity entityToDelete = this.dbSet.Find(id);
            return Delete(entityToDelete, commit);
        }
    }
}