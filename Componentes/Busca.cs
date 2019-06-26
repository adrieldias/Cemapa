using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace Componentes
{
    public partial class Busca : UserControl
    {
        [Description("BindingSorce"), Category("Cemapa")]
        public BindingSource BindingSource { get; set; }
        [Description("URI"), Category("Cemapa")]
        public string URI { get; set; }
        [Description("definition"), Category("Cemapa")]
        public dynamic definition { get; set; }
        public Busca()
        {
            InitializeComponent();
        }
        public async void Buscar(string texto)
        {
            var anonymousType = JsonConvert.DeserializeAnonymousType(
                (await RunAsyncGet(URI, texto)), definition);
            BindingSource.DataSource = anonymousType.Data;
        }
        public async void ListarTodos()
        {
            var anonymousType = JsonConvert.DeserializeAnonymousType(
                (await RunAsyncGet(URI)), definition);
            BindingSource.DataSource = anonymousType.Data;
        }
        public static async Task<string> RunAsyncGet(string uri, string valor)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync(string.Format("{0}/{1}", uri, valor));

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

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            if (txtBusca.Text != "DIGITE PARA PROCURAR")
                Buscar(txtBusca.Text);
        }

        private void txtBusca_Enter(object sender, EventArgs e)
        {
            txtBusca.SelectAll();
        }

        private void txtBusca_Leave(object sender, EventArgs e)
        {
            if (txtBusca.Text.Trim() == "")
                txtBusca.Text = "DIGITE PARA PROCURAR";
        }
    }
}
