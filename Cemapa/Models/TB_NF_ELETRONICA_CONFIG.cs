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
    
    public partial class TB_NF_ELETRONICA_CONFIG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_NF_ELETRONICA_CONFIG()
        {
            this.TB_FILIAL = new HashSet<TB_FILIAL>();
        }
    
        public string DESC_SERVIDOR { get; set; }
        public Nullable<int> NUM_MAX_BUFFER { get; set; }
        public string DESC_PASTA_CANCELAMENTO { get; set; }
        public string DESC_PASTA_INUTILIZACAO { get; set; }
        public string DESC_PASTA_STATUS { get; set; }
        public string DESC_PASTA_CONSULTA_PROT { get; set; }
        public string DESC_PASTA_COPIA_DANFE { get; set; }
        public string DESC_PASTA_EMAIL { get; set; }
        public string DESC_CERTIFICADO { get; set; }
        public string DESC_LOGO { get; set; }
        public string DESC_SERVIDOR_IMPRESSAO { get; set; }
        public Nullable<int> NUM_INTERVALO { get; set; }
        public Nullable<byte> NUM_MAX_CONSULTA_NFE { get; set; }
        public string DESC_EMAIL { get; set; }
        public string DESC_SENHA_EMAIL { get; set; }
        public Nullable<int> NUM_PORTA_SMTP { get; set; }
        public string DESC_ENDERECO_SMTP { get; set; }
        public string IND_EMAIL_AUTOMATICO { get; set; }
        public string IND_EMAIL_SSL { get; set; }
        public string IND_COD_CADASTRO_NFE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_FILIAL> TB_FILIAL { get; set; }
    }
}