using AnomalyTracking.Business.ServiceApp.Clients;
using AnomalyTracking.Model.Clients;
using AnomalyTracking.WebServices.Containers;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Unity;

namespace AnomalyTracking.WebServices.API.Clients
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceClientWeb : ServiceBaseWeb, IServiceClientWeb
    {
        private readonly IServiceClientApp serviceClientApp;

        public ServiceClientWeb()
        {
            this.serviceClientApp = IocContainer.Instance.UnityContainer.Resolve<IServiceClientApp>();
        }

        public Response<Client> Create(Client client)
        {
            return this.serviceClientApp.Create(client);
        }

        public Response<Client> Update(string id, Client client)
        {
            int.TryParse(id, out int clientId);

            return this.serviceClientApp.Update(clientId, client);
        }

        public Response<Client> Get(string serviceClientId)
        {
            int.TryParse(serviceClientId, out int id);

            return this.serviceClientApp.Get(id);
        }


        public Response<int> Delete(string clientId)
        {
            int.TryParse(clientId, out int id);

            return this.serviceClientApp.Delete(id);
        }

        public Response<IEnumerable<int>> DeleteALL(string[] clientsIds)
        {
            return this.serviceClientApp.DeleteAll(clientsIds);
        }

        public Response<IEnumerable<Client>> GetAll(SearchFilterBase filter)
        {
            return this.serviceClientApp.GetAll(filter);
        }
    }
}
