//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shopping_Cart
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblorder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblorder()
        {
            this.tblorderdetails = new HashSet<tblorderdetail>();
        }
    
        public int oid { get; set; }
        public Nullable<System.DateTime> odate { get; set; }
        public string opname { get; set; }
        public string opphone { get; set; }
        public string opaddress { get; set; }
        public string opsaddress { get; set; }
        public Nullable<decimal> oamount { get; set; }
        public Nullable<int> ostatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblorderdetail> tblorderdetails { get; set; }
    }
}