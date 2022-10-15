using AnomalyTracking.Model.Authentifications;
using AnomalyTracking.Model.Users;
using Shared.Core.Client.Common;

namespace AnomalyTracking.Business.ServiceApp.Authentifications
{
    /// <summary>
    /// Interface that manages user's account.
    /// </summary>
    public interface IServiceAuthenticationApp
    {
        /// <summary>
        /// User's Login interface.
        /// </summary>
        /// <param name="user">User information. All fields can be empty excepts email and password</param>
        /// <returns>User connection</returns>
        Response<AuthenticationData> Login(User user);
    }
}
