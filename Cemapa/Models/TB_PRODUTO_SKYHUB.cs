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
    
    public partial class TB_PRODUTO_SKYHUB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_PRODUTO_SKYHUB()
        {
            this.TB_PRODUTO_CATEGORIA_SKYHUB = new HashSet<TB_PRODUTO_CATEGORIA_SKYHUB>();
            this.TB_PRODUTO_ESP_SKYHUB = new HashSet<TB_PRODUTO_ESP_SKYHUB>();
            this.TB_PRODUTO_IMAGEM_SKYHUB = new HashSet<TB_PRODUTO_IMAGEM_SKYHUB>();
        }
    
        public int COD_PRODUTO_SKYHUB { get; set; }
        public int COD_PRODUTO { get; set; }
        public string IND_SINCRONIZA { get; set; }
        public string DESC_PRODUTO { get; set; }
        public string DESC_DESCRICAO { get; set; }
        public string DESC_STATUS { get; set; }
        public string DESC_MARCA { get; set; }
        public Nullable<decimal> VAL_ALTURA { get; set; }
        public Nullable<decimal> VAL_LARGURA { get; set; }
        public Nullable<decimal> VAL_COMPRIMENTO { get; set; }
        public Nullable<int> COD_PRODUTO_SKYHUB_PAI { get; set; }
    
        public virtual TB_PRODUTO TB_PRODUTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_PRODUTO_CATEGORIA_SKYHUB> TB_PRODUTO_CATEGORIA_SKYHUB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_PRODUTO_ESP_SKYHUB> TB_PRODUTO_ESP_SKYHUB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_PRODUTO_IMAGEM_SKYHUB> TB_PRODUTO_IMAGEM_SKYHUB { get; set; }
    }
}
