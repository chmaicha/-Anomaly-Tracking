using AnomalyTracking.Model.Products;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AnomalyTracking.WebServices.API.Products
{
    [ServiceContract]
    public interface IServiceProductWeb : IServiceBaseWeb
    {
        /// <summary>
        /// Create the given product
        /// </summary>
        /// <param name="product">Product to create</param>
        /// <returns>Create a new product</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/products", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Product> Create(Product product);

        /// <summary>
        /// Update the given product
        /// <param name="id"></param>
        /// </summary>
        /// <param name="product"> Product to update</param>
        /// <returns>Modified product info</returns>
        [WebInvoke(Method = "PUT", UriTemplate = "/api/products/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<Product> Update(string id, Product product);

        /// <summary>
        ///  Delete specific product by id
        /// </summary>
        /// <param name="id">identifier of the product to delete</param>
        /// <returns> Deleted product identifier</returns>
        [WebInvoke(Method = "DELETE", UriTemplate = "/api/products/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<int> Delete(string id);

        /// <summary>
        /// Deletes a list of products based on identifiers.
        /// </summary>
        /// <param name="productsIds"> The list of identifiers of products to be deleted</param>
        /// <returns> The list of products identifiers deleted</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/products/_delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<int>> DeleteALL(string[] productsIds);

        /// <summary>
        /// Get specific product by id
        /// </summary>
        /// <param name="id"> product on retrieved data </param>
        /// <returns> get the existant product</returns>
        [WebInvoke(Method = "GET", UriTemplate = "/api/products/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Response<Product> Get(string id);

        /// <summary>
        /// Get all product
        /// </summary>
        /// <returns> get all existant product </returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/products/_filter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<IEnumerable<Product>> GetAll(SearchFilterBase filter);


    }
}
