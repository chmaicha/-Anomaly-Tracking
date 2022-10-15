using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Products
{
    /// <summary>
    /// Interface that manages product operations.
    /// </summary>
    public interface IServiceProduct : IServiceBase<ProductDb, IAnomalyTrackingUnitOfWork>, IServiceContext<IAnomalyTrackingUnitOfWork>
    {
        /// <summary>
        /// Create the given product
        /// </summary>
        /// <param name="productDb">Product to create</param>
        /// <returns>Create a new product</returns>
        ProductDb Create(ProductDb productDb);
        /// <summary>
        /// Update the given product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDb"> Product to update</param>
        /// <returns>Modified product info</returns>
        ProductDb Update(ProductDb productDb, bool commit = true);

        /// <summary>
        /// Get specific product by id
        /// </summary>
        /// <param name="id"> product on retrieved data </param>
        /// <returns> get the existant product</returns>
        ProductDb Get(int id, string includeProperties = "");

        /// <summary>
        ///  Delete specific product by id
        /// </summary>
        /// <param name="id">identifier of the product to delete</param>
        /// <returns> Deleted product identifier</returns>
        int Delete(int id);
       
        /// <summary>
        /// Get all product
        /// </summary>
        /// <returns> get all existant product </returns>
        IEnumerable<ProductDb> GetAll(Expression<Func<ProductDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true);
    }
}
