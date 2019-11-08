using Cliente.Forms.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Newtonsoft.Json;
using Cliente.POCO;
using System.Reflection;

namespace Cliente.Forms
{
    public partial class FCadastroCad : FModeloCad
    {
        #region Properties
        
        public List<TB_ESTADO> EstadoPropriedade { get; set; }
        public List<TB_CIDADE> CidadePropriedade { get; set; }
        public object Propriedade { get; set; }
        public BindingSource TipoCadastroBindingSource { get; set; }
        public BindingSource ClassCadastroBindingSource { get; set; }
        public BindingSource VendedorBindingSource { get; set; }
        public BindingSource EstadoBindingSource { get; set; }        
        public BindingSource PaisBindingSource { get; set; }
        public BindingSource CidadePropriedadeBindingSource { get; set; }
        public BindingSource PropriedadeBindingSource { get; set; }
        public BindingSource CadastroBindingSource { get; set; }
        public BindingSource EnderecoBindingSource { get; set; }
        public BindingSource QualificacaoSocioBindingSource { get; set; }
        public BindingSource FilialBindingSource { get; set; }
        public BindingSource RegimeTributarioBindingSource { get; set; }
        public BindingSource fisicaJuridicaBindingSource { get; set; }
        public BindingSource ConsumidorFinalBindingSource { get; set; }
        public BindingSource OperadoraBindingSource { get; set; }
        public BindingSource RegiaoBindingSource { get; set; }
        public BindingSource MotivoBindingSource { get; set; }
        public BindingSource GarantiaBindingSource { get; set; }
        public BindingSource QualificacaoBindingSource { get; set; }
        public BindingSource AvalistaBindingSource { get; set; }

        #endregion

        #region Acesso assíncrono ao servidor

        private async void BuscaDados()
        {
            // Busca o cadastro no servidor
            if (CadastroBindingSource == null)
                CadastroBindingSource = new BindingSource();

            if ("VISUALIZAR ALTERAR".Contains(this.LayoutTela))
            {
                var cadastro = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                    ConfigurationManager.AppSettings["UriCadastro"], string.Format("{0}/{1}", "Get", this.ChaveConsulta["Codigo"])
                    )), new TB_CADASTRO());
                CadastroBindingSource.DataSource = cadastro;
            }
            else
            if ("INSERIR".Contains(this.LayoutTela))
            {
                var cadastro = new TB_CADASTRO();
                CadastroBindingSource.DataSource = cadastro;
                CadastroBindingSource.AddNew();
                (CadastroBindingSource.Current as TB_CADASTRO).COD_CADASTRO = -1;
            }   

            // DataBindings
            tbsCodigo.DataBindings.Add("Text", CadastroBindingSource.Current, "COD_CADASTRO");
            tbsNome.DataBindings.Add("Text", CadastroBindingSource.Current, "NOME");
            tbsNomeFantasia.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_FANTASIA");
            tbsTelefone.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_TELEFONE");
            tbsCelular.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_CELULAR");
            tbsEmailXML.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_E_MAIL");
            tbsEmailContato.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_E_MAIL1");
            tbsEmpresa.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_EMPRESA");
            tbsFuncao.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_FUNCAO");
            tbsTelefoneEmpresa.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_FONE_EMPRESA");
            tbsGarantiasOutras.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_GARANTIAS_OUTRAS");
            tbsPessoaAutorizada1.DataBindings.Add("Text", CadastroBindingSource.Current, "NOME_AUTORIZADO1");
            tbsPessoaAutorizada2.DataBindings.Add("Text", CadastroBindingSource.Current, "NOME_AUTORIZADO2");
            tbsPessoaAutorizada3.DataBindings.Add("Text", CadastroBindingSource.Current, "NOME_AUTORIZADO3");
            tbsCpfAutorizado1.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_CPF_AUTORIZADO1");
            tbsCpfAutorizado2.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_CPF_AUTORIZADO2");
            tbsCpfAutorizado3.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_CPF_AUTORIZADO3");
            tbsTelComercial.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_TELEFONE_COMERCIAL");
            tbsCrc.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_CRC");
            tbsSequencial.DataBindings.Add("Text", CadastroBindingSource.Current, "NUM_SEQ_CRC");
            tbsValidadeCrc.DataBindings.Add("Text", CadastroBindingSource.Current, "DT_CRC");
            tbsDtFimSociedade.DataBindings.Add("Text", CadastroBindingSource.Current, "DT_CANCELAMENTO");
            tbsPercParticCapitalTotal.DataBindings.Add("Text", CadastroBindingSource.Current, "PERC_CAP_TOT");
            tbsPercParticCapitalVolante.DataBindings.Add("Text", CadastroBindingSource.Current, "PERC_CAP_VOT");
            tbsDtSeprocado.DataBindings.Add("Text", CadastroBindingSource.Current, "DT_SEPROCADO");
            tbsDtCancelamento.DataBindings.Add("Text", CadastroBindingSource.Current, "DT_CANCELAMENTO");
            tbsObsCancelamento.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_OBS");
            tbsRefComercial1.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_REF_COMERCIAL1");
            tbsRefComercial2.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_REF_COMERCIAL2");
            tbsRefBanco1.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_REF_BANCO1");
            tbsRefBanco2.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_REF_BANCO2");
            tbsRefParenteNome.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_REF_PARENTE_NOME");
            tbsRefParenteEnderceo.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_REF_PARENTE_ENDERECO");
            tbsRefParenteCidade.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_REF_PARENTE_CIDADE");
            tbsRefTelefoneParente.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_REF_PARENTE_TELEFONE");
            tbsRefOutras.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_REF_OUTRAS");

            //cbsTipoCadastro
            if (TipoCadastroBindingSource == null)
                TipoCadastroBindingSource = new BindingSource();
            TipoCadastroBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriTipoCadastro"]
                )), new List<TB_TIPO_CADASTRO>());
            if ((CadastroBindingSource.Current as TB_CADASTRO).COD_TIPO_CADASTRO > 0)
            {
                var obj = TipoCadastroBindingSource.List.OfType<TB_TIPO_CADASTRO>().First(c => c.COD_TIPO_CADASTRO == ((TB_CADASTRO)CadastroBindingSource.Current).COD_TIPO_CADASTRO);
                var pos = TipoCadastroBindingSource.IndexOf(obj);
                TipoCadastroBindingSource.Position = pos;
                obj = null;
                pos = 0;
            }
            cbsTipoCadastro.BindingSource = TipoCadastroBindingSource;
            cbsTipoCadastro.DisplayMember = "DESC_TIPO_CADASTRO";
            

            //cbsFisicaJuridica
            List<POCO.FisicaJuridica> ListaFisicaJuridica = new List<POCO.FisicaJuridica>();
            var f = new POCO.FisicaJuridica();
            f.IND_FISICA_JURIDICA = "F";
            f.DESC_FISICA_JURIDICA = "FISICA";
            var j = new POCO.FisicaJuridica();
            j.IND_FISICA_JURIDICA = "J";
            j.DESC_FISICA_JURIDICA = "JURIDICA";
            ListaFisicaJuridica.Add(f);
            ListaFisicaJuridica.Add(j);
            fisicaJuridicaBindingSource = new BindingSource();
            fisicaJuridicaBindingSource.DataSource = ListaFisicaJuridica;
            cbsFisicaJuridica.BindingSource = fisicaJuridicaBindingSource;
            cbsFisicaJuridica.DisplayMember = "DESC_FISICA_JURIDICA";
            if ((CadastroBindingSource.Current as TB_CADASTRO).IND_FISICA_JURIDICA != null)
            {
                var objFJ = fisicaJuridicaBindingSource.List.OfType<POCO.FisicaJuridica>().First(p => p.IND_FISICA_JURIDICA == ((TB_CADASTRO)CadastroBindingSource.Current).IND_FISICA_JURIDICA);
                var pos = fisicaJuridicaBindingSource.IndexOf(objFJ);
                fisicaJuridicaBindingSource.Position = pos;
                objFJ = null;
                pos = 0;
            }

            //cbsGarantias
            List<Generico> ListaGarantias = new List<Generico>();
            ListaGarantias.Add(new Generico() { Codigo = "", Descricao = "" });
            ListaGarantias.Add(new Generico() { Codigo = "1", Descricao = "CPR" });
            ListaGarantias.Add(new Generico() { Codigo = "2", Descricao = "DUPLICATA" });
            ListaGarantias.Add(new Generico() { Codigo = "3", Descricao = "OUTRAS" });
            if (GarantiaBindingSource == null)
            {
                GarantiaBindingSource = new BindingSource();
                GarantiaBindingSource.DataSource = ListaGarantias;
            }
            cbsGarantias.BindingSource = GarantiaBindingSource;
            cbsGarantias.DisplayMember = "Descricao";
            if ((CadastroBindingSource.Current as TB_CADASTRO).DESC_GARANTIAS != null)
            {
                var obj = GarantiaBindingSource.List.OfType<POCO.Generico>().First(p => p.Descricao == ((TB_CADASTRO)CadastroBindingSource.Current).DESC_GARANTIAS);
                var pos = GarantiaBindingSource.IndexOf(obj);
                GarantiaBindingSource.Position = pos;
                obj = null;
                pos = 0;
            }

            //cbsQualificacao
            List<Generico> ListaQualificacao = new List<Generico>();
            ListaQualificacao.Add(new Generico() { Codigo = "", Descricao = "" });
            ListaQualificacao.Add(new Generico() { Codigo = "203", Descricao = "DIRETOR" });
            ListaQualificacao.Add(new Generico() { Codigo = "204", Descricao = "CONSELHEIRO DE ADMINISTRAÇÃO" });
            ListaQualificacao.Add(new Generico() { Codigo = "205", Descricao = "ADMINISTRADOR" });
            ListaQualificacao.Add(new Generico() { Codigo = "206", Descricao = "ADMINISTRADOR DE GRUPO" });
            ListaQualificacao.Add(new Generico() { Codigo = "207", Descricao = "ADMINISTRADOR DE SOCIEDADE FILIADA" });
            ListaQualificacao.Add(new Generico() { Codigo = "220", Descricao = "ADMINISTRADOR JUDICIAL - PESSOA FÍSICA" });
            ListaQualificacao.Add(new Generico() { Codigo = "222", Descricao = "ADMINISTRADOR JUDICIAL - PESSOA JURÍDICA - PROFISSIONAL RESPONSÁVEL" });
            ListaQualificacao.Add(new Generico() { Codigo = "223", Descricao = "ADMINISTRADOR JUDICIAL/GESTOR" });
            ListaQualificacao.Add(new Generico() { Codigo = "226", Descricao = "GESTOR JUDICIAL" });
            ListaQualificacao.Add(new Generico() { Codigo = "309", Descricao = "PROCURADOR" });
            ListaQualificacao.Add(new Generico() { Codigo = "312", Descricao = "INVENTARIANTE" });
            ListaQualificacao.Add(new Generico() { Codigo = "313", Descricao = "LIQUIDANTE" });
            ListaQualificacao.Add(new Generico() { Codigo = "315", Descricao = "INTERVENTOR" });
            ListaQualificacao.Add(new Generico() { Codigo = "801", Descricao = "EMPRESÁRIO" });
            ListaQualificacao.Add(new Generico() { Codigo = "900", Descricao = "CONTADOR" });
            ListaQualificacao.Add(new Generico() { Codigo = "999", Descricao = "OUTROS" });

            if (QualificacaoBindingSource == null)
            {
                QualificacaoBindingSource = new BindingSource();
                QualificacaoBindingSource.DataSource = ListaQualificacao;
            }
            cbsQualificacao.BindingSource = QualificacaoBindingSource;
            cbsQualificacao.DisplayMember = "Descricao";
            if ((CadastroBindingSource.Current as TB_CADASTRO).DESC_QUALIFICACAO != null)
            {
                var obj = QualificacaoBindingSource.List.OfType<POCO.Generico>().First(p => p.Descricao == ((TB_CADASTRO)CadastroBindingSource.Current).DESC_GARANTIAS);
                var pos = QualificacaoBindingSource.IndexOf(obj);
                QualificacaoBindingSource.Position = pos;
                obj = null;
                pos = 0;
            }

            //cbsRegimeTributário
            List<POCO.RegimeTributario> ListaRegimeTributario = new List<RegimeTributario>();
            var nRegime = new RegimeTributario();
            nRegime.IND_REGIME_TRIBUTARIO = null;
            nRegime.DESC_REGIME_TRIBUTARIO = string.Empty;
            var simples = new RegimeTributario();
            simples.IND_REGIME_TRIBUTARIO = 1;
            simples.DESC_REGIME_TRIBUTARIO = "SIMPLES NACIONAL";
            var simplesExcesso = new RegimeTributario();
            simplesExcesso.IND_REGIME_TRIBUTARIO = 2;
            simplesExcesso.DESC_REGIME_TRIBUTARIO = "SIMPLES NACIONAL - EXCESSO";
            var normal = new RegimeTributario();
            normal.IND_REGIME_TRIBUTARIO = 3;
            normal.DESC_REGIME_TRIBUTARIO = "NORMAL";
            ListaRegimeTributario.Add(nRegime);
            ListaRegimeTributario.Add(simples);
            ListaRegimeTributario.Add(simplesExcesso);
            ListaRegimeTributario.Add(normal);
            RegimeTributarioBindingSource = new BindingSource();
            RegimeTributarioBindingSource.DataSource = ListaRegimeTributario;
            cbsRegimeTributario.BindingSource = RegimeTributarioBindingSource;
            cbsRegimeTributario.DisplayMember = "DESC_REGIME_TRIBUTARIO";
            if((CadastroBindingSource.Current as TB_CADASTRO).IND_REGIME_TRIBUTARIO != null)
            {
                var objRegime = RegimeTributarioBindingSource.List.OfType<POCO.RegimeTributario>().First(p => p.IND_REGIME_TRIBUTARIO == ((TB_CADASTRO)CadastroBindingSource.Current).IND_REGIME_TRIBUTARIO);
                var pos = RegimeTributarioBindingSource.IndexOf(objRegime);
                RegimeTributarioBindingSource.Position = pos;
                objRegime = null;
                pos = 0;
            }

            //cbsConsumidorFinal
            List<ConsumidorFinal> ListaConsumidorFinal = new List<ConsumidorFinal>();
            var nnConsumidor = new ConsumidorFinal();
            nnConsumidor.IND_CONSUMIDOR_FINAL = null;
            nnConsumidor.DESC_CONSUMIDOR_FINAL = string.Empty;
            var sConsumidor = new ConsumidorFinal();
            sConsumidor.IND_CONSUMIDOR_FINAL = "S";
            sConsumidor.DESC_CONSUMIDOR_FINAL = "SIM";
            var nConsumidor = new ConsumidorFinal();
            nConsumidor.IND_CONSUMIDOR_FINAL = "N";
            nConsumidor.DESC_CONSUMIDOR_FINAL = "NAO";            
            ListaConsumidorFinal.Add(nnConsumidor);
            ListaConsumidorFinal.Add(sConsumidor);
            ListaConsumidorFinal.Add(nConsumidor);
            ConsumidorFinalBindingSource = new BindingSource();
            ConsumidorFinalBindingSource.DataSource = ListaConsumidorFinal;
            cbsConsumidorFinal.BindingSource = ConsumidorFinalBindingSource;
            cbsConsumidorFinal.DisplayMember = "DESC_CONSUMIDOR_FINAL";
            if ((CadastroBindingSource.Current as TB_CADASTRO).IND_CONSUMIDOR_FINAL != null)
            {
                var objConsumidor = ConsumidorFinalBindingSource.List.OfType<ConsumidorFinal>().First(p => p.IND_CONSUMIDOR_FINAL == ((TB_CADASTRO)CadastroBindingSource.Current).IND_CONSUMIDOR_FINAL);
                var pos = ConsumidorFinalBindingSource.IndexOf(objConsumidor);
                ConsumidorFinalBindingSource.Position = pos;
                objConsumidor = null;
                pos = 0;
            }

            //cbsClassCadastro            
            if (ClassCadastroBindingSource == null)
                ClassCadastroBindingSource = new BindingSource();
            ClassCadastroBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriClassCadastro"]
                )), new List<TB_CLASS_CADASTRO>());
            TB_CLASS_CADASTRO classcad = new TB_CLASS_CADASTRO();
            classcad.COD_CLASS_CADASTRO = 0;
            classcad.DESC_CLASSIFICACAO = string.Empty;
            ClassCadastroBindingSource.Insert(0, classcad);
            cbsClassificacao.BindingSource = ClassCadastroBindingSource;
            cbsClassificacao.DisplayMember = "DESC_CLASSIFICACAO";            
            if ((CadastroBindingSource.Current as TB_CADASTRO).COD_CLASS_CADASTRO != null)
            {
                var objCC = ClassCadastroBindingSource.List.OfType<TB_CLASS_CADASTRO>().First(c => c.COD_CLASS_CADASTRO == ((TB_CADASTRO)CadastroBindingSource.Current).COD_CLASS_CADASTRO);
                var pos = ClassCadastroBindingSource.IndexOf(objCC);
                ClassCadastroBindingSource.Position = pos;
                objCC = null;
                pos = 0;
            }
            else
                cbsClassificacao.SelectedIndex = -1;

            //cbsVendedor
            if (VendedorBindingSource == null)
                VendedorBindingSource = new BindingSource();
            VendedorBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
            ConfigurationManager.AppSettings["UriVendedor"]
            )), new List<TB_VENDEDOR>());
            TB_VENDEDOR vendedor = new TB_VENDEDOR();
            vendedor.COD_VENDEDOR = 0;
            vendedor.NOME = string.Empty;
            VendedorBindingSource.Insert(0, vendedor);
            cbsVendedor.BindingSource = VendedorBindingSource;
            cbsVendedor.DisplayMember = "NOME";            
            if ((CadastroBindingSource.Current as TB_CADASTRO).COD_VENDEDOR != null)
            {
                var objV = VendedorBindingSource.List.OfType<TB_VENDEDOR>().First(c => c.COD_VENDEDOR == ((TB_CADASTRO)CadastroBindingSource.Current).COD_VENDEDOR);
                var pos = VendedorBindingSource.IndexOf(objV);
                VendedorBindingSource.Position = pos;
                objV = null;
                pos = 0;
            }
            else
                cbsVendedor.SelectedIndex = -1;

            //cbsQualificacaoSocio
            if (QualificacaoSocioBindingSource == null)
                QualificacaoSocioBindingSource = new BindingSource();
            QualificacaoSocioBindingSource.DataSource
                = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriQualificacaoSocio"]
                )), new List<TB_QUALIFICACAO_SOCIO>());
            cbsQualificacaoSocio.BindingSource = QualificacaoSocioBindingSource;
            cbsQualificacaoSocio.DisplayMember = "DESC_QUALIFICACAO_SOCIO";            
            if ((CadastroBindingSource.Current as TB_CADASTRO).COD_QUALIFICACAO_SOCIO != null)
            {
                var objQualif
                    = QualificacaoSocioBindingSource.List.OfType<TB_QUALIFICACAO_SOCIO>().First(q => q.COD_QUALIFICACAO_SOCIO == (CadastroBindingSource.Current as TB_CADASTRO).COD_QUALIFICACAO_SOCIO);
                var pos = QualificacaoSocioBindingSource.IndexOf(objQualif);
                QualificacaoSocioBindingSource.Position = pos;
                objQualif = null;
                pos = 0;
            }
            else
            {
                cbsQualificacaoSocio.SelectedIndex = -1;
            }

            //cbsFilial
            if (FilialBindingSource == null)
                FilialBindingSource = new BindingSource();            
            FilialBindingSource.DataSource
                = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                string.Format("{0}/{1}", ConfigurationManager.AppSettings["UriFilial"], "Get")
                )), new List<TB_FILIAL>());
            TB_FILIAL filial = new TB_FILIAL();
            filial.COD_FILIAL = 0;
            filial.DESC_FILIAL = string.Empty;
            FilialBindingSource.Insert(0, filial);
            cbsFilial.BindingSource = FilialBindingSource;
            cbsFilial.DisplayMember = "DESC_FILIAL";
            if ((CadastroBindingSource.Current as TB_CADASTRO).COD_FILIAL != null)
            {   
                var objFilial
                    = FilialBindingSource.List.OfType<TB_FILIAL>().First(q => q.COD_FILIAL == (CadastroBindingSource.Current as TB_CADASTRO).COD_FILIAL);
                var pos = FilialBindingSource.IndexOf(objFilial);
                FilialBindingSource.Position = pos;
                objFilial = null;
                pos = 0;
            }
            else
            {
                cbsFilial.SelectedIndex = -1;
            }

            //cbsOperadora
            List<Generico> ListaOperadora = new List<Generico>();            
            ListaOperadora.Add(new Generico() { Codigo = "", Descricao = "" });
            ListaOperadora.Add(new Generico() { Codigo = "Tim", Descricao = "TIM" });
            ListaOperadora.Add(new Generico() { Codigo = "Claro", Descricao = "CLARO" });
            ListaOperadora.Add(new Generico() { Codigo = "Oi", Descricao = "OI" });
            ListaOperadora.Add(new Generico() { Codigo = "Vivo", Descricao = "VIVO" });
            ListaOperadora.Add(new Generico() { Codigo = "Embratel", Descricao = "EMBRATEL" });
            ListaOperadora.Add(new Generico() { Codigo = "Sercomtel", Descricao = "SERCOMTEL" });
            ListaOperadora.Add(new Generico() { Codigo = "Nextel", Descricao = "NEXTEL"});
            OperadoraBindingSource = new BindingSource();
            OperadoraBindingSource.DataSource = ListaOperadora;
            cbsOperadora.BindingSource = OperadoraBindingSource;
            cbsOperadora.DisplayMember = "Descricao";
            if ((CadastroBindingSource.Current as TB_CADASTRO).DESC_OPERADORA_CELULAR != null)
            {
                var objOperadora = OperadoraBindingSource.List.OfType<Generico>().First(p => p.Codigo == ((TB_CADASTRO)CadastroBindingSource.Current).DESC_OPERADORA_CELULAR);
                var pos = OperadoraBindingSource.IndexOf(objOperadora);
                OperadoraBindingSource.Position = pos;
                objOperadora = null;
                pos = 0;
            }

            //cbsRegiao            
            if (RegiaoBindingSource == null)
                RegiaoBindingSource = new BindingSource();
            RegiaoBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriRegiao"]
                )), new List<TB_REGIAO>());
            TB_REGIAO regiao = new TB_REGIAO();
            regiao.COD_REGIAO = 0;
            regiao.DESC_REGIAO = string.Empty;
            RegiaoBindingSource.Insert(0, regiao);
            cbsRegiao.BindingSource = RegiaoBindingSource;
            cbsRegiao.DisplayMember = "DESC_REGIAO";
            if ((CadastroBindingSource.Current as TB_CADASTRO).COD_REGIAO != null)
            {
                var objRegiao = RegiaoBindingSource.List.OfType<TB_REGIAO>().First(c => c.COD_REGIAO == ((TB_CADASTRO)CadastroBindingSource.Current).COD_REGIAO);
                var pos = RegiaoBindingSource.IndexOf(objRegiao);
                RegiaoBindingSource.Position = pos;
                objRegiao = null;
                pos = 0;
            }
            else
                cbsRegiao.SelectedIndex = -1;

            //cbsMotivo
            if (MotivoBindingSource == null)
                MotivoBindingSource = new BindingSource();
            MotivoBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                string.Format("{0}/{1}", ConfigurationManager.AppSettings["UriMotivo"], "GetPersonalizado")
                )), new List<TB_MOTIVO>());
            TB_MOTIVO motivo = new TB_MOTIVO();
            motivo.COD_MOTIVO = 0;
            motivo.DESC_MOTIVO = string.Empty;
            MotivoBindingSource.Insert(0, motivo);
            cbsMotivoCancelamento.BindingSource = MotivoBindingSource;
            cbsMotivoCancelamento.DisplayMember = "DESC_MOTIVO";
            if ((CadastroBindingSource.Current as TB_CADASTRO).COD_MOTIVO != null)
            {
                var objMotivo = MotivoBindingSource.List.OfType<TB_MOTIVO>().First(m => m.COD_MOTIVO == ((TB_MOTIVO)CadastroBindingSource.Current).COD_MOTIVO);
                var pos = MotivoBindingSource.IndexOf(objMotivo);
                MotivoBindingSource.Position = pos;
                objMotivo = null;
                pos = 0;
            }
            else
                cbsMotivoCancelamento.SelectedIndex = -1;

            //cbsPais
            if (PaisBindingSource == null)
                PaisBindingSource = new BindingSource();
            PaisBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                string.Format("{0}/{1}", ConfigurationManager.AppSettings["UriPais"], "Get")
                )), new List<TB_PAIS>());
            TB_PAIS pais = new TB_PAIS();
            pais.COD_PAIS = 0;
            pais.DESC_PAIS = string.Empty;
            PaisBindingSource.Insert(0, pais);
            cbsPais.BindingSource = PaisBindingSource;
            cbsPais.DisplayMember = "DESC_PAIS";
            if ((CadastroBindingSource.Current as TB_CADASTRO).COD_PAIS != null)
            {
                var objPais = PaisBindingSource.List.OfType<TB_PAIS>().First(m => m.COD_PAIS == ((TB_CADASTRO)CadastroBindingSource.Current).COD_PAIS);
                var pos = PaisBindingSource.IndexOf(objPais);
                PaisBindingSource.Position = pos;
                objPais = null;
                pos = 0;
            }
            else
                cbsPais.SelectedIndex = -1;

            //cbsAvalista
            if (AvalistaBindingSource == null)
                AvalistaBindingSource = new BindingSource();
            cbsAvalista.BindingSource = AvalistaBindingSource;

            //dgvPropriedades
            if (PropriedadeBindingSource == null)
                PropriedadeBindingSource = new BindingSource();            
            dgvPropriedades.DataSource = PropriedadeBindingSource;            
            PropriedadeBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriPropriedade"], string.Format("{0}/{1}", "Get", ((TB_CADASTRO)CadastroBindingSource.Current).COD_CADASTRO)
                )), new List<TB_PROPRIEDADE>());
            DataGridViewTextBoxColumn dgvcCidade = new DataGridViewTextBoxColumn();
            dgvcCidade.DataPropertyName = "CIDADE";
            dgvcCidade.HeaderText = "CIDADE";            
            dgvPropriedades.Columns.Add(dgvcCidade);
            dgvPropriedades.Columns["TB_CIDADE"].Visible = false;

            foreach (var col in dgvPropriedades.Columns)
            {
                var colunas = "DESC_PROPRIEDADE DESC_LOCALIDADE NUM_AREA CIDADE DESC_CRI VAL_PROPRIEDADE NUM_MATRICULA";
                if (colunas.Contains(((DataGridViewColumn)col).Name))
                {
                    ((DataGridViewColumn)col).Visible = true;
                    if ((col as DataGridViewColumn).Name.Equals("DESC_PROPRIEDADE"))
                    {
                        (col as DataGridViewColumn).HeaderText = "PROPRIEDADE";
                        (col as DataGridViewColumn).DisplayIndex = 0;
                        (col as DataGridViewColumn).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    if ((col as DataGridViewColumn).Name.Equals("DESC_LOCALIDADE"))
                    {
                        (col as DataGridViewColumn).HeaderText = "ENDEREÇO";
                        (col as DataGridViewColumn).DisplayIndex = 1;
                    }
                    if ((col as DataGridViewColumn).Name.Equals("VAL_PROPRIEDADE"))
                    {
                        (col as DataGridViewColumn).HeaderText = "VALOR";
                        (col as DataGridViewColumn).DisplayIndex = 2;
                    }
                    if ((col as DataGridViewColumn).Name.Equals("NUM_AREA"))                    
                        (col as DataGridViewColumn).HeaderText = "AREA";
                    if ((col as DataGridViewColumn).Name.Equals("DESC_CRI"))
                        (col as DataGridViewColumn).HeaderText = "CRI";
                    if ((col as DataGridViewColumn).Name.Equals("NUM_MATRICULA"))
                        (col as DataGridViewColumn).HeaderText = "MATRICULA";
                }
                else
                    ((DataGridViewColumn)col).Visible = false;
            }            

            //dgvEndereco
            if (EnderecoBindingSource == null)
                EnderecoBindingSource = new BindingSource();
            dgvEndereco.DataSource = EnderecoBindingSource;
            var endereco = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriEndereco"], string.Format("{0}/{1}", "Get", ((TB_CADASTRO)CadastroBindingSource.Current).COD_CADASTRO)
                )), new List<TB_CADASTRO_ENDERECOS>());
            EnderecoBindingSource.DataSource = endereco;
            var colEndereco = new string[] { "DESC_ENDERECO", "DESC_CIDADE", "COD_ESTADO", "DESC_CEP" };
            foreach (var col in dgvEndereco.Columns)
            {
                if (colEndereco.Contains(((DataGridViewColumn)col).Name))
                {
                    ((DataGridViewColumn)col).Visible = true;
                    if (((DataGridViewColumn)col).Name.Equals("DESC_ENDERECO"))
                    {
                        (col as DataGridViewColumn).HeaderText = "ENDEREÇO";
                        (col as DataGridViewColumn).DisplayIndex = 1;
                        (col as DataGridViewColumn).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    if ((col as DataGridViewColumn).Name.Equals("DESC_CIDADE"))
                    {
                        (col as DataGridViewColumn).HeaderText = "CIDADE";
                        (col as DataGridViewColumn).DisplayIndex = 2;
                    }
                    if ((col as DataGridViewColumn).Name.Equals("COD_ESTADO"))
                    {
                        (col as DataGridViewColumn).HeaderText = "ESTADO";
                        (col as DataGridViewColumn).DisplayIndex = 3;
                    }
                    if ((col as DataGridViewColumn).Name.Equals("DESC_CEP"))
                    {
                        (col as DataGridViewColumn).HeaderText = "CEP";
                        (col as DataGridViewColumn).DisplayIndex = 4;
                    }
                }
                else
                {
                    (col as DataGridViewColumn).Visible = false;
                }

            }

        }

        #endregion

        public FCadastroCad(string layout) : base(layout)
        {
            InitializeComponent();
            this.Personaliza();            
        }

        private async void Salvar()
        {
            (CadastroBindingSource.Current as TB_CADASTRO).TB_CADASTRO_ENDERECOS
                .Add(EnderecoBindingSource.Current as TB_CADASTRO_ENDERECOS);
            (CadastroBindingSource.Current as TB_CADASTRO).TB_PROPRIEDADE
                .Add(PropriedadeBindingSource.Current as TB_PROPRIEDADE);
            (CadastroBindingSource.Current as TB_CADASTRO).COD_TIPO_CADASTRO
                = (TipoCadastroBindingSource.Current as TB_TIPO_CADASTRO).COD_TIPO_CADASTRO;

            if ((FilialBindingSource.Current as TB_FILIAL).COD_FILIAL == 0)
                (CadastroBindingSource.Current as TB_CADASTRO).COD_FILIAL = null;
            else
                (CadastroBindingSource.Current as TB_CADASTRO).COD_FILIAL
                    = (FilialBindingSource.Current as TB_FILIAL).COD_FILIAL;

            if ((VendedorBindingSource.Current as TB_VENDEDOR).COD_VENDEDOR == 0)
                (CadastroBindingSource.Current as TB_CADASTRO).COD_VENDEDOR = null;
            else
                (CadastroBindingSource.Current as TB_CADASTRO).COD_VENDEDOR
                    = (VendedorBindingSource.Current as TB_VENDEDOR).COD_VENDEDOR;
            if ((ClassCadastroBindingSource.Current as TB_CLASS_CADASTRO).COD_CLASS_CADASTRO == 0)
                (CadastroBindingSource.Current as TB_CADASTRO).COD_CLASS_CADASTRO = null;
            else
                (CadastroBindingSource.Current as TB_CADASTRO).COD_CLASS_CADASTRO
                    = (ClassCadastroBindingSource.Current as TB_CLASS_CADASTRO).COD_CLASS_CADASTRO;
            if ((MotivoBindingSource.Current as TB_MOTIVO).COD_MOTIVO == 0)
                (CadastroBindingSource.Current as TB_CADASTRO).COD_MOTIVO = null;
            else
                (CadastroBindingSource.Current as TB_CADASTRO).COD_MOTIVO 
                    = (MotivoBindingSource.Current as TB_MOTIVO).COD_MOTIVO;
            if((GarantiaBindingSource.Current as Generico).Descricao == string.Empty)
                (CadastroBindingSource.Current as TB_CADASTRO).DESC_GARANTIAS = null;
            else
                (CadastroBindingSource.Current as TB_CADASTRO).DESC_GARANTIAS
                    = (GarantiaBindingSource.Current as Generico).Descricao;
            if ((QualificacaoBindingSource.Current as Generico).Descricao == string.Empty)
                (CadastroBindingSource.Current as TB_CADASTRO).DESC_QUALIFICACAO = null;
            else
                (CadastroBindingSource.Current as TB_CADASTRO).DESC_QUALIFICACAO
                    = (QualificacaoBindingSource.Current as Generico).Descricao;
            if ((PaisBindingSource.Current as TB_PAIS).DESC_PAIS == string.Empty)
                (CadastroBindingSource.Current as TB_CADASTRO).COD_PAIS = null;
            else
                (CadastroBindingSource.Current as TB_CADASTRO).COD_PAIS
                    = (PaisBindingSource.Current as TB_PAIS).COD_PAIS;
            (CadastroBindingSource.Current as TB_CADASTRO).IND_REGIME_TRIBUTARIO
                = (RegimeTributarioBindingSource.Current as RegimeTributario).IND_REGIME_TRIBUTARIO;
            (CadastroBindingSource.Current as TB_CADASTRO).IND_CONSUMIDOR_FINAL
                = (ConsumidorFinalBindingSource.Current as ConsumidorFinal).IND_CONSUMIDOR_FINAL;
            (CadastroBindingSource.Current as TB_CADASTRO).DESC_OPERADORA_CELULAR
                = (OperadoraBindingSource.Current as Generico).Codigo;
            (CadastroBindingSource.Current as TB_CADASTRO).COD_REGIAO
                = (RegiaoBindingSource.Current as TB_REGIAO).COD_REGIAO;

            CadastroBindingSource.EndEdit();
            MessageBox.Show(
            await RunAsyncPost(
                string.Format("{0}/{1}", ConfigurationManager.AppSettings["UriCadastro"], "Save"), CadastroBindingSource.Current)

                );
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {   
            CadastroBindingSource.CancelEdit();
        }         

        private void cbsFisicaJuridica_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
            {
                MessageBox.Show(cbsFisicaJuridica.SelectedValue + " " + cbsFisicaJuridica.SelectedText);
            }
        }

        private void cbsFisicaJuridica_Leave(object sender, EventArgs e)
        {
            //MessageBox.Show(cbsFisicaJuridica.SelectedValue + " " + cbsFisicaJuridica.SelectedText);
        }

        

        private void FCadastroCad_Load(object sender, EventArgs e)
        {  
            BuscaDados();           
        }

        private void btAlterarProriedade_Click(object sender, EventArgs e)
        {
            if (PropriedadeBindingSource.Current == null)
                return;

            FPropriedadeCad f = new FPropriedadeCad("ALTERAR");                       
            f.PropriedadeBindingSource = PropriedadeBindingSource;            
            f.Show();            
        }

        private void btNovaPropriedade_Click(object sender, EventArgs e)
        {
            MessageBox.Show(((TB_PROPRIEDADE)PropriedadeBindingSource.Current).IND_TIPO_IMOVEL);
        }

        private void dgvPropriedades_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPropriedades.Rows[e.RowIndex].DataBoundItem != null)
            {
                if (dgvPropriedades.Columns[e.ColumnIndex].DataPropertyName.Equals("CIDADE"))
                    if ((dgvPropriedades.Rows[e.RowIndex].DataBoundItem as TB_PROPRIEDADE).TB_CIDADE != null)
                        e.Value = (((dgvPropriedades.Rows[e.RowIndex].DataBoundItem as TB_PROPRIEDADE).TB_CIDADE).DESC_CIDADE);
            }
        }

        private void btAlterarEndereco_Click(object sender, EventArgs e)
        {
            if (EnderecoBindingSource.Current == null)
                return;

            FEnderecoCad f = new FEnderecoCad("ALTERAR");
            f.EnderecoBindingSource = EnderecoBindingSource;
            f.Show();
        }

        private void cbsFisicaJuridica_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbsGarantias_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbsGarantiasOutras.Enabled = (cbsGarantias.Text == "OUTRAS");
        }

        private async void cbsAvalista_ComboBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (cbsAvalista.SelectedIndex == -1 && e.KeyCode == Keys.Enter)
            {
                var cad = new TB_CADASTRO();
                if (!string.IsNullOrEmpty(cbsAvalista.Text))
                {
                    cad.NOME = cbsAvalista.Text;
                    AvalistaBindingSource.DataSource =
                            JsonConvert.DeserializeAnonymousType((await RunAsyncPost(
                    string.Format("{0}/{1}", ConfigurationManager.AppSettings["UriCadastro"], "Get"), cad)
                ), new List<TB_CADASTRO>());
                    if (((TB_CADASTRO)CadastroBindingSource.Current).COD_AVALISTA != null)
                    {
                        var obj = AvalistaBindingSource.List.OfType<TB_CADASTRO>()
                            .First(c => c.COD_CADASTRO == ((TB_CADASTRO)CadastroBindingSource.Current).COD_AVALISTA);
                        var pos = AvalistaBindingSource.IndexOf(obj);
                        AvalistaBindingSource.Position = pos;
                        obj = null;
                        pos = 0;
                    }
                    cbsAvalista.DisplayMember = "NOME";
                    cbsAvalista.ValueMember = "COD_CADASTRO";
                    cbsAvalista.DroppedDown = true;
                }
            }
        }
    }

}