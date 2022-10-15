using AnomalyTracking.Business.ServiceApp.Authentifications;
using AnomalyTracking.Model.Authentifications;
using AnomalyTracking.Model.Users;
using AnomalyTracking.WebServices.Containers;
using Shared.Core.Client.Common;
using Shared.Core.WebServices.Common;
using System.ServiceModel.Activation;
using Unity;

namespace AnomalyTracking.WebServices.API.Authentifications
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceAuthentificationWeb : ServiceBaseWeb, IServiceAuthentificationWeb
    {
        private readonly IServiceAuthenticationApp serviceAuthentificationApp;

        public ServiceAuthentificationWeb()
        {
            this.serviceAuthentificationApp = IocContainer.Instance.UnityContainer.Resolve<IServiceAuthenticationApp>();
        }

        public Response<AuthenticationData> Login(User user)
        {
            return this.serviceAuthentificationApp.Login(user);
        }
    }
}
