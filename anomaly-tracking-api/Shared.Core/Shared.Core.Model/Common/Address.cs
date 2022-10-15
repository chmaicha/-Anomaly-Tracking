using Shared.Core.Model.Core;
using System.Runtime.Serialization;

namespace Shared.Core.Model.Common
{
    /// <summary>
    /// Represents <code>Address</code> object
    /// </summary>
    [DataContract]
    public class Address : EntityBase
    {
        /// <summary>
        /// Represents <code>Address</code> Street Number
        /// </summary>
        [DataMember]
        public string StreetNumber { get; set; }

        /// <summary>
        /// Represents <code>Address</code> Street Name
        /// </summary>
        [DataMember]
        public string StreetName { get; set; }

        /// <summary>
        /// Represents <code>Address</code> ZipCode
        /// </summary>
        [DataMember]
        public string ZipCode { get; set; }

        /// <summary>
        /// Represents <code>Address</code> department.
        /// </summary>
        [DataMember]
        public string Department { get; set; }

        /// <summary>
        /// Represents <code>Address</code> City
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// Represents <code>Address</code> Country
        /// </summary>
        [DataMember]
        public string CountryCode { get; set; }

        /// <summary>
        /// Represents <code>Address</code> More information
        /// </summary>
        [DataMember]
        public string FurtherInformation { get; set; }

        /// <summary>
        /// Checks whether the given address is valid.
        /// </summary>
        /// <param name="address">Address to check</param>
        /// <returns>True if the address instance is valid, otherwise false.</returns>
        public static bool IsValid(Address address)
        {
            return
                address != null &&
                !string.IsNullOrWhiteSpace(address.StreetName) &&
                !string.IsNullOrWhiteSpace(address.ZipCode) &&
                !string.IsNullOrWhiteSpace(address.City) &&
                !string.IsNullOrWhiteSpace(address.CountryCode);
        }
    }
}