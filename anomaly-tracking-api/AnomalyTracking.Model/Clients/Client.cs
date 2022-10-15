using AnomalyTracking.Model.Molds;
using Shared.Core.Model.Common;
using Shared.Core.Model.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnomalyTracking.Model.Clients
{
    /// <summary>
    /// DTO that represents Client object.
    /// </summary>
    [DataContract]
    public class Client : EntityBase
    {
        /// <summary>
        /// Client's label.
        /// </summary>
        [DataMember]
        public string Label { get; set; }
        /// <summary>
        /// client email
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Client phone number
        /// </summary>
        [DataMember]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [DataMember]
        public int? AddressId { get; set; }

        /// <summary>
        /// Client adress
        /// </summary>
        [DataMember]
        public Address Address { get; set; }

        /// <summary>
        /// Client Mold
        /// </summary>
        [DataMember]
        public IEnumerable<Mold> Molds { get; set; }

        /// <summary>
        /// Client phone number
        /// </summary>
        [DataMember]
        public string ClientMold { get; set; }


    }
}
