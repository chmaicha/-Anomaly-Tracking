using AnomalyTracking.Business.ServiceApp.Cavities;
using AnomalyTracking.Model.Cavities;
using AnomalyTracking.WebServices.Containers;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Unity;

namespace AnomalyTracking.WebServices.API.Cavities
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceCavityWeb : ServiceBaseWeb, IServiceCavityWeb
    {
        private readonly IServiceCavityApp serviceCavityApp;

        public ServiceCavityWeb()
        {
            this.serviceCavityApp = IocContainer.Instance.UnityContainer.Resolve<IServiceCavityApp>();
        }

        public Response<Cavity> Create(Cavity cavity)
        {
            return this.serviceCavityApp.Create(cavity);
        }

        public Response<Cavity> Update(string id, Cavity cavity)
        {
            int.TryParse(id, out int cavityId);

            return this.serviceCavityApp.Update(cavityId, cavity);
        }

        public Response<Cavity> Get(string serviceCavityId)
        {
            int.TryParse(serviceCavityId, out int id);

            return this.serviceCavityApp.Get(id);
        }

        public Response<int> Delete(string cavityId)
        {
            int.TryParse(cavityId, out int id);

            return this.serviceCavityApp.Delete(id);
        }

        public Response<IEnumerable<int>> DeleteALL(string[] cavitysIds)
        {
            return this.serviceCavityApp.DeleteAll(cavitysIds);
        }

        public Response<IEnumerable<Cavity>> GetAll(SearchFilterBase filter)
        {
            return this.serviceCavityApp.GetAll(filter);
        }
    }
}
