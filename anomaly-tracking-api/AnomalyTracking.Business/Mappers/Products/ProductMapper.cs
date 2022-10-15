using AnomalyTracking.Model.Products;
using AnomalyTracking.Repository;
using Shared.Core.Business.Common;
using System.Collections.Generic;
using System.Linq;

namespace AnomalyTracking.Business.Mappers.Products
{
    public class ProductMapper : IMapper<Product, ProductDb>, IMapper<ProductDb, Product>
    {
        
        public Product Map(ProductDb entityDb)
        {
            return new Product()
            {
                Id = entityDb.Id,
                Ref = entityDb.Ref,
                LastModificationDate = entityDb.LastModificationDate,
            };
        }

        public ProductDb Map(Product entity)
        {
            return new ProductDb()
            {
                Id = entity.Id,
                Ref = entity.Ref,
            };
        }

        public IEnumerable<ProductDb> Map(IEnumerable<Product> entities)
        {
            return entities.Select(e => this.Map(e)).ToList();
        }

        public IEnumerable<Product> Map(IEnumerable<ProductDb> entitiesDb)
        {
            return entitiesDb.Select(e => this.Map(e)).ToList();
        }
    }
}