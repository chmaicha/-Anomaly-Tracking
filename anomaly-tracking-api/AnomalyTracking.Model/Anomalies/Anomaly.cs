using System.Collections.Generic;
using System.Runtime.Serialization;
using AnomalyTracking.Model.Faces;
using Shared.Core.Model.Core;

namespace AnomalyTracking.Model.Anomalies
{

    /// <summary>
    /// DTO that represents Client object.
    /// </summary>
    [DataContract]
     public class Anomaly : EntityBase

    {
        /// <summary>
        /// Face
        /// </summary>
        [DataMember]
        public IEnumerable<Face> Faces { get; set; }


        /// <summary>
        /// Face's reference.
        /// </summary> 
        [DataMember]
        public int AnomalyTypeId { get; set; }



  



    }
}
