using AnomalyTracking.Model.Products;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using System.Collections.Generic;

namespace AnomalyTracking.Business.ServiceApp.Products
{ /// <summary>
  /// Interface that manages product operations.
  /// </summary>
    public interface IServiceProductApp
    {
        /// <summary>
        /// Create the given product
        /// </summary>
        /// <param name="product">Product to create</param>
        /// <returns>Create a new product</returns>
        Response<Product> Create(Product product);

        /// <summary>
        /// Update the given product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"> Product to update</param>
        /// <returns>Modified product info</returns>
        Response<Product> Update(int id, Product product);


        /// <summary>
        /// Get specific product by id
        /// </summary>
        /// <param name="id"> product on retrieved data </param>
        /// <returns> get the existant product</returns>
        Response<Product> Get(int id);


        /// <summary>
        ///  Delete specific product by id
        /// </summary>
        /// <param name="id">identifier of the product to delete</param>
        /// <returns> Deleted product identifier</returns>
        Response<int> Delete(int id);

        /// <summary>
        /// Deletes a list of products based on identifiers.
        /// </summary>
        /// <param name="entitiesIds"> The list of identifiers of products to be deleted</param>
        /// <returns> The list of products identifiers deleted</returns>
        Response<IEnumerable<int>> DeleteAll(string[] entitiesIds);

        /// <summary>
        /// Get all product
        /// </summary>
        /// <returns> get all existant product </returns>
        Response<IEnumerable<Product>> GetAll(SearchFilterBase filter);
       
    }
}