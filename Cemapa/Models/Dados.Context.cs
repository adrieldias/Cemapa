﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TB_TIPO_CADASTRO> TB_TIPO_CADASTRO { get; set; }
        public virtual DbSet<TB_CLASS_CADASTRO> TB_CLASS_CADASTRO { get; set; }
        public virtual DbSet<TB_ESTADO> TB_ESTADO { get; set; }
        public virtual DbSet<TB_ESTADO_CIVIL> TB_ESTADO_CIVIL { get; set; }
        public virtual DbSet<TB_MOTIVO> TB_MOTIVO { get; set; }
        public virtual DbSet<TB_PAIS> TB_PAIS { get; set; }
        public virtual DbSet<TB_USUARIO> TB_USUARIO { get; set; }
        public virtual DbSet<TB_TIPO_PROPRIEDADE> TB_TIPO_PROPRIEDADE { get; set; }
        public virtual DbSet<TB_PROPRIEDADE> TB_PROPRIEDADE { get; set; }
        public virtual DbSet<TB_QUALIFICACAO_SOCIO> TB_QUALIFICACAO_SOCIO { get; set; }
        public virtual DbSet<TB_CADASTRO_ENDERECOS> TB_CADASTRO_ENDERECOS { get; set; }
        public virtual DbSet<TB_CLASSE> TB_CLASSE { get; set; }
        public virtual DbSet<TB_TRIBUTACAO> TB_TRIBUTACAO { get; set; }
        public virtual DbSet<TB_ESTOQUE> TB_ESTOQUE { get; set; }
        public virtual DbSet<TB_CADASTRO> TB_CADASTRO { get; set; }
        public virtual DbSet<TB_PEDIDO_CAB> TB_PEDIDO_CAB { get; set; }
        public virtual DbSet<TB_PRODUTO> TB_PRODUTO { get; set; }
        public virtual DbSet<TB_CIDADE> TB_CIDADE { get; set; }
        public virtual DbSet<TB_REGIAO> TB_REGIAO { get; set; }
        public virtual DbSet<TB_VENDEDOR> TB_VENDEDOR { get; set; }
        public virtual DbSet<TB_CONFIGURACAO_SKYHUB> TB_CONFIGURACAO_SKYHUB { get; set; }
        public virtual DbSet<TB_PRODUTO_CATEGORIA_SKYHUB> TB_PRODUTO_CATEGORIA_SKYHUB { get; set; }
        public virtual DbSet<TB_PRODUTO_ESP_SKYHUB> TB_PRODUTO_ESP_SKYHUB { get; set; }
        public virtual DbSet<TB_PRODUTO_IMAGEM_SKYHUB> TB_PRODUTO_IMAGEM_SKYHUB { get; set; }
        public virtual DbSet<TB_PRODUTO_SKYHUB> TB_PRODUTO_SKYHUB { get; set; }
        public virtual DbSet<TB_SINCRONIZACAO_SKYHUB> TB_SINCRONIZACAO_SKYHUB { get; set; }
        public virtual DbSet<TB_SEQUENCIA> TB_SEQUENCIA { get; set; }
        public virtual DbSet<TB_PEDIDO_ITEM> TB_PEDIDO_ITEM { get; set; }
    }
}
