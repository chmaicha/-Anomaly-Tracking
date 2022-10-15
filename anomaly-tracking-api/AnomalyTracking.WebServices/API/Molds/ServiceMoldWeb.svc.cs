using AnomalyTracking.Business.ServiceApp.Molds;
using AnomalyTracking.Model.Molds;
using AnomalyTracking.WebServices.Containers;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Unity;

namespace AnomalyTracking.WebServices.API.Molds
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceMoldWeb : ServiceBaseWeb, IServiceMoldWeb
    {
        private readonly IServiceMoldApp serviceMoldApp;

        public ServiceMoldWeb()
        {
            this.serviceMoldApp = IocContainer.Instance.UnityContainer.Resolve<IServiceMoldApp>();
        }

        public Response<Mold> Create(Mold mold)
        {
            return this.serviceMoldApp.Create(mold);
        }

        public Response<Mold> Update(string id, Mold mold)
        {
            int.TryParse(id, out int moldId);

            return this.serviceMoldApp.Update(moldId, mold);
        }

        public Response<Mold> Get(string serviceMoldId)
        {
            int.TryParse(serviceMoldId, out int id);

            return this.serviceMoldApp.Get(id);
        }

        public Response<int> Delete(string moldId)
        {
            int.TryParse(moldId, out int id);

            return this.serviceMoldApp.Delete(id);
        }

        public Response<IEnumerable<int>> DeleteALL(string[] moldsIds)
        {
            return this.serviceMoldApp.DeleteAll(moldsIds);
        }

        public Response<IEnumerable<Mold>> GetAll(SearchFilterBase filter)
        {
            return this.serviceMoldApp.GetAll(filter);
        }
    }
}
