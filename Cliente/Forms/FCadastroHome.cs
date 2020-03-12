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

using Cliente.Forms.Modelo;
using Cliente.UtilsCliente;
using System.Reflection;
using System.Configuration;
using System.Runtime.InteropServices;

namespace Cliente.Forms
{
    public partial class FCadastroHome : FModeloHome
    {
        public BindingSource CadastroBindingSource { get; set; }
        dynamic definition = new
        {
            Data = new[] {
                    new {
                        CODIGO = 0,
                        TIPO = string.Empty,
                        NOME = string.Empty,
                        TELEFONE = string.Empty,
                        CELULAR = string.Empty,
                        CNPJ_CPF = string.Empty,
                        ENDERECO = string.Empty,
                        //CodCidade = string.Empty,
                        CIDADE = string.Empty,
                        BAIRRO = string.Empty,
                        INSCRICAO = string.Empty,
                        FANTASIA = string.Empty,
                        CLASSIFICACAO = string.Empty
                    }
                }
        };
        public FCadastroHome()
        {
            InitializeComponent();
            this.Inicializa();
            if (CadastroBindingSource == null)
                CadastroBindingSource = new BindingSource();
            CadastroBindingSource.CurrentChanged += new EventHandler(CadastroBindingSource_CurrentChanged);
            dataGridView1.DataSource = CadastroBindingSource;            
        }

        private void CadastroBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private async void FCadastroHome_Load(object sender, EventArgs e)
        {
            // Busca os dados no servidor
            //var anonymousType = JsonConvert.DeserializeAnonymousType(
            //    (await RunAsyncGet(
            //        string.Format("{0}{1}", ConfigurationManager.AppSettings["UriCadastro"], "GetPersonalizado")
            //        )), definition);
            var uri = ConfigurationManager.AppSettings["Endereco"]
                + ":" + ConfigurationManager.AppSettings["Porta"]
                + "/" + ConfigurationManager.AppSettings["UriCadastro"]
                + "GetPersonalizado";
            CadastroBindingSource.DataSource = (await busca1.ListarTodos(uri, definition));
            //busca1.Focus();
        }



        private void btNovo_Click(object sender, EventArgs e)
        {
            FCadastroCad f = new FCadastroCad("INSERIR");
            f.Show();
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            FCadastroCad f = new FCadastroCad("ALTERAR");
            f.ChaveConsulta.Add("Codigo", dataGridView1.CurrentRow.Cells["CODIGO"].Value.ToString());
            f.Show();
        }

        private void btVisualizar_Click(object sender, EventArgs e)
        {
            FCadastroCad f = new FCadastroCad("VISUALIZAR");            
            f.ChaveConsulta.Add("Codigo", dataGridView1.CurrentRow.Cells["CODIGO"].Value.ToString());
            f.Show();
        }

        private async void btExcluir_Click(object sender, EventArgs e)
        {
            RetornoJson modelo = new RetornoJson();
            if (FDialogBox.Message(FDialogBox.Questionamento, "Exclusão", "Deseja realmente excluir este cadastro?") == DialogResult.OK)
            {
                var r =
                    await RunAsyncPost(
                    string.Format("{0}{1}", ConfigurationManager.AppSettings["UriCadastro"], "Delete"),
                    ((int)dataGridView1.CurrentRow.Cells["CODIGO"].Value)
                    );
                var retorno = JsonConvert.DeserializeAnonymousType(r, modelo);
                if (retorno.Data == "Deletado")
                {
                    FDialogBox.Message(FDialogBox.Informacao, "Mensagem", "Cadastro excluído");
                    FCadastroHome_Load(sender, e);
                }
                else
                    FDialogBox.Message(FDialogBox.Erro, "Erro", retorno.Data, FDialogBox.TamGrande);
                
            }
        }        

        private void busca1_TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)            
                busca1_ButtonClick(this, e);
            
        }

        private async void busca1_ButtonClick(object sender, EventArgs e)
        {
            if (busca1.parametroInformado())
            {
                Cliente.POCO.TB_CADASTRO c = new POCO.TB_CADASTRO();
                c.NOME = busca1.Text;
                CadastroBindingSource.DataSource = (await busca1.Buscar(string.Format("{0}{1}", ConfigurationManager.AppSettings["UriCadastro"],
                    "GetPersonalizado"), definition, c));
            }
            else
            {
                CadastroBindingSource.DataSource = (await busca1.ListarTodos(string.Format("{0}{1}", ConfigurationManager.AppSettings["UriCadastro"],
                    "GetPersonalizado"), definition));
            }
        }
    }
}