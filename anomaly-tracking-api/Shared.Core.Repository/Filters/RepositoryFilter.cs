using System;
using System.Linq;
using System.Linq.Expressions;

namespace Shared.Core.Repository.Filters
{
    /// <summary>
    /// Represents the entity's filter instance.
    /// </summary>
    /// <typeparam name="TEntity">Entity type to retrieve</typeparam>
    public class RepositoryFilter<TEntity> where TEntity : class
    {
        /// <summary>
        /// Creates a new instance of <code>RepositoryFilter</code>.
        /// </summary>
        /// <param name="entityFilter">Defines the filter expression to apply on entities.</param>
        /// <param name="orderBy">Defines the order expression to apply on entities.</param>
        /// <param name="includeProperties">Defines the properties using navifation properties.</param>
        public RepositoryFilter(Expression<Func<TEntity, bool>> entityFilter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            this.Page = 1;
            this.PageSize = 12;
            this.EntityFilter = entityFilter;
            this.OrderBy = orderBy;
            this.IncludeProperties = includeProperties;
            this.Paginate = true;
            this.NoCaching = false;
            this.IsLargeTable = false;
        }

        /// <summary>
        /// Indicates the page index.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Indicates the page count to get.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets the total elements count existing into database.
        /// </summary>
        public int? TotalCount { get; set; }

        /// <summary>
        /// Defines the filter expression to apply on entities.
        /// </summary>
        public Expression<Func<TEntity, bool>> EntityFilter { get; set; }

        /// <summary>
        /// Defines the order expression to apply on entities
        /// </summary>
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; set; }

        /// <summary>
        /// Defines the properties using navifation properties.
        /// </summary>
        public string IncludeProperties { get; set; }

        /// <summary>
        /// Indicates whether or not the request should be paginated.
        /// </summary>
        public bool Paginate { get; set; }

        /// <summary>
        /// Indicates whether or not the EF cache should be used.
        /// </summary>
        public bool NoCaching { get; set; }

        /// <summary>
        /// Indicates whether or not the request concern a large table.
        /// </summary>
        public bool IsLargeTable { get; set; }

        /// <summary>
        /// Contains number of items to retrieve.
        /// </summary>
        public int? ItemsCount { get; set; }
    }
}