using AnomalyTracking.Model.Anomalies;
using Shared.Core.Model.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnomalyTracking.Model.AnomalyTypes
{
    /// <summary>
    /// DTO that represents Client object.
    /// </summary>
    [DataContract]
    public class AnomalyType : EntityBase
    {
        /// <summary>
        ///AnomalyTypes's reference.
        /// </summary>
        [DataMember]
        public string Label { get; set; }



        /// <summary>
        /// Address
        /// </summary>
        [DataMember]
        public int ProcessId { get; set; }

        /// <summary>
        /// Mold
        /// </summary>
        [DataMember]
        public IEnumerable<Anomaly> Anomalies { get; set; }

    }
}
