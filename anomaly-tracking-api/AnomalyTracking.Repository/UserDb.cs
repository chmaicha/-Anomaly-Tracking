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
    
    public partial class UserDb
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserDb()
        {
            this.AnomalyDeclarationDbs = new HashSet<AnomalyDeclarationDb>();
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public int LvfUserRole { get; set; }
        public System.DateTime LastModificationDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnomalyDeclarationDb> AnomalyDeclarationDbs { get; set; }
        public virtual LvfUserRoleDb LvfUserRoleDb { get; set; }
    }
}
