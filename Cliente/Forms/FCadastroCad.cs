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

using System.Net.Http;
using Newtonsoft.Json;
using Cliente.POCO;

namespace Cliente.Forms
{
    public partial class FCadastroCad : FModeloCad
    {

        public TB_CADASTRO Cadastro { get; set; }
        public List<TB_TIPO_CADASTRO> TipoCadastro { get; set; }
        public BindingSource TipoCadastroBindingSource { get; set; }

        public FCadastroCad(string layout) : base(layout)
        {
            InitializeComponent();
            this.Inicializa();
        }
        
        static async Task<string> RunAsync(int codCadastro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53233/API/Cadastro/GetCadastro/" + codCadastro);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
                else
                    return "ERRO";
            }
        }

        static async Task<string> RunAsyncGetAllTipoCadastro()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53233/API/TipoCadastro");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
                else
                    return "ERRO";
            }
        }

        private async void buscaDados()
        {
            // Busca os dados no servidor            
            this.Cadastro = JsonConvert.DeserializeAnonymousType((await RunAsync(Convert.ToInt32(this.ChaveConsulta["Codigo"]))), new TB_CADASTRO());
            this.TipoCadastro = JsonConvert.DeserializeAnonymousType((await RunAsyncGetAllTipoCadastro()), new List<TB_TIPO_CADASTRO>());

            atualizaTela();
        }

        private async void btSalvar_Click(object sender, EventArgs e)
        {
            buscaDados();
        }

        private void atualizaTela()
        {
            txbsCodigo.DataBindings.Add("Text", this.Cadastro, "COD_CADASTRO");
            //txbNome.DataBindings.Add("Text", this.Cadastro, "NOME");
            txbsNome.DataBindings.Add("Text", this.Cadastro, "NOME");
            txbsNomeFantasia.DataBindings.Add("Text", this.Cadastro, "DESC_FANTASIA");
            txbsTelefone.DataBindings.Add("Text", this.Cadastro, "DESC_TELEFONE");
            txbsCelular.DataBindings.Add("Text", this.Cadastro, "DESC_CELULAR");
            txbsEmailXML.DataBindings.Add("Text", this.Cadastro, "DESC_E_MAIL");
            txbsEmailContato.DataBindings.Add("Text", this.Cadastro, "DESC_E_MAIL1");


            TipoCadastroBindingSource = new BindingSource();
            TipoCadastroBindingSource.DataSource = TipoCadastro;
            var obj = TipoCadastroBindingSource.List.OfType<TB_TIPO_CADASTRO>().First(c => c.COD_TIPO_CADASTRO == Cadastro.TB_TIPO_CADASTRO.COD_TIPO_CADASTRO);
            var pos = TipoCadastroBindingSource.IndexOf(obj);
            TipoCadastroBindingSource.Position = pos;
            
            //this.cbTipoCadastro.DataSource = TipoCadastroBindingSource;
            //this.cbTipoCadastro.DisplayMember = "DESC_TIPO_CADASTRO";


            cbsTipoCadastro.BindingSource = TipoCadastroBindingSource;
            cbsTipoCadastro.DisplayMember = "DESC_TIPO_CADASTRO";

            obj = null;
            pos = 0;

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
            var obj2 = fisicaJuridicaBindingSource.List.OfType<FisicaJuridica>().First(p => p.IND_FISICA_JURIDICA == Cadastro.IND_FISICA_JURIDICA);
            pos = fisicaJuridicaBindingSource.IndexOf(obj2);
            fisicaJuridicaBindingSource.Position = pos;
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
            if (LayoutTela.Equals("VISUALIZAR"))
                buscaDados();
        }
    }   

}