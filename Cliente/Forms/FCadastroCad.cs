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
        public FCadastroCad()
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


                //TB_CADASTRO c = new TB_CADASTRO();
                //c.DESC_BAIRRO = "CENTRO";
                //c.DESC_CELULAR = "45 99232323";
                //c.NUM_CGC_CPF = "00000000000";
                //c.COD_CLASS_CADASTRO = "1";
                //c.COD_CIDADE = 1;
                //c.NUM_INSCRICAO = null;
                //c.NOME = "JOAO";
                //c.DESC_TELEFONE = "32645555";
                //c.DESC_FANTASIA = "BATMAN";


                //var stringContent = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");
                //// HTTP POST
                //HttpResponseMessage response = await client.PostAsync("http://localhost:53233/API/Create", stringContent);
                
                //if (response.IsSuccessStatusCode)
                //    return await response.Content.ReadAsStringAsync();
                //else
                //    return "ERRO";
            }
        }

        private async void btSalvar_Click(object sender, EventArgs e)
        {
            var definition = new Cadastro();

            // Busca os dados no servidor            
            var anonymousType = JsonConvert.DeserializeAnonymousType((await RunAsync(Convert.ToInt32(this.ChaveConsulta["Codigo"]))), definition);
            this.Dados = anonymousType;
            atualizaTela();
        }

        private void atualizaTela()
        {
            txbCodigo.DataBindings.Add("Text", this.Dados, "COD_CADASTRO");
            txbNome.DataBindings.Add("Text", this.Dados, "NOME");
            txbFantasia.DataBindings.Add("Text", this.Dados, "DESC_FANTASIA");
            txbTelefone.DataBindings.Add("Text", this.Dados, "DESC_TELEFONE");
            txbCelular.DataBindings.Add("Text", this.Dados, "DESC_CELULAR");
            txbEmailXML.DataBindings.Add("Text", this.Dados, "DESC_E_MAIL");
            txbEmailContato.DataBindings.Add("Text", this.Dados, "DESC_E_MAIL1");
        }
    }
}
