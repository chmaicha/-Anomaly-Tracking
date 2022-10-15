using AnomalyTracking.Business.ServiceApp.Products;
using AnomalyTracking.Model.Products;
using AnomalyTracking.WebServices.Containers;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Unity;

namespace AnomalyTracking.WebServices.API.Products
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceProductWeb : ServiceBaseWeb, IServiceProductWeb
    {
        private readonly IServiceProductApp serviceProductApp;

        public ServiceProductWeb()
        {
            this.serviceProductApp = IocContainer.Instance.UnityContainer.Resolve<IServiceProductApp>();
        }

        public Response<Product> Create(Product product)
        {
            return this.serviceProductApp.Create(product);
        }

        public Response<Product> Update(string id, Product product)
        {
            int.TryParse(id, out int productId);

            return this.serviceProductApp.Update(productId, product);
        }

        public Response<Product> Get(string serviceProductId)
        {
            int.TryParse(serviceProductId, out int id);

            return this.serviceProductApp.Get(id);
        }

        public Response<int> Delete(string productId)
        {
            int.TryParse(productId, out int id);

            return this.serviceProductApp.Delete(id);
        }

        public Response<IEnumerable<int>> DeleteALL(string[] productsIds)
        {
            return this.serviceProductApp.DeleteAll(productsIds);
        }

        public Response<IEnumerable<Product>> GetAll(SearchFilterBase filter)
        {
            return this.serviceProductApp.GetAll(filter);
        }
    }
}
