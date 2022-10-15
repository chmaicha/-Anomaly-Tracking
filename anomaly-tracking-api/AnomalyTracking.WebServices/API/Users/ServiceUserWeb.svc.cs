using AnomalyTracking.Business.ServiceApp.Users;
using AnomalyTracking.Model.Users;
using AnomalyTracking.WebServices.Containers;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Unity;

namespace AnomalyTracking.WebServices.API.Users
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceUserWeb : ServiceBaseWeb, IServiceUserWeb
    {
        private readonly IServiceUserApp serviceUserApp;

        public ServiceUserWeb()
        {
            this.serviceUserApp = IocContainer.Instance.UnityContainer.Resolve<IServiceUserApp>();
        }

        public Response<User> Create(User user)
        {
            return this.serviceUserApp.Create(user);
        }

        public Response<User> Update(string id, User user)
        {
            int.TryParse(id, out int userId);

            return this.serviceUserApp.Update(userId, user);
        }

        public Response<User> Get(string serviceUserId)
        {
            int.TryParse(serviceUserId, out int id);

            return this.serviceUserApp.Get(id);
        }

        public Response<int> Delete(string userId)
        {
            int.TryParse(userId, out int id);

            return this.serviceUserApp.Delete(id);
        }

        public Response<IEnumerable<int>> DeleteALL(string[] usersIds)
        {
            return this.serviceUserApp.DeleteAll(usersIds);
        }

        public Response<IEnumerable<User>> GetAll(SearchFilterBase filter)
        {
            return this.serviceUserApp.GetAll(filter);
        }
    }
}
