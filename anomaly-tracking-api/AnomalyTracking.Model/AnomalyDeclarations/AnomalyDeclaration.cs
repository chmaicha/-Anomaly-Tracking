using Shared.Core.Model.Common;
using Shared.Core.Model.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnomalyTracking.Model.AnomalyDeclarations
{
    /// <summary>
    /// DTO that represents AnomalyDeclaration object.
    /// </summary>
    [DataContract]
    public class AnomalyDeclaration : EntityBase
    {
        /// <summary>
        /// AnomalyDeclaration's MoldId.
        /// </summary>
        [DataMember]
        public int UserId { get; set; }


        /// <summary>
        /// from tthe Cavity id we get the project and the mold
        /// </summary>
        [DataMember]
        public int CavityId { get; set; }


        /// <summary>
        /// here we stock the processId
        /// </summary>
        [DataMember]
        public int ProcessId { get; set; }


        /// <summary>
        /// from the anomalyId we get the face/position and anomalyType
        /// </summary>
        [DataMember]
        public int AnomalyId { get; set; }


      
        


       
      
    }
}
