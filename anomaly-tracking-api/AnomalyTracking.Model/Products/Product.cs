using Shared.Core.Model.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnomalyTracking.Model.Products
{
    /// <summary>
    /// DTO that represents Client object.
    /// </summary>
    [DataContract]
    public class Product : EntityBase
    {
        /// <summary>
        /// Product's reference.
        /// </summary>
        [DataMember]
        public string Ref { get; set; }



    }
}
