using AnomalyTracking.Model.Users;
using System.Runtime.Serialization;

namespace AnomalyTracking.Model.Authentifications
{
    /// <summary>
    /// Contains the authentification data.
    /// TODO : Moved this DTO into pedetti shared to ensure that all the apps have the same authentication data.
    /// </summary>
    [DataContract]
    public class AuthenticationData
    {
        /// <summary>
        /// Connected <code>User</code> instance.
        /// </summary>
        [DataMember]
        public User User { get; set; }
    }
}
