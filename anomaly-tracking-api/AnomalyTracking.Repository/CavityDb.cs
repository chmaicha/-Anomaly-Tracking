//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnomalyTracking.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class CavityDb
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CavityDb()
        {
            this.AnomalyDeclarationDbs = new HashSet<AnomalyDeclarationDb>();
        }
    
        public int Id { get; set; }
        public string Label { get; set; }
        public System.DateTime LastModificationDate { get; set; }
        public int MoldId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnomalyDeclarationDb> AnomalyDeclarationDbs { get; set; }
        public virtual MoldDb MoldDb { get; set; }
    }
}
