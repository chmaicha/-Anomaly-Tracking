using AnomalyTracking.Model.AnomalyDeclarations;
using AnomalyTracking.Model.Cavities;
using Shared.Core.Model.Common;
using Shared.Core.Model.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnomalyTracking.Model.Molds
{
    /// <summary>
    /// DTO that represents Mold object.
    /// </summary>
    [DataContract]
    public class Mold : EntityBase
    {
        /// <summary>
        /// Mold's label.
        /// </summary>
        [DataMember]
        public string Label { get; set; }
       


        /// <summary>
        /// Address
        /// </summary>
        [DataMember]
        public int ClientId { get; set; }


        /// <summary>
        /// Mold
        /// </summary>
        [DataMember]
        public IEnumerable<AnomalyDeclaration> AnomalyDeclarations { get; set; }

        /// <summary>
        /// Mold
        /// </summary>
        [DataMember]
        public IEnumerable<Cavity> Cavities { get; set; }
    }
}
