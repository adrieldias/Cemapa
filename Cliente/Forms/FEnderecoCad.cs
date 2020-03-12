using Cliente.Forms.Modelo;
using Cliente.POCO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente.Forms
{
    public partial class FEnderecoCad : FModeloCad
    {
        public FEnderecoCad()
        {
            InitializeComponent();
        }

        public FEnderecoCad(string layout) : base(layout)
        {
            InitializeComponent();
            this.Personaliza();
        }

        #region Properties

        public BindingSource EnderecoBindingSource { get; set; }
        public BindingSource EstadoBindingSource { get; set; }
        public BindingSource CidadeBindingSource { get; set; }
        public BindingSource PaisBindingSource { get; set; }
        public BindingSource TipoEnderecoBindingSource { get; set; }

        #endregion

        #region DataBindings

        private void ConfiguraComponentes()
        {
            tbsEndereco.DataBindings.Add("Text", EnderecoBindingSource.Current, "DESC_ENDERECO");
            tbsComplemento.DataBindings.Add("Text", EnderecoBindingSource.Current, "DESC_COMPLEMENTO");
            tbsBairro.DataBindings.Add("Text", EnderecoBindingSource.Current, "DESC_BAIRRO");
            tbsCep.DataBindings.Add("Text", EnderecoBindingSource.Current, "DESC_CEP");
            tbsCaixaPostal.DataBindings.Add("Text", EnderecoBindingSource.Current, "NUM_CAIXA_POSTAL");
            tbsDistrito.DataBindings.Add("Text", EnderecoBindingSource.Current, "DESC_DISTRITO");

            //CbsEstado
            if (EstadoBindingSource == null)
                EstadoBindingSource = new BindingSource();
            cbsEstado.BindingSource = EstadoBindingSource;

            //CbsCidade
            if (CidadeBindingSource == null)
                CidadeBindingSource = new BindingSource();
            cbsCidade.BindingSource = CidadeBindingSource;

            //CbsPais
            if (PaisBindingSource == null)
                PaisBindingSource = new BindingSource();
            cbsPais.BindingSource = PaisBindingSource;

            //CbsTipoEndereco
            if (TipoEnderecoBindingSource == null)
                TipoEnderecoBindingSource = new BindingSource();
            cbsTipoEndereco.BindingSource = TipoEnderecoBindingSource;
        }

        #endregion

        #region Acesso Assíncrono ao Servidor

        private async void BuscaDados()
        {
            TB_CIDADE cidade = null;
            if(((TB_CADASTRO_ENDERECOS)EnderecoBindingSource.Current).COD_CIDADE != null)
            {
                cidade =
                    JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                    string.Format("{0}{1}/{2}", ConfigurationManager.AppSettings["UriCidade"], "Get", ((TB_CADASTRO_ENDERECOS)EnderecoBindingSource.Current).COD_CIDADE)
                    )), new TB_CIDADE());
            }
            
            EstadoBindingSource.DataSource = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriEstado"]
                )), new List<TB_ESTADO>());
            cbsEstado.DisplayMember = "DESC_ESTADO";
            cbsEstado.ValueMember = "COD_ESTADO";
            if ((EnderecoBindingSource.Current as TB_CADASTRO_ENDERECOS).COD_ESTADO != null)
            {
                var objEst = EstadoBindingSource.List.OfType<TB_ESTADO>().First(c => c.COD_ESTADO == (EnderecoBindingSource.Current as TB_CADASTRO_ENDERECOS).COD_ESTADO);
                var pos = EstadoBindingSource.IndexOf(objEst);
                EstadoBindingSource.Position = pos;
                objEst = null;
                pos = 0;
            }
            else
                cbsEstado.SelectedIndex = -1;            
            EstadoBindingSource.CurrentItemChanged += new EventHandler(EstadoBindingSource_CurrentChanged);
            var estado = (cidade != null ? cidade.COD_ESTADO : (EnderecoBindingSource.Current as TB_CADASTRO_ENDERECOS).COD_ESTADO);
            BuscaCidades(estado);
            PaisBindingSource.DataSource = JsonConvert.DeserializeAnonymousType(
                (await RunAsyncGet(
                    ConfigurationManager.AppSettings["UriPais"]
                    )), new List<TB_PAIS>()
                );
            cbsPais.DisplayMember = "DESC_PAIS";
            cbsPais.ValueMember = "COD_PAIS";

            //cbsTipoEndereco
            List<Generico> ListaTipo = new List<Generico>();            
            ListaTipo.Add(new Generico() { Codigo = "PRINCIPAL", Descricao = "PRINCIPAL" });
            ListaTipo.Add(new Generico() { Codigo = "ENTREGA", Descricao = "ENTREGA" });
            ListaTipo.Add(new Generico() { Codigo = "TRABALHO", Descricao = "TRABALHO" });            
            TipoEnderecoBindingSource.DataSource = ListaTipo;            
            cbsTipoEndereco.BindingSource = TipoEnderecoBindingSource;
            cbsTipoEndereco.DisplayMember = "Descricao";
            if ((EnderecoBindingSource.Current as TB_CADASTRO_ENDERECOS).IND_TIPO_ENDERECO != null)
            {
                var obj = TipoEnderecoBindingSource.List.OfType<POCO.Generico>().First(p => p.Descricao == ((TB_CADASTRO_ENDERECOS)EnderecoBindingSource.Current).IND_TIPO_ENDERECO);
                var pos = TipoEnderecoBindingSource.IndexOf(obj);
                TipoEnderecoBindingSource.Position = pos;
                obj = null;
                pos = 0;
            }            
        }

        private async void BuscaCidades(string estado)
        {
            if (estado == null || estado == string.Empty)
                return;

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
                if (((TB_CADASTRO_ENDERECOS)EnderecoBindingSource.Current).COD_CIDADE != null)
                {
                    var objCid = CidadeBindingSource.List.OfType<TB_CIDADE>().First(c => c.COD_CIDADE == ((TB_CADASTRO_ENDERECOS)EnderecoBindingSource.Current).COD_CIDADE);
                    pos = CidadeBindingSource.IndexOf(objCid);
                    CidadeBindingSource.Position = pos;
                    objCid = null;
                    pos = 0;
                }
                else
                    cbsCidade.SelectedIndex = -1;
            }
            catch (Exception e)
            {
                cbsCidade.SelectedIndex = -1;
            }
        }


        #endregion

        private void EstadoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            BuscaCidades(((TB_ESTADO)EstadoBindingSource.Current).COD_ESTADO);
        }

        private void FEnderecoCad_Load(object sender, EventArgs e)
        {
            ConfiguraComponentes();
            if ("ALTERAR".Contains(this.LayoutTela))
            {
                BuscaDados();
            }
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            EnderecoBindingSource.CancelEdit();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            ((TB_CADASTRO_ENDERECOS)EnderecoBindingSource.Current).COD_CIDADE
                = ((TB_CIDADE)CidadeBindingSource.Current).COD_CIDADE;
            ((TB_CADASTRO_ENDERECOS)EnderecoBindingSource.Current).COD_ESTADO
                = ((TB_ESTADO)EstadoBindingSource.Current).COD_ESTADO;
            ((TB_PAIS)PaisBindingSource.Current).COD_PAIS
                = ((TB_PAIS)PaisBindingSource.Current).COD_PAIS;
            (EnderecoBindingSource.Current as TB_CADASTRO_ENDERECOS).IND_TIPO_ENDERECO
                = (TipoEnderecoBindingSource.Current as Generico).Codigo;
            EnderecoBindingSource.EndEdit();
        }
    }
}
