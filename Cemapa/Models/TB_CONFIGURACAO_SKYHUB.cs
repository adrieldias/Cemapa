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
    
    public partial class TB_CONFIGURACAO_SKYHUB
    {
        public string IND_ATIVO { get; set; }
        public string DESC_USUARIO_EMAIL { get; set; }
        public string DESC_TOKEN_INTEGRACAO { get; set; }
        public string DESC_TOKEN_ACCOUNT { get; set; }
        public int COD_CONFIGURACAO_SKYHUB { get; set; }
        public int COD_FILIAL { get; set; }
        public int COD_OPERACAO { get; set; }
    
        public virtual TB_FILIAL TB_FILIAL { get; set; }
        public virtual TB_OPERACAO TB_OPERACAO { get; set; }
    }
}
