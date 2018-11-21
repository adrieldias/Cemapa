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

namespace Cliente.Forms
{
    public partial class FCadastroHome : FModeloHome
    {
        public FCadastroHome()
        {
            InitializeComponent();
            PersonalizaForm.Instancia.Personaliza(this);
        }

        private async void btPesquisar_Click(object sender, EventArgs e)
        {
            var definition = new {
                Data = new[] {
                    new {
                        Codigo = 0,
                        Tipo = string.Empty,
                        Nome = string.Empty,
                        Telefone = string.Empty,
                        Celular = string.Empty,
                        CGC_CPF = string.Empty,
                        Endereco = string.Empty,
                        CodCidade = string.Empty,
                        Cidade = string.Empty,
                        Bairro = string.Empty,
                        Inscricao = string.Empty,
                        Fantasia = string.Empty,
                        Classificacao = string.Empty
                    }
                }
            };      
            
            // Busca os dados no servidor
            var anonymousType = JsonConvert.DeserializeAnonymousType((await RunAsync()), definition);
            dataGridView1.DataSource = anonymousType.Data;           
            
        }

        static async Task<string> RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53233/API/Cadastro");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("");
                //HttpResponseMessage response = client.GetAsync("").Result;
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
                else
                    return "ERRO";
            }
        }

        private void FCadastroHome_Load(object sender, EventArgs e)
        {

        }
    }
}
