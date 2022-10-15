using AnomalyTracking.Business.ServiceApp.AnomalyDeclarations;
using AnomalyTracking.Model.AnomalyDeclarations;
using AnomalyTracking.WebServices.Containers;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Unity;

namespace AnomalyTracking.WebServices.API.AnomalyDeclarations
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceAnomalyDeclarationWeb : ServiceBaseWeb, IServiceAnomalyDeclarationWeb
    {
        private readonly IServiceAnomalyDeclarationApp serviceAnomalyDeclarationApp;

        public ServiceAnomalyDeclarationWeb()
        {
            this.serviceAnomalyDeclarationApp = IocContainer.Instance.UnityContainer.Resolve<IServiceAnomalyDeclarationApp>();
        }

        public Response<AnomalyDeclaration> Create(AnomalyDeclaration anomalyDeclaration)
        {
            return this.serviceAnomalyDeclarationApp.Create(anomalyDeclaration);
        }

        public Response<AnomalyDeclaration> Update(string id, AnomalyDeclaration anomalyDeclaration)
        {
            int.TryParse(id, out int anomalyDeclarationId);

            return this.serviceAnomalyDeclarationApp.Update(anomalyDeclarationId, anomalyDeclaration);
        }

        public Response<AnomalyDeclaration> Get(string serviceAnomalyDeclarationId)
        {
            int.TryParse(serviceAnomalyDeclarationId, out int id);

            return this.serviceAnomalyDeclarationApp.Get(id);
        }

        public Response<int> Delete(string anomalyDeclarationId)
        {
            int.TryParse(anomalyDeclarationId, out int id);

            return this.serviceAnomalyDeclarationApp.Delete(id);
        }

        public Response<IEnumerable<int>> DeleteALL(string[] anomalyDeclarationsIds)
        {
            return this.serviceAnomalyDeclarationApp.DeleteAll(anomalyDeclarationsIds);
        }

        public Response<IEnumerable<AnomalyDeclaration>> GetAll(SearchFilterBase filter)
        {
            return this.serviceAnomalyDeclarationApp.GetAll(filter);
        }
    }
}
