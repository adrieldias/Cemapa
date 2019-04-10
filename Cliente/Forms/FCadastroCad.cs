﻿using Cliente.Forms.Modelo;
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
        public BindingSource CidadeBindingSource { get; set; }
        public BindingSource PaisBindingSource { get; set; }
        public BindingSource EstadoPropriedadeBindingSource { get; set; }
        public BindingSource CidadePropriedadeBindingSource { get; set; }
        public BindingSource PropriedadeBindingSource { get; set; }
        public BindingSource CadastroBindingSource { get; set; }

        #endregion

        #region Dados Propriedade
        

        /// <summary>
        /// Busca os dados complementares no servidor
        /// </summary>
        private async void BuscaDadosPropriedade()
        {   
            this.EstadoPropriedade = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriEstado"]
                )), new List<TB_ESTADO>());

            var definition = new
            {
                Data = new[] {
                    new {
                        CODIGO = 0,
                        NOME = string.Empty,
                        ENDERECO = string.Empty,
                        CIDADE = string.Empty,
                        BAIRRO = string.Empty,
                        CEP = string.Empty,
                        AREA = 0.0,
                        VALOR = 0.0,
                        MATRICULA = 0,
                        CRI = string.Empty,
                        TIPO = DBNull.Value,
                        PROPRIO = string.Empty                        
                    }
                }
            };

            // Busca os dados no servidor
            var anonymousType = JsonConvert.DeserializeAnonymousType(
                (await RunAsyncGet(
                ConfigurationManager.AppSettings["UriPropriedade"], "GetPersonalizado/?COD_CADASTRO=600"
                ))
                , definition);
            this.Propriedade = anonymousType.Data;



            
            //BuscaCidadesPropriedade(Cadastro.COD_ESTADO);

            
        }

        private void AtualizaCbsCidadePropriedade()
        {
            //if (CidadePropriedade != null)
            //{
            //    if (CidadePropriedadeBindingSource == null)
            //        CidadePropriedadeBindingSource = new BindingSource();
            //    CidadeBindingSource.DataSource = CidadePropriedade;
            //    try
            //    {
            //        var objCid = CidadePropriedadeBindingSource.List.OfType<TB_CIDADE>().First(c => c.COD_CIDADE == 1);
            //        var pos = CidadePropriedadeBindingSource.IndexOf(objCid);
            //        CidadePropriedadeBindingSource.Position = pos;
            //        objCid = null;
            //        pos = 0;
            //    }
            //    catch (Exception e)
            //    {
            //        CidadePropriedadeBindingSource.Position = 0;
            //    }
            //    cbsCidadePropriedade.BindingSource = CidadePropriedadeBindingSource;
            //    cbsCidadePropriedade.DisplayMember = "DESC_CIDADE";
            //}
        }


        //private async void BuscaCidadesPropriedade(string estado)
        //{
        //    //Carrega apenas cidades do estado da propriedade
        //    if (EstadoPropriedadeBindingSource == null)
        //        EstadoPropriedadeBindingSource = new BindingSource();
        //    EstadoPropriedadeBindingSource.DataSource = Estado;
        //    var objEst = EstadoPropriedadeBindingSource.List.OfType<TB_ESTADO>().First(c => c.COD_ESTADO == estado);
        //    var pos = EstadoPropriedadeBindingSource.IndexOf(objEst);
        //    EstadoPropriedadeBindingSource.Position = pos;
        //    objEst = null;
        //    pos = 0;
        //    this.CidadePropriedade = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
        //        string.Format("{0}{1}", ConfigurationManager.AppSettings["UriCidade"], "GetCidadesPorEstado")
        //        , ((TB_ESTADO)EstadoPropriedadeBindingSource.Current).COD_ESTADO)), new List<TB_CIDADE>());
        //    AtualizaCbsCidadePropriedade();
        //}

        #endregion

        #region Acesso assíncrono ao servidor

        private async void BuscaDados()
        {
            // Busca o cadastro no servidor
            if (CadastroBindingSource == null)
                CadastroBindingSource = new BindingSource();
            CadastroBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriCadastro"], string.Format("{0}/{1}", "Get", this.ChaveConsulta["Codigo"])
                )), new TB_CADASTRO());

            txbsCodigo.DataBindings.Add("Text", CadastroBindingSource.Current, "COD_CADASTRO");            
            txbsNome.DataBindings.Add("Text", CadastroBindingSource.Current, "NOME");
            txbsNomeFantasia.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_FANTASIA");
            txbsTelefone.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_TELEFONE");
            txbsCelular.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_CELULAR");
            txbsEmailXML.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_E_MAIL");
            txbsEmailContato.DataBindings.Add("Text", CadastroBindingSource.Current, "DESC_E_MAIL1");
            
            //cbsTipoCadastro
            if(TipoCadastroBindingSource == null)
                TipoCadastroBindingSource = new BindingSource();
            TipoCadastroBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriTipoCadastro"]
                )), new List<TB_TIPO_CADASTRO>());
            var obj = TipoCadastroBindingSource.List.OfType<TB_TIPO_CADASTRO>().First(c => c.COD_TIPO_CADASTRO == ((TB_CADASTRO)CadastroBindingSource.Current).COD_TIPO_CADASTRO);
            var pos = TipoCadastroBindingSource.IndexOf(obj);
            TipoCadastroBindingSource.Position = pos;
            cbsTipoCadastro.BindingSource = TipoCadastroBindingSource;
            cbsTipoCadastro.DisplayMember = "DESC_TIPO_CADASTRO";
            obj = null;
            pos = 0;

            //cbsFisicaJuridica
            List<FisicaJuridica> ListaFisicaJuridica = new List<FisicaJuridica>();
            var f = new FisicaJuridica();
            f.IND_FISICA_JURIDICA = "F";
            f.DESC_FISICA_JURIDICA = "FISICA";
            var j = new FisicaJuridica();
            j.IND_FISICA_JURIDICA = "J";
            j.DESC_FISICA_JURIDICA = "JURIDICA";
            ListaFisicaJuridica.Add(f);
            ListaFisicaJuridica.Add(j);
            BindingSource fisicaJuridicaBindingSource = new BindingSource();
            fisicaJuridicaBindingSource.DataSource = ListaFisicaJuridica;
            cbsFisicaJuridica.BindingSource = fisicaJuridicaBindingSource;
            cbsFisicaJuridica.DisplayMember = "DESC_FISICA_JURIDICA";
            var objFJ = fisicaJuridicaBindingSource.List.OfType<FisicaJuridica>().First(p => p.IND_FISICA_JURIDICA == ((TB_CADASTRO)CadastroBindingSource.Current).IND_FISICA_JURIDICA);
            pos = fisicaJuridicaBindingSource.IndexOf(objFJ);
            fisicaJuridicaBindingSource.Position = pos;
            objFJ = null;
            pos = 0;

            //cbsClassCadastro            
            if(ClassCadastroBindingSource == null)
                ClassCadastroBindingSource = new BindingSource();
            ClassCadastroBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriClassCadastro"]
                )), new List<TB_CLASS_CADASTRO>());
            var objCC = ClassCadastroBindingSource.List.OfType<TB_CLASS_CADASTRO>().First(c => c.COD_CLASS_CADASTRO == ((TB_CADASTRO)CadastroBindingSource.Current).COD_CLASS_CADASTRO);
            pos = ClassCadastroBindingSource.IndexOf(objCC);
            ClassCadastroBindingSource.Position = pos;
            cbsClassificacao.BindingSource = ClassCadastroBindingSource;
            cbsClassificacao.DisplayMember = "DESC_CLASSIFICACAO";
            objCC = null;
            pos = 0;

            //cbsVendedor  
            if (!((TB_CADASTRO)CadastroBindingSource.Current).COD_VENDEDOR.Equals(null))
            {
                if(VendedorBindingSource == null)
                    VendedorBindingSource = new BindingSource();
                VendedorBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriVendedor"]
                )), new List<TB_VENDEDOR>());
                var objV = VendedorBindingSource.List.OfType<TB_VENDEDOR>().First(c => c.COD_VENDEDOR == ((TB_CADASTRO)CadastroBindingSource.Current).COD_VENDEDOR);
                pos = VendedorBindingSource.IndexOf(objV);
                VendedorBindingSource.Position = pos;
                cbsVendedor.BindingSource = VendedorBindingSource;
                cbsVendedor.DisplayMember = "NOME";
                objV = null;
                pos = 0;
            }

            //cbsEstado
            if (EstadoBindingSource == null)
                EstadoBindingSource = new BindingSource();
            EstadoBindingSource.CurrentItemChanged += new EventHandler(EstadoBindingSource_CurrentChanged);
            EstadoBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriEstado"]
                )), new List<TB_ESTADO>());
            var objEst = EstadoBindingSource.List.OfType<TB_ESTADO>().First(c => c.COD_ESTADO == ((TB_CADASTRO)CadastroBindingSource.Current).COD_ESTADO);
            pos = EstadoBindingSource.IndexOf(objEst);
            EstadoBindingSource.Position = pos;
            cbsEstado.BindingSource = EstadoBindingSource;
            cbsEstado.DisplayMember = "DESC_ESTADO";
            cbsEstado.ValueMember = "COD_ESTADO";
            objEst = null;
            pos = 0;
            

            //BuscaCidades(((TB_ESTADO)EstadoBindingSource.Current).COD_ESTADO);
            

            //cbsPais
            if (PaisBindingSource == null)
                PaisBindingSource = new BindingSource();
            PaisBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriPais"]
                )), new List<TB_PAIS>());
            var objPais = PaisBindingSource.List.OfType<TB_PAIS>().First(p => p.COD_PAIS == ((TB_CADASTRO)CadastroBindingSource.Current).COD_PAIS);
            pos = PaisBindingSource.IndexOf(objPais);
            PaisBindingSource.Position = pos;
            cbsPais.BindingSource = PaisBindingSource;
            cbsPais.DisplayMember = "DESC_PAIS";
            cbsPais.ValueMember = "COD_PAIS";
            objPais = null;
            pos = 0;

            //dgvPropriedades
            if (PropriedadeBindingSource == null)
                PropriedadeBindingSource = new BindingSource();            
            dgvPropriedades.DataSource = PropriedadeBindingSource;
            var definition = new
            {
                Data = new[] {
                    new {
                        CODIGO = 0,
                        NOME = string.Empty,
                        ENDERECO = string.Empty,
                        CIDADE = string.Empty,
                        BAIRRO = string.Empty,
                        CEP = string.Empty,
                        AREA = 0.0,
                        VALOR = 0.0,
                        MATRICULA = 0,
                        CRI = string.Empty,
                        TIPO = DBNull.Value,
                        PROPRIO = string.Empty
                    }
                }
            };            
            var anonymousType = JsonConvert.DeserializeAnonymousType(
                (await RunAsyncGet(
                    ConfigurationManager.AppSettings["UriPropriedade"], 
                    string.Format("{0}{1}", "GetPersonalizado/?COD_CADASTRO=", ((TB_CADASTRO)CadastroBindingSource.Current).COD_CADASTRO)
                ))
                , definition);
            PropriedadeBindingSource.DataSource = anonymousType.Data;
        }

        private async void BuscaCidades(string estado)
        {
            //Carrega apenas cidades do estado do cadastro             
            var objEst = EstadoBindingSource.List.OfType<TB_ESTADO>().First(c => c.COD_ESTADO == estado);
            var pos = EstadoBindingSource.IndexOf(objEst);
            EstadoBindingSource.Position = pos;
            objEst = null;
            pos = 0;
            if (CidadeBindingSource == null)
                CidadeBindingSource = new BindingSource();            
            CidadeBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                string.Format("{0}{1}", ConfigurationManager.AppSettings["UriCidade"], "GetCidadesPorEstado")
                , ((TB_ESTADO)EstadoBindingSource.Current).COD_ESTADO)), new List<TB_CIDADE>());

            try
            {
                var objCid = CidadeBindingSource.List.OfType<TB_CIDADE>().First(c => c.COD_CIDADE == ((TB_CADASTRO)CadastroBindingSource.Current).COD_CIDADE);
                pos = CidadeBindingSource.IndexOf(objCid);
                CidadeBindingSource.Position = pos;
                objCid = null;
                pos = 0;
            }
            catch (Exception e)
            {
                CidadeBindingSource.Position = 0;
            }
            cbsCidade.BindingSource = CidadeBindingSource;
            cbsCidade.DisplayMember = "DESC_CIDADE";
        }

        #endregion

        public FCadastroCad(string layout) : base(layout)
        {
            InitializeComponent();
            this.Personaliza();
            this.lbNome.Text = "FORMULÁRIO DE CADASTRO";
        }

        private void EstadoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            BuscaCidades(((TB_ESTADO)EstadoBindingSource.Current).COD_ESTADO);
        }

        public void Inicializa()
        {
            if (this.LayoutTela.Equals("VISUALIZAR"))
            {
                BuscaDados();
            }
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {

        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(((TB_TIPO_CADASTRO)cbsTipoCadastro.Current).DESC_TIPO_CADASTRO);
        }       

        private void cbsFisicaJuridica_SelectedValueChanged(object sender, EventArgs e)
        {            
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

        private void cbsFisicaJuridica_ComboBoxDropDownClosed(object sender, EventArgs e)
        {
            MessageBox.Show(cbsFisicaJuridica.SelectedValue + " " + cbsFisicaJuridica.SelectedText);
        }

        private void FCadastroCad_Load(object sender, EventArgs e)
        {
            Inicializa();
        }        

        private void btAlterar_Click(object sender, EventArgs e)
        {   
            FPropriedadeCad f = new FPropriedadeCad("CADASTRAR");            
            f.ChaveConsulta.Add("Codigo", ObterValoresTipoAnonimo(PropriedadeBindingSource.Current)[0]);
            f.Show();
        }
    }   

}