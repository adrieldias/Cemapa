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
using System.Reflection;
using System.Configuration;

namespace Cliente.Forms
{
    public partial class FCadastroHome : FModeloHome
    {
        public BindingSource CadastroBindingSource { get; set; }

        public FCadastroHome()
        {
            InitializeComponent();
            this.Inicializa();
            if (CadastroBindingSource == null)
                CadastroBindingSource = new BindingSource();
            CadastroBindingSource.CurrentChanged += new EventHandler(CadastroBindingSource_CurrentChanged);
            busca1.BindingSource = CadastroBindingSource;
        }

        private void CadastroBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private async void FCadastroHome_Load(object sender, EventArgs e)
        {
            var definition = new
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

            busca1.definition = definition;
            busca1.URI = ConfigurationManager.AppSettings["UriCadastro"];
            //Busca os dados no servidor
            //var anonymousType = JsonConvert.DeserializeAnonymousType(
            //   (await RunAsyncGet("http://localhost:53233/API/Cadastro/GetPersonalizado")), definition);
            //BindingSource.DataSource = anonymousType.Data;
            //dataGridView1.DataSource = BindingSource.DataSource;
            busca1.ListarTodos();
            busca1.Focus();
        }



        private void btNovo_Click(object sender, EventArgs e)
        {

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
    }
}