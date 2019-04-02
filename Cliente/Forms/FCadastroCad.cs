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

using System.Net.Http;
using Newtonsoft.Json;
using Cliente.POCO;

namespace Cliente.Forms
{
    public partial class FCadastroCad : FModeloCad
    {
        #region Properties

        public TB_CADASTRO Cadastro { get; set; }
        public List<TB_TIPO_CADASTRO> TipoCadastro { get; set; }        
        public List<TB_CLASS_CADASTRO> ClassCadastro { get; set; }
        public List<TB_VENDEDOR> Vendedor { get; set; }
        public List<TB_ESTADO> Estado { get; set; }
        public List<TB_CIDADE> Cidade { get; set; }
        public List<TB_PAIS> Pais { get; set; }
        public BindingSource TipoCadastroBindingSource { get; set; }
        public BindingSource ClassCadastroBindingSource { get; set; }
        public BindingSource VendedorBindingSource { get; set; }
        public BindingSource EstadoBindingSource { get; set; }
        public BindingSource CidadeBindingSource { get; set; }
        public BindingSource PaisBindingSource { get; set; }

        #endregion

        #region Acesso assíncrono ao servidor

        public static async Task<string> RunAsyncGet(string uri, int codigo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri + codigo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var resposta = response.Content.ReadAsStringAsync();
                    client.Dispose();
                    response.Dispose();
                    return await resposta;
                }
                else
                {
                    client.Dispose();
                    response.Dispose();
                    return "ERRO";
                }
            }
        }

        public static async Task<string> RunAsyncGet(string uri, string codigo)
        {            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);                
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync(string.Format("{0}/{1}", uri, codigo));
                
                if (response.IsSuccessStatusCode)
                {
                    var resposta = response.Content.ReadAsStringAsync();
                    client.Dispose();
                    response.Dispose();
                    return await resposta;
                }
                else
                {
                    client.Dispose();
                    response.Dispose();
                    return "ERRO";
                }
            }
        }

        public static async Task<string> RunAsyncGet(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var resposta = response.Content.ReadAsStringAsync();
                    response.Dispose();
                    client.Dispose();
                    return await resposta;
                }
                else
                {
                    response.Dispose();
                    client.Dispose();
                    return "ERRO";
                }
            }
        }
        

        /// <summary>
        /// Busca os dados do cadastro no servidor
        /// </summary>
        private async void buscaDadosPrincipal()
        {            
            this.Cadastro = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriCadastro"], 
                Convert.ToInt32(this.ChaveConsulta["Codigo"]))), new TB_CADASTRO());
            buscaDadosComplementares();            
        }

        /// <summary>
        /// Busca os dados complementares no servidor
        /// </summary>
        private async void buscaDadosComplementares()
        {
            this.TipoCadastro = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(                 
                ConfigurationManager.AppSettings["UriTipoCadastro"]
                )), new List<TB_TIPO_CADASTRO>());
            this.ClassCadastro = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriClassCadastro"]
                )), new List<TB_CLASS_CADASTRO>());
            this.Vendedor = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriVendedor"]
                )), new List<TB_VENDEDOR>());
            this.Estado = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriEstado"]
                )), new List<TB_ESTADO>());
            this.Pais = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                ConfigurationManager.AppSettings["UriPais"]
                )), new List<TB_PAIS>());

            BuscaCidades(Cadastro.COD_ESTADO);

            AtualizaTela();
        }

        private async void BuscaCidades(string estado)
        {
            //Carrega apenas cidades do estado do cadastro
            if(EstadoBindingSource == null)
                EstadoBindingSource = new BindingSource();
            EstadoBindingSource.DataSource = Estado;
            var objEst = EstadoBindingSource.List.OfType<TB_ESTADO>().First(c => c.COD_ESTADO == estado);
            var pos = EstadoBindingSource.IndexOf(objEst);
            EstadoBindingSource.Position = pos;
            objEst = null;
            pos = 0;
            this.Cidade = JsonConvert.DeserializeAnonymousType((await RunAsyncGet(
                string.Format("{0}{1}", ConfigurationManager.AppSettings["UriCidade"], "GetCidadesPorEstado")
                , ((TB_ESTADO)EstadoBindingSource.Current).COD_ESTADO)), new List<TB_CIDADE>());
                AtualizaCbsCidade();
        }

        #endregion

        #region DataBindings

        private void AtualizaCbsCidade()
        {
            if (Cidade != null)
            {
                if(CidadeBindingSource == null)
                    CidadeBindingSource = new BindingSource();
                CidadeBindingSource.DataSource = Cidade;
                try
                {
                    var objCid = CidadeBindingSource.List.OfType<TB_CIDADE>().First(c => c.COD_CIDADE == Cadastro.COD_CIDADE);
                    var pos = CidadeBindingSource.IndexOf(objCid);
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
        }

        private void AtualizaTela()
        {
            txbsCodigo.DataBindings.Add("Text", this.Cadastro, "COD_CADASTRO");            
            txbsNome.DataBindings.Add("Text", this.Cadastro, "NOME");
            txbsNomeFantasia.DataBindings.Add("Text", this.Cadastro, "DESC_FANTASIA");
            txbsTelefone.DataBindings.Add("Text", this.Cadastro, "DESC_TELEFONE");
            txbsCelular.DataBindings.Add("Text", this.Cadastro, "DESC_CELULAR");
            txbsEmailXML.DataBindings.Add("Text", this.Cadastro, "DESC_E_MAIL");
            txbsEmailContato.DataBindings.Add("Text", this.Cadastro, "DESC_E_MAIL1");
            
            //cbsTipoCadastro
            if(TipoCadastroBindingSource == null)
                TipoCadastroBindingSource = new BindingSource();
            TipoCadastroBindingSource.DataSource = TipoCadastro;
            var obj = TipoCadastroBindingSource.List.OfType<TB_TIPO_CADASTRO>().First(c => c.COD_TIPO_CADASTRO == Cadastro.COD_TIPO_CADASTRO);
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
            var objFJ = fisicaJuridicaBindingSource.List.OfType<FisicaJuridica>().First(p => p.IND_FISICA_JURIDICA == Cadastro.IND_FISICA_JURIDICA);
            pos = fisicaJuridicaBindingSource.IndexOf(objFJ);
            fisicaJuridicaBindingSource.Position = pos;
            objFJ = null;
            pos = 0;

            //cbsClassCadastro            
            if(ClassCadastroBindingSource == null)
                ClassCadastroBindingSource = new BindingSource();
            ClassCadastroBindingSource.DataSource = ClassCadastro;
            var objCC = ClassCadastroBindingSource.List.OfType<TB_CLASS_CADASTRO>().First(c => c.COD_CLASS_CADASTRO == Cadastro.COD_CLASS_CADASTRO);
            pos = ClassCadastroBindingSource.IndexOf(objCC);
            ClassCadastroBindingSource.Position = pos;
            cbsClassificacao.BindingSource = ClassCadastroBindingSource;
            cbsClassificacao.DisplayMember = "DESC_CLASSIFICACAO";
            objCC = null;
            pos = 0;

            //cbsVendedor  
            if (!Cadastro.COD_VENDEDOR.Equals(null))
            {
                if(VendedorBindingSource == null)
                    VendedorBindingSource = new BindingSource();
                VendedorBindingSource.DataSource = Vendedor;
                var objV = VendedorBindingSource.List.OfType<TB_VENDEDOR>().First(c => c.COD_VENDEDOR == Cadastro.COD_VENDEDOR);
                pos = VendedorBindingSource.IndexOf(objV);
                VendedorBindingSource.Position = pos;
                cbsVendedor.BindingSource = VendedorBindingSource;
                cbsVendedor.DisplayMember = "NOME";
                objV = null;
                pos = 0;
            }

            //cbsEstado
            if(EstadoBindingSource == null)
                EstadoBindingSource = new BindingSource();
            EstadoBindingSource.DataSource = Estado;
            var objEst = EstadoBindingSource.List.OfType<TB_ESTADO>().First(c => c.COD_ESTADO == Cadastro.COD_ESTADO);
            pos = EstadoBindingSource.IndexOf(objEst);
            EstadoBindingSource.Position = pos;
            cbsEstado.BindingSource = EstadoBindingSource;
            cbsEstado.DisplayMember = "DESC_ESTADO";
            cbsEstado.ValueMember = "COD_ESTADO";
            objEst = null;
            pos = 0;

            AtualizaCbsCidade();

            //cbsPais
            if (PaisBindingSource == null)
                PaisBindingSource = new BindingSource();
            PaisBindingSource.DataSource = Pais;
            var objPais = PaisBindingSource.List.OfType<TB_PAIS>().First(p => p.COD_PAIS == Cadastro.COD_PAIS);
            pos = PaisBindingSource.IndexOf(objPais);
            PaisBindingSource.Position = pos;
            cbsPais.BindingSource = PaisBindingSource;
            cbsPais.DisplayMember = "DESC_PAIS";
            cbsPais.ValueMember = "COD_PAIS";
            objPais = null;
            pos = 0;
        }

        #endregion

        public FCadastroCad(string layout) : base(layout)
        {
            InitializeComponent();
            this.Personaliza();
            this.lbNome.Text = "FORMULÁRIO DE CADASTRO";
        }

        public void Inicializa()
        {
            if (this.LayoutTela.Equals("VISUALIZAR"))
            {
                buscaDadosPrincipal();
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

        private void cbsEstado_SelectedValueChanged(object sender, EventArgs e)
        {
            cbsEstado.ValueMember = "COD_ESTADO";
            cbsEstado.DisplayMember = "DESC_ESTADO";
            BuscaCidades(cbsEstado.SelectedValue);
        }
    }   

}