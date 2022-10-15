using Shared.Core.Model.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnomalyTracking.Model.Cavities
{
    /// <summary>
    /// DTO that represents Client object.
    /// </summary>
    [DataContract]
    public class Cavity : EntityBase
    {
        /// <summary>
        /// Cavity's reference.
        /// </summary>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// Cavity's reference.
        /// </summary>
        [DataMember]
        public int MoldId { get; set; }
    }
}
