//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cemapa.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_CST_PIS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_CST_PIS()
        {
            this.TB_PRODUTO = new HashSet<TB_PRODUTO>();
        }
    
        public string COD_CST_PIS { get; set; }
        public string DESC_CST_PIS { get; set; }
        public string IND_TIPO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_PRODUTO> TB_PRODUTO { get; set; }
    }
}
