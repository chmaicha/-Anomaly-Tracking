using AnomalyTracking.Model.AnomalyDeclarations;
using AnomalyTracking.Model.AnomalyTypes;
using Shared.Core.Model.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnomalyTracking.Model.Processs
{
    /// <summary>
    /// DTO that represents Client object.
    /// </summary>
    [DataContract]
    public class Process : EntityBase
    {
        /// <summary>
        /// Process's reference.
        /// </summary>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// Process's reference.
        /// </summary>
        [DataMember]
        public IEnumerable<AnomalyDeclaration> AnomalyDeclarations { get; set; }

        /// <summary>
        /// Process's reference.
        /// </summary>
        [DataMember]
        public IEnumerable<AnomalyType> AnomalyTypes { get; set; }



    }
}
