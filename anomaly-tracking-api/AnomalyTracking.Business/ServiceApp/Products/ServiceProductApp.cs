using AnomalyTracking.Business.Mappers.Products;
using AnomalyTracking.Business.Service.Products;
using AnomalyTracking.Model.Products;
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

namespace AnomalyTracking.Business.ServiceApp.Products
{
    public class ServiceProductApp : IServiceProductApp
    {
        private readonly ProductMapper productMapper;
        private readonly IServiceProduct serviceProduct;
        private readonly IAnomalyTrackingUnitOfWork unitOfWork;

        public ServiceProductApp(IServiceProduct serviceProduct, IAnomalyTrackingUnitOfWork unitOfWork)
        {
            this.productMapper = new ProductMapper();
            this.unitOfWork = unitOfWork;
            this.serviceProduct = serviceProduct;
            this.serviceProduct.SetContext(this.unitOfWork);
        }

        public Response<Product> Create(Product product)
        {
            try
            {
                CheckProductInfo(product);

                ProductDb productDb = this.productMapper.Map(product);

                productDb = this.serviceProduct.Create(productDb);

                product = this.productMapper.Map(productDb);

                return new Response<Product>(product, "app.shared.succeededcreate");
            }
            catch (ArgumentException aex)
            {
                return new Response<Product>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Product>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Product> Update(int id, Product product)
        {
            try
            {
                CheckProductInfo(product, id);

                ProductDb productDb = this.productMapper.Map(product);
               
                productDb = this.serviceProduct.Update(productDb);

                product = this.productMapper.Map(productDb);

                return new Response<Product>(product, "app.shared.succeededupdate");
            }
            catch (ArgumentException aex)
            {

                return new Response<Product>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Product>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Product> Get(int id)
        {
            try
            {
                ProductDb productDb = this.serviceProduct.Get(id);

                Product product = this.productMapper.Map(productDb);

                return new Response<Product>(product, "app.shared.succeededget");
            }
            catch (ArgumentException aex)
            {
                return new Response<Product>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);
                return new Response<Product>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<IEnumerable<Product>> GetAll(SearchFilterBase filter)
        {
            try
            {
                IEnumerable<ProductDb> productDbs = this.serviceProduct.GetAll(this.ApplyFilter(filter), "", filter.Page, filter.Paginate).ToList();

                IEnumerable<Product> products = this.productMapper.Map(productDbs);

                return new Response<IEnumerable<Product>>(products, "app.shared.succeededget", this.serviceProduct.GetTotalCount());
            }
            catch (ArgumentException aex)
            {
                return new Response<IEnumerable<Product>>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<IEnumerable<Product>>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<int> Delete(int productId)
        {
            try
            {
                this.unitOfWork.BeginTransaction();
                List<string> paths = new List<string>();

                this.unitOfWork.CommitTransaction();

                return new Response<int>(productId, "app.shared.succeededdelete");
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

                List<int> productsIds = entitiesIds.Select(id => int.Parse(id)).ToList();
                List<string> paths = new List<string>();

                foreach (int productId in productsIds)
                {
                    this.DeleteProductById(productId, paths);
                }

                this.unitOfWork.CommitTransaction();

                return new Response<IEnumerable<int>>(productsIds, "app.shared.succeededdelete");
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

        private Expression<Func<ProductDb, bool>> ApplyFilter(SearchFilterBase filter)
        {
            if (filter == null)
            {
                throw new ArgumentException("app.error.invalidfilter");
            }

            bool hasGlobalSearchInput = !string.IsNullOrEmpty(filter.GlobalSearchInput);

            return product =>

                 (!hasGlobalSearchInput ||
                    product.Ref.Contains(filter.GlobalSearchInput)
                );
        }

        private void DeleteProductById(int productId, List<string> paths)
        {
            Product product = this.productMapper.Map(this.serviceProduct.Get(productId));

            ProductDb productDb = this.serviceProduct.GetAll(c => c.Id == productId).Single();

            this.serviceProduct.Delete(productId);
        }

        private static void CheckProductInfo(Product product, int productId = 0)
        {
            ManagementRuleHelper.CheckRequestParameters(product, productId);
        }
    }
}
