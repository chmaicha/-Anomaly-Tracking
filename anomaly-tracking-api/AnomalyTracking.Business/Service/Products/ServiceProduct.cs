using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Products
{
    /// <summary>
    /// Class that manages product.
    /// </summary>
    public class ServiceProduct : ServiceBase<ProductDb, IAnomalyTrackingUnitOfWork>, IServiceProduct
    {
        public ServiceProduct(IAnomalyTrackingUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ProductDb Create(ProductDb productDb)
        {
            productDb.LastModificationDate = DateTime.Now;
            productDb = this.unitOfWork.ProductRepo.Add(productDb, false);
            return this.Get(productDb.Id);
        }

        public ProductDb Update(ProductDb product, bool commit = true)
        {
            ProductDb productDb = this.Get(product.Id);

            productDb.Ref = product.Ref;
            productDb.LastModificationDate = DateTime.Now;

            this.unitOfWork.ProductRepo.Update(productDb, true);

            return this.Get(productDb.Id);
        }

        public int Delete(int id)
        {
            ProductDb productDb = this.unitOfWork.ProductRepo.GetById(id);
            this.unitOfWork.ProductRepo.Update(productDb, true);
            this.unitOfWork.ProductRepo.Delete(id, true);
            return id;
        }

        public ProductDb Get(int id, string includeProperties = "")
        {
            return this.GetAll(c => c.Id == id, includeProperties).Single();
        }

        public IEnumerable<ProductDb> GetAll(Expression<Func<ProductDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true)
        {
            this.filter.IncludeProperties = string.IsNullOrWhiteSpace(includeProperties) ? "AnomalyDB" : includeProperties;
            this.filter.EntityFilter = filter;
            this.filter.Page = page;
            this.filter.Paginate = paginate;
            this.filter.OrderBy = p => p.OrderByDescending(e => e.LastModificationDate);

            return this.unitOfWork.ProductRepo.GetAll(this.filter).ToList();
        }
    }
}
