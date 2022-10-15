using AnomalyTracking.Business.ServiceApp.Anomalies;
using AnomalyTracking.Model.Anomalies;
using AnomalyTracking.WebServices.Containers;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Unity;

namespace AnomalyTracking.WebServices.API.Anomalies
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceAnomalyWeb : ServiceBaseWeb, IServiceAnomalyWeb
    {
        private readonly IServiceAnomalyApp serviceAnomalyApp;

        public ServiceAnomalyWeb()
        {
            this.serviceAnomalyApp = IocContainer.Instance.UnityContainer.Resolve<IServiceAnomalyApp>();
        }

        public Response<Anomaly> Create(Anomaly anomaly)
        {
            return this.serviceAnomalyApp.Create(anomaly);
        }

        public Response<Anomaly> Update(string id, Anomaly anomaly)
        {
            int.TryParse(id, out int anomalyId);

            return this.serviceAnomalyApp.Update(anomalyId, anomaly);
        }

        public Response<Anomaly> Get(string serviceAnomalyId)
        {
            int.TryParse(serviceAnomalyId, out int id);

            return this.serviceAnomalyApp.Get(id);
        }

        public Response<int> Delete(string anomalyId)
        {
            int.TryParse(anomalyId, out int id);

            return this.serviceAnomalyApp.Delete(id);
        }

        public Response<IEnumerable<int>> DeleteALL(string[] anomaliesIds)
        {
            return this.serviceAnomalyApp.DeleteAll(anomaliesIds);
        }

        public Response<IEnumerable<Anomaly>> GetAll(SearchFilterBase filter)
        {
            return this.serviceAnomalyApp.GetAll(filter);
        }
    }
}
