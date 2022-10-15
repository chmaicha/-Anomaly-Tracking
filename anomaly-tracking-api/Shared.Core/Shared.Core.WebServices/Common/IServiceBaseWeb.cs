using System.ServiceModel;
using System.ServiceModel.Web;

namespace Shared.Core.WebServices.Common
{
    /// <summary>
    /// Interface that defines the autorized HTTP operations.
    /// </summary>
    [ServiceContract]
    public interface IServiceBaseWeb
    {
        /// <summary>
        /// Gets the ensures to HTTP Prelight request.
        /// </summary>
        [WebInvoke(Method = "OPTIONS", UriTemplate = "*")]
        void GetOptions();
    }
}