using AnomalyTracking.Business.ServiceApp.AnomalyTypes;
using AnomalyTracking.Model.AnomalyTypes;
using AnomalyTracking.WebServices.Containers;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Unity;

namespace AnomalyTracking.WebServices.API.AnomalyTypes
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceAnomalyTypeWeb : ServiceBaseWeb, IServiceAnomalyTypeWeb
    {
        private readonly IServiceAnomalyTypeApp serviceAnomalyTypeApp;

        public ServiceAnomalyTypeWeb()
        {
            this.serviceAnomalyTypeApp = IocContainer.Instance.UnityContainer.Resolve<IServiceAnomalyTypeApp>();
        }

        public Response<AnomalyType> Create(AnomalyType anomalyType)
        {
            return this.serviceAnomalyTypeApp.Create(anomalyType);
        }

        public Response<AnomalyType> Update(string id, AnomalyType anomalyType)
        {
            int.TryParse(id, out int anomalyTypeId);

            return this.serviceAnomalyTypeApp.Update(anomalyTypeId, anomalyType);
        }

        public Response<AnomalyType> Get(string serviceAnomalyTypeId)
        {
            int.TryParse(serviceAnomalyTypeId, out int id);

            return this.serviceAnomalyTypeApp.Get(id);
        }

        public Response<int> Delete(string anomalyTypeId)
        {
            int.TryParse(anomalyTypeId, out int id);

            return this.serviceAnomalyTypeApp.Delete(id);
        }

        public Response<IEnumerable<int>> DeleteALL(string[] anomalyTypesIds)
        {
            return this.serviceAnomalyTypeApp.DeleteAll(anomalyTypesIds);
        }

        public Response<IEnumerable<AnomalyType>> GetAll(SearchFilterBase filter)
        {
            return this.serviceAnomalyTypeApp.GetAll(filter);
        }
    }
}
