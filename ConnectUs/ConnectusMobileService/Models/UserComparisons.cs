//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConnectusMobileService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserComparisons
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserComparisons()
        {
            this.Networks = new HashSet<Networks>();
        }
    
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CompUserId { get; set; }
        public System.DateTimeOffset CreatedAt { get; set; }
        public string EqualJson { get; set; }
        public string OnlyUserJson { get; set; }
        public string OnlyCompUserJson { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTimeOffset> UpdatedAt { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Accounts Accounts { get; set; }
        public virtual Accounts Accounts1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Networks> Networks { get; set; }
    }
}