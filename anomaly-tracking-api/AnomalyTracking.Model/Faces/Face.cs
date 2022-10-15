
using Shared.Core.Model.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnomalyTracking.Model.Faces
{
    /// <summary>
    /// DTO that represents Client object.
    /// </summary>
    [DataContract]
    public class Face : EntityBase
    {
        /// <summary>
        /// Face's reference.
        /// </summary> 
        [DataMember]
        public string Label { get; set; }

        [DataMember]

        public IEnumerable<string> Position { get; set; }
    }
}
