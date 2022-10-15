using AnomalyTracking.Model.AnomalyDeclarations;
using Shared.Core.Model.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnomalyTracking.Model.Users
{
    /// <summary>
    /// DTO that represents Client object.
    /// </summary>
    [DataContract]
    public class User : EntityBase
    {
        /// <summary>
        /// User's reference.
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// User's reference.
        /// </summary>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// User's reference.
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// User's Password.
        /// </summary>
        [DataMember]
        public string Password { get; set; }

       

        /// <summary>
        /// User's reference.
        /// </summary>
        [DataMember]
        public int LvfUserRole { get; set; }

        /// <summary>
        /// User's reference.
        /// </summary>
        [DataMember]
        public IEnumerable<AnomalyDeclaration> AnomalyDeclarations { get; set; }
       



    }
}
