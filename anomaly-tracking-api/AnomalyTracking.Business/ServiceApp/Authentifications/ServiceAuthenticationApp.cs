using AnomalyTracking.Business.Mappers.Users;
using AnomalyTracking.Business.Service.Users;
using AnomalyTracking.Model.Authentifications;
using AnomalyTracking.Model.Users;
using AnomalyTracking.Repository;
using Shared.Core.Client.Common;
using System;
using System.Linq;

namespace AnomalyTracking.Business.ServiceApp.Authentifications
{
    public class ServiceAuthenticationApp : IServiceAuthenticationApp
    {
        private readonly UserMapper userMapper;
        private readonly IServiceUser serviceUser;

        public ServiceAuthenticationApp(IServiceUser serviceUser)
        {
            this.userMapper = new UserMapper();
            this.serviceUser = serviceUser;
        }
        public Response<AuthenticationData> Login(User user)
        {

            UserDb userDb = this.serviceUser.GetAll(u => u.Code == user.Code).SingleOrDefault();

            AuthenticationData authenticationData = new AuthenticationData();

            if (userDb != null)
            {
                User connectedUser = this.userMapper.Map(userDb);
                authenticationData.User = connectedUser;
            }

            return new Response<AuthenticationData>(authenticationData, "app.shared.succeedpassword");
        }

    }
}
