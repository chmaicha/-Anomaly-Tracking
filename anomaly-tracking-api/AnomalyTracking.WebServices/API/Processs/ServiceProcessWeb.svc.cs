using AnomalyTracking.Business.ServiceApp.Processs;
using AnomalyTracking.Model.Processs;
using AnomalyTracking.WebServices.Containers;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Unity;

namespace AnomalyTracking.WebServices.API.Processs
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceProcessWeb : ServiceBaseWeb, IServiceProcessWeb
    {
        private readonly IServiceProcessApp serviceProcessApp;

        public ServiceProcessWeb()
        {
            this.serviceProcessApp = IocContainer.Instance.UnityContainer.Resolve<IServiceProcessApp>();
        }

        public Response<Process> Create(Process process)
        {
            return this.serviceProcessApp.Create(process);
        }

        public Response<Process> Update(string id, Process process)
        {
            int.TryParse(id, out int processId);

            return this.serviceProcessApp.Update(processId, process);
        }

        public Response<Process> Get(string serviceProcessId)
        {
            int.TryParse(serviceProcessId, out int id);

            return this.serviceProcessApp.Get(id);
        }

        public Response<int> Delete(string processId)
        {
            int.TryParse(processId, out int id);

            return this.serviceProcessApp.Delete(id);
        }

        public Response<IEnumerable<int>> DeleteALL(string[] processsIds)
        {
            return this.serviceProcessApp.DeleteAll(processsIds);
        }

        public Response<IEnumerable<Process>> GetAll(SearchFilterBase filter)
        {
            return this.serviceProcessApp.GetAll(filter);
        }
    }
}
