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
    
    public partial class TB_VENDEDOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_VENDEDOR()
        {
            this.TB_CADASTRO = new HashSet<TB_CADASTRO>();
            this.TB_PEDIDO_CAB = new HashSet<TB_PEDIDO_CAB>();
            this.TB_CONFIGURACAO_SKYHUB = new HashSet<TB_CONFIGURACAO_SKYHUB>();
        }
    
        public int COD_VENDEDOR { get; set; }
        public string COD_ESTADO { get; set; }
        public string NOME { get; set; }
        public string DESC_ENDERECO { get; set; }
        public string DESC_CIDADE { get; set; }
        public string DESC_CEP { get; set; }
        public string DESC_TELEFONE { get; set; }
        public string DESC_CELULAR { get; set; }
        public string DESC_E_MAIL { get; set; }
        public Nullable<System.DateTime> DT_NASCIMENTO { get; set; }
        public string DESC_CPF { get; set; }
        public string DESC_IDENTIDADE { get; set; }
        public Nullable<decimal> PERC_COMISSAO1 { get; set; }
        public Nullable<decimal> PERC_COMISSAO2 { get; set; }
        public Nullable<decimal> PERC_IRRF { get; set; }
        public Nullable<int> COD_SUPERVISOR { get; set; }
        public string DESC_FAX { get; set; }
        public string IND_TIPO_FRETE { get; set; }
        public string NOME_CONTATO1 { get; set; }
        public string NOME_CONTATO2 { get; set; }
        public string NOME_CONTATO3 { get; set; }
        public string IND_SITUACAO { get; set; }
        public Nullable<System.DateTime> DT_CADASTRO { get; set; }
        public Nullable<decimal> PERC_FRETE { get; set; }
        public Nullable<int> COD_DEPARTAMENTO { get; set; }
        public Nullable<decimal> VAL_FRETE_POCKET { get; set; }
        public Nullable<decimal> PERC_ICMS_POCKET { get; set; }
        public Nullable<int> COD_BANCO_AGENCIA { get; set; }
        public string DESC_AGENCIA { get; set; }
        public string DESC_CONTA_CORRENTE { get; set; }
        public Nullable<int> COD_FILIAL { get; set; }
        public Nullable<byte> PERC_DESCONTO_LIBERADO { get; set; }
        public Nullable<byte> PERC_DESCONTO_RESTRICAO { get; set; }
        public Nullable<byte> PERC_ACRESCIMO_MAXIMO { get; set; }
        public Nullable<byte> PERC_NOTA_FISCAL { get; set; }
        public string IND_TROCA { get; set; }
        public string IND_BONIFICACAO { get; set; }
        public string IND_BRINDE { get; set; }
        public string IND_AMOSTRA { get; set; }
        public string IND_SEM_NOTA { get; set; }
        public string DESC_BAIRRO { get; set; }
        public string DESC_E_MAIL1 { get; set; }
        public Nullable<decimal> PERC_COMISSAO3 { get; set; }
        public Nullable<decimal> PERC_COMISSAO4 { get; set; }
        public string IND_SINC_MAX_ROTEIRIZADOR { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_CADASTRO> TB_CADASTRO { get; set; }
        public virtual TB_ESTADO TB_ESTADO { get; set; }
        public virtual TB_FILIAL TB_FILIAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_PEDIDO_CAB> TB_PEDIDO_CAB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_CONFIGURACAO_SKYHUB> TB_CONFIGURACAO_SKYHUB { get; set; }
    }
}
