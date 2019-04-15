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

using Cliente.POCO;
using Newtonsoft.Json;

namespace Cliente.Forms
{
    public partial class FPropriedadeCad : FModeloCad
    {
        #region Properties
        public BindingSource PropriedadeBindingSource { get; set; }
        public BindingSource EstadoBindingSource { get; set; }
        public BindingSource CidadeBindingSource { get; set; }
        public BindingSource TipoPropriedadeBindingSource { get; set; }
        #endregion

        #region Acesso assíncrono ao servidor
        private async void BuscaDados()
        {
            //Busca a propriedade no servidor
            if (PropriedadeBindingSource == null)
                PropriedadeBindingSource = new BindingSource();
            PropriedadeBindingSource.DataSource =
                JsonConvert.DeserializeAnonymousType(
                    (await RunAsyncGet(
                        ConfigurationManager.AppSettings["UriPropriedade"], string.Format("{0}/{1}", "Get", this.ChaveConsulta["Codigo"])
                    ))
                , new TB_PROPRIEDADE());

            tbsNomePropriedade.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_PROPRIEDADE");
            tbsEnderecoPropriedade.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_LOCALIDADE");
            tbsBairroPropriedade.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_BAIRRO");
            tbsArea.DataBindings.Add("Text", PropriedadeBindingSource.Current, "NUM_AREA");
            tbsCepPropriedade.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_CEP");
            tbsCri.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_CRI");
            tbsMatriculaPropriedade.DataBindings.Add("Text", PropriedadeBindingSource.Current, "NUM_MATRICULA");
            tbsValorPropriedade.DataBindings.Add("Text", PropriedadeBindingSource.Current, "VAL_PROPRIEDADE");

            // cbCidade
            if (CidadeBindingSource == null)
                CidadeBindingSource = new BindingSource();
            CidadeBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriCidade"])), new List<TB_CIDADE>());
            var objCid = CidadeBindingSource.List.OfType<TB_CIDADE>().First(c => c.COD_CIDADE == ((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_CIDADE);
            var pos = CidadeBindingSource.IndexOf(objCid);
            CidadeBindingSource.Position = pos;
            objCid = null;
            pos = 0;

            // cbsEstado
            if (EstadoBindingSource == null)
                EstadoBindingSource = new BindingSource();
            EstadoBindingSource.CurrentItemChanged += new EventHandler(EstadoBindingSource_CurrentChanged);
            EstadoBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriEstado"]
                )), new List<TB_ESTADO>());
            var objEst = EstadoBindingSource.List.OfType<TB_ESTADO>().First(c => c.COD_ESTADO == ((TB_CIDADE)CidadeBindingSource.Current).COD_ESTADO);
            pos = EstadoBindingSource.IndexOf(objEst);
            EstadoBindingSource.Position = pos;
            cbsEstado.BindingSource = EstadoBindingSource;
            cbsEstado.DisplayMember = "DESC_ESTADO";
            cbsEstado.ValueMember = "COD_ESTADO";
            objEst = null;
            pos = 0;

            // cbsTipoPropriedade
            if (TipoPropriedadeBindingSource == null)
                TipoPropriedadeBindingSource = new BindingSource();
            TipoPropriedadeBindingSource.DataSource =
                JsonConvert.DeserializeAnonymousType((
                    await RunAsyncGet(ConfigurationManager.AppSettings["UriTipoPropriedade"])), new List<TB_TIPO_PROPRIEDADE>());
            var objTP = TipoPropriedadeBindingSource.List.OfType<TB_TIPO_PROPRIEDADE>()
                .First(c => c.COD_TIPO_PROPRIEDADE == ((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_TIPO_PROPRIEDADE);
            pos = TipoPropriedadeBindingSource.IndexOf(objTP);
            TipoPropriedadeBindingSource.Position = pos;
            cbsTipoPropriedade.BindingSource = TipoPropriedadeBindingSource;
            cbsTipoPropriedade.DisplayMember = "DESC_TIPO_PROPRIEDADE";
            cbsTipoPropriedade.ValueMember = "COD_TIPO_PROPRIEDADE";
            objTP = null;
            pos = 0;

            // cbsPropriedadePropria
            List<String> ListaPropria = new List<string>() {"SIM", "NAO"};            
            BindingSource TipoImovelBindingSource = new BindingSource();
            TipoImovelBindingSource.DataSource = ListaPropria;
            cbsPropriedadePropria.BindingSource = TipoImovelBindingSource;
            TipoImovelBindingSource.Position =
                TipoImovelBindingSource.IndexOf(((TB_PROPRIEDADE)PropriedadeBindingSource.Current).IND_TIPO_IMOVEL);                
        }

        private async void BuscaCidades(string estado)
        {
            //Carrega apenas cidades do estado        
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
                var objCid = CidadeBindingSource.List.OfType<TB_CIDADE>().First(c => c.COD_CIDADE == ((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_CIDADE);
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

        private void EstadoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            BuscaCidades(((TB_ESTADO)EstadoBindingSource.Current).COD_ESTADO);
        }

        public FPropriedadeCad(string layout) : base(layout)
        {
            InitializeComponent();
            this.Personaliza();
            this.lbNome.Text = "FORMULÁRIO DE PROPRIEDADE";
        }

        private void FPropriedadeCad_Load(object sender, EventArgs e)
        {
            if("VISUALIZAR ALTERAR".Contains(this.LayoutTela))            
                BuscaDados();
           
        }

        private async void btSalvar_Click(object sender, EventArgs e)
        {
            var retorno = await RunAsyncPost(
                string.Format("{0}/{1}/"
                    , ConfigurationManager.AppSettings["UriPropriedade"]
                    , "Save")
                    , (TB_PROPRIEDADE)PropriedadeBindingSource.Current);
            MessageBox.Show(retorno);
        }
    }
}
