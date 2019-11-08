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
using System.Reflection;

namespace Cliente.Forms
{
    public partial class FPropriedadeCad : FModeloCad
    {
        #region Properties
        public BindingSource PropriedadeBindingSource { get; set; }
        public BindingSource EstadoBindingSource { get; set; }
        public BindingSource CidadeBindingSource { get; set; }
        public BindingSource TipoPropriedadeBindingSource { get; set; }
        public BindingSource TipoImovelBindingSource { get; set; }
        public BindingSource CadastroBindingSource { get; set; }
        dynamic definitionCadastro = new
        {
            Data = new[] {
                    new {
                        CODIGO = 0,                        
                        NOME = string.Empty                        
                    }
                }
        };

        #endregion

        #region DataBindings
        private void ConfiguraComponentes()
        {
            tbsNomePropriedade.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_PROPRIEDADE");
            tbsEnderecoPropriedade.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_LOCALIDADE");
            tbsBairroPropriedade.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_BAIRRO");
            tbsArea.DataBindings.Add("Text", PropriedadeBindingSource.Current, "NUM_AREA");
            
            tbsCepPropriedade.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_CEP");
            tbsCri.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_CRI");
            tbsMatriculaPropriedade.DataBindings.Add("Text", PropriedadeBindingSource.Current, "NUM_MATRICULA");
            tbsValorPropriedade.DataBindings.Add("Text", PropriedadeBindingSource.Current, "VAL_PROPRIEDADE");
            tbsLatitude.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_LATITUDE");
            tbsLongitude.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_LONGITUDE");
            tbsObservacao.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_OBSERVACAO");
            tbsVencContrato.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DT_VENC_CONTRATO");
            tbsDtVencHipoteca.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DT_VENC_HIPOTECA");
            tbsDtVencHipoteca2.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DT_VENC_HIPOTECA2");
            tbsDtVencHipoteca3.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DT_VENC_HIPOTECA3");
            tbsDtVencHipoteca4.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DT_VENC_HIPOTECA4");
            tbsDtVencHipoteca5.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DT_VENC_HIPOTECA5");
            tbsDtVencHipoteca6.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DT_VENC_HIPOTECA6");
            tbsDescFavorecidoHipoteca.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_FAVORECIDO_HIPOTECA");
            tbsDescFavorecidoHipoteca2.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_FAVORECIDO_HIPOTECA2");
            tbsDescFavorecidoHipoteca3.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_FAVORECIDO_HIPOTECA3");
            tbsDescFavorecidoHipoteca4.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_FAVORECIDO_HIPOTECA4");
            tbsDescFavorecidoHipoteca5.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_FAVORECIDO_HIPOTECA5");
            tbsDescFavorecidoHipoteca6.DataBindings.Add("Text", PropriedadeBindingSource.Current, "DESC_FAVORECIDO_HIPOTECA6");
            tbsValHipoteca.DataBindings.Add("Text", PropriedadeBindingSource.Current, "VAL_HIPOTECA");
            tbsValHipoteca2.DataBindings.Add("Text", PropriedadeBindingSource.Current, "VAL_HIPOTECA2");
            tbsValHipoteca3.DataBindings.Add("Text", PropriedadeBindingSource.Current, "VAL_HIPOTECA3");
            tbsValHipoteca4.DataBindings.Add("Text", PropriedadeBindingSource.Current, "VAL_HIPOTECA4");
            tbsValHipoteca5.DataBindings.Add("Text", PropriedadeBindingSource.Current, "VAL_HIPOTECA5");
            tbsValHipoteca6.DataBindings.Add("Text", PropriedadeBindingSource.Current, "VAL_HIPOTECA6");
            
            // cbsCidade
            if (CidadeBindingSource == null)
                CidadeBindingSource = new BindingSource();
            cbsCidade.BindingSource = CidadeBindingSource;            

            // cbsEstado
            if (EstadoBindingSource == null)
                EstadoBindingSource = new BindingSource();
            cbsEstado.BindingSource = EstadoBindingSource;            

            // cbsTipoPropriedade
            if (TipoPropriedadeBindingSource == null)
                TipoPropriedadeBindingSource = new BindingSource();
            cbsTipoPropriedade.BindingSource = TipoPropriedadeBindingSource;
            

            // cbsPropriedadePropria
            if(TipoImovelBindingSource == null)
                TipoImovelBindingSource = new BindingSource();
            cbsPropriedadePropria.BindingSource = TipoImovelBindingSource;

            //cbsCadastroProprietario
            if (CadastroBindingSource == null)
                CadastroBindingSource = new BindingSource();
            cbsCadastroProprietario.BindingSource = CadastroBindingSource;
        }
        #endregion

        #region Acesso assíncrono ao servidor
        private async void BuscaDados()
        {
            TB_CIDADE cidade = null;
            if (((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_CIDADE != null)
            {
                cidade =
                JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                    string.Format("{0}{1}/{2}", ConfigurationManager.AppSettings["UriCidade"], "Get", ((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_CIDADE)
                    )), new TB_CIDADE());
            }

            EstadoBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriEstado"]
                )), new List<TB_ESTADO>());
            cbsEstado.DisplayMember = "DESC_ESTADO";
            cbsEstado.ValueMember = "COD_ESTADO";
            if (cidade != null)
            {
                var objEst = EstadoBindingSource.List.OfType<TB_ESTADO>().First(c => c.COD_ESTADO == cidade.COD_ESTADO);
                var pos = EstadoBindingSource.IndexOf(objEst);
                EstadoBindingSource.Position = pos;
                objEst = null;
                pos = 0;
            }
            else
                cbsEstado.SelectedIndex = -1;
            EstadoBindingSource.CurrentItemChanged += new EventHandler(EstadoBindingSource_CurrentChanged);

            if (cidade != null)
                BuscaCidades(cidade.COD_ESTADO);            

            TipoPropriedadeBindingSource.DataSource =
                JsonConvert.DeserializeAnonymousType((
                    await RunAsyncGet(ConfigurationManager.AppSettings["UriTipoPropriedade"])), new List<TB_TIPO_PROPRIEDADE>());
            if (((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_TIPO_PROPRIEDADE != null)
            {
                var objTP = TipoPropriedadeBindingSource.List.OfType<TB_TIPO_PROPRIEDADE>()
                    .First(c => c.COD_TIPO_PROPRIEDADE == ((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_TIPO_PROPRIEDADE);
                var pos = TipoPropriedadeBindingSource.IndexOf(objTP);
                TipoPropriedadeBindingSource.Position = pos;
                objTP = null;
                pos = 0;
            }
            cbsTipoPropriedade.DisplayMember = "DESC_TIPO_PROPRIEDADE";
            cbsTipoPropriedade.ValueMember = "COD_TIPO_PROPRIEDADE";

            //CadastroBindingSource.DataSource =
            //    JsonConvert.DeserializeAnonymousType((
            //        await RunAsyncGet(string.Format("{0}/{1}", ConfigurationManager.AppSettings["UriCadastro"], "GetPersonalizado"))), definitionCadastro);
            //if (((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_CADASTRO_PROPRIETARIO != null)
            //{
            //    var obj = CadastroBindingSource.List.OfType<TB_CADASTRO>()
            //        .First(c => c.COD_CADASTRO == ((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_CADASTRO_PROPRIETARIO);
            //    var pos = CadastroBindingSource.IndexOf(obj);
            //    CadastroBindingSource.Position = pos;
            //    obj = null;
            //    pos = 0;
            //}
            //cbsCadastroProprietario.DisplayMember = "NOME";
            //cbsCadastroProprietario.ValueMember = "CODIGO";

            List<String> ListaPropria = new List<string>() {"SIM", "NAO"};            
            TipoImovelBindingSource.DataSource = ListaPropria;            
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
            CidadeBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                string.Format("{0}{1}", ConfigurationManager.AppSettings["UriCidade"], "GetCidadesPorEstado")
                , ((TB_ESTADO)EstadoBindingSource.Current).COD_ESTADO)), new List<TB_CIDADE>());
            cbsCidade.DisplayMember = "DESC_CIDADE";
            cbsCidade.ValueMember = "COD_CIDADE";
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
            ConfiguraComponentes();
            if ("ALTERAR".Contains(this.LayoutTela))
            {
                BuscaDados();
            }
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            // Busca as alterações feitas nos DataBindings            
            ((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_CIDADE =
                ((TB_CIDADE)CidadeBindingSource.Current).COD_CIDADE;
            if (TipoPropriedadeBindingSource.Count > 0)
                ((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_TIPO_PROPRIEDADE =
                    ((TB_TIPO_PROPRIEDADE)TipoPropriedadeBindingSource.Current).COD_TIPO_PROPRIEDADE;
            ((TB_PROPRIEDADE)PropriedadeBindingSource.Current).IND_TIPO_IMOVEL =
                TipoImovelBindingSource.Current.ToString();
            if (((TB_CADASTRO)CadastroBindingSource.Current).COD_CADASTRO > 0)
                ((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_CADASTRO_PROPRIETARIO =
                    ((TB_CADASTRO)CadastroBindingSource.Current).COD_CADASTRO;
            PropriedadeBindingSource.EndEdit();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {               
            PropriedadeBindingSource.CancelEdit();
        }

        private void tbsArea_Leave(object sender, EventArgs e)
        {

        }

        private async void cbsCadastroProprietario_ComboBoxKeyDown(object sender, KeyEventArgs e)
        {
            if(cbsCadastroProprietario.SelectedIndex == -1 && e.KeyCode == Keys.Enter)
            {
                var cad = new TB_CADASTRO();
                if (!string.IsNullOrEmpty(cbsCadastroProprietario.Text))
                {
                    cad.NOME = cbsCadastroProprietario.Text;                    
                    CadastroBindingSource.DataSource =
                            JsonConvert.DeserializeAnonymousType((await RunAsyncPost(
                    string.Format("{0}/{1}", ConfigurationManager.AppSettings["UriCadastro"], "Get"), cad)
                ), new List<TB_CADASTRO>());
                    if (((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_CADASTRO_PROPRIETARIO != null)
                    {
                        var obj = CadastroBindingSource.List.OfType<TB_CADASTRO>()
                            .First(c => c.COD_CADASTRO == ((TB_PROPRIEDADE)PropriedadeBindingSource.Current).COD_CADASTRO_PROPRIETARIO);
                        var pos = CadastroBindingSource.IndexOf(obj);
                        CadastroBindingSource.Position = pos;
                        obj = null;
                        pos = 0;
                    }
                    cbsCadastroProprietario.DisplayMember = "NOME";
                    cbsCadastroProprietario.ValueMember = "COD_CADASTRO";
                    cbsCadastroProprietario.DroppedDown = true;
                }
            }
        }
    }
}
