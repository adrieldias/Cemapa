﻿using System;
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
        #region Eventos
        public event EventHandler TextBoxTextChanged;
        public event KeyEventHandler TextBoxKeyDown;
        public event EventHandler ButtonClick;


        private void HandleTextBoxTextChanged(object sender, EventArgs e)
        {
            this.OnTextBoxTextChanged(EventArgs.Empty);
        }      

        private void HandleTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            this.OnTextBoxKeyDown(e);
        }

        private void HandleButtonClick(object sender, EventArgs e)
        {
            this.OnButtonClick(EventArgs.Empty);
        }
        
        protected virtual void OnTextBoxTextChanged(EventArgs e)
        {
            EventHandler handler = this.TextBoxTextChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnTextBoxKeyDown(KeyEventArgs e)
        {            
            KeyEventHandler handler = this.TextBoxKeyDown;
            if (handler != null)
            {                    
                handler(this, e);
            }            
        }

        protected virtual void OnButtonClick(EventArgs e)
        {
            EventHandler handler = this.ButtonClick;
            if(handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #region Propriedades
        [Description("Text"), Category("Cemapa")]
        public override string Text
        {
            get => txtBusca.Text;
            set => txtBusca.Text = value;
        }        
        #endregion
        public Busca()
        {
            InitializeComponent();
            txtBusca.TextChanged += this.HandleTextBoxTextChanged;
            txtBusca.KeyDown += this.HandleTextBoxKeyDown;
            btPesquisa.Click += this.HandleButtonClick;
        }
        public async Task<object> Buscar(string uri, dynamic definition, object param)
        {
            var anonymousType = JsonConvert.DeserializeAnonymousType(
                (await RunAsyncPost(uri, param)), definition);
            return anonymousType.Data;
        }
        public async Task<object> ListarTodos(string uri, dynamic definition)
        {
            var anonymousType = JsonConvert.DeserializeAnonymousType(
                (await RunAsyncGet(uri)), definition);
            return anonymousType.Data;
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
        public static async Task<string> RunAsyncPost(string Uri, Object valor)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.PostAsJsonAsync(new Uri(Uri), valor);
                return await response.Content.ReadAsStringAsync();
            }
        }
        private void txtBusca_Enter(object sender, EventArgs e)
        {
            if (txtBusca.Text == "DIGITE PARA PROCURAR")
                txtBusca.Clear();
            else
                txtBusca.SelectAll();                        
        }

        private void txtBusca_Leave(object sender, EventArgs e)
        {
            if (txtBusca.Text.Trim() == "")            
                txtBusca.Text = "DIGITE PARA PROCURAR";            
        }

        public bool parametroInformado()
        {
            return !((txtBusca.Text == "DIGITE PARA PROCURAR") || (string.IsNullOrEmpty(txtBusca.Text)));
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {

        }

        private void btPesquisa_Click(object sender, EventArgs e)
        {

        }
    }
}
