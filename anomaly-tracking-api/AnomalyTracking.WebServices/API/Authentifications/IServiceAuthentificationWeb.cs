using AnomalyTracking.Model.Authentifications;
using AnomalyTracking.Model.Users;
using Shared.Core.Client.Common;
using Shared.Core.WebServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AnomalyTracking.WebServices.API.Authentifications

{
    /// <summary>
    /// Inteface that manages user authentification.
    /// </summary>
    [ServiceContract]
    public interface IServiceAuthentificationWeb : IServiceBaseWeb
    {
        /// <summary>
        /// User's Login interface.
        /// </summary>
        /// <param name="user">User information. All fields can be empty excepts email and password</param>
        /// <returns>User connection</returns>
        [WebInvoke(Method = "POST", UriTemplate = "/api/users", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response<AuthenticationData> Login(User user);
    }
}
