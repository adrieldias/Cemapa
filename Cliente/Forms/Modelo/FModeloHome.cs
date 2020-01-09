using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Configuration;

namespace Cliente.Forms.Modelo
{
    public partial class FModeloHome : Form
    {
        public const int cGrip = 16;      // Grip size
        public const int cCaption = 32;   // Caption bar height;

        public bool mover;
        public int cX, cY;

        public virtual object Dados { get; set; }
        private Hashtable LinhaSelecionada = new Hashtable();

        public static string Endereco { get; set; }
        public static string Porta { get; set; }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottonRect,
            int nWidthEllipse,
            int nHeightEllipse
            );


        #region Acesso assíncrono ao servidor

        public static async Task<string> RunAsyncGet(string uri, int codigo)
        {
            using (var client = new HttpClient())
            {   
                client.BaseAddress = new Uri(string.Format("{0}:{1}/{2}", Endereco, Porta, uri) + codigo);
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

        public static async Task<string> RunAsyncGet(string uri, string valor)
        {
            using (var client = new HttpClient())
            {   
                client.BaseAddress = new Uri(string.Format("{0}:{1}/{2}", Endereco, Porta, uri));
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
                client.BaseAddress = new Uri(string.Format("{0}:{1}/{2}", Endereco, Porta, uri));
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

        public static async Task<string> RunAsyncPost(string uri, int codigo)
        {
            using (var client = new HttpClient())
            {   
                client.BaseAddress = new Uri(string.Format("{0}:{1}/{2}", Endereco, Porta, uri));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(uri + "/" + codigo.ToString(),
                    new StringContent(
                        new JavaScriptSerializer().Serialize(""), Encoding.UTF8, "application/json"
                    )
                    );

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
        #endregion


        public FModeloHome()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            Endereco = ConfigurationManager.AppSettings["Endereco"];
            Porta = ConfigurationManager.AppSettings["Porta"];
        }

        public void btMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public void btCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void pCabecalho_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                cX = e.X;
                cY = e.Y;
                mover = true;
            }
        }

        public void pCabecalho_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mover = false;
        }

        public void pCabecalho_MouseMove(object sender, MouseEventArgs e)
        {
            if (mover)
            {
                this.Left += e.X - (cX - pCabecalho.Left);
                this.Top += e.Y - (cY - pCabecalho.Top);
            }
        }

        public void FModeloHome_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }

        protected T Cast<T>(object obj, T tipo)
        {
            return (T)obj;
        }

        private void AddEventGridView()
        {
            foreach(Control c in this.Controls)
            {
                if(c.GetType().Name.Equals("DataGridView"))
                {
                    ((DataGridView)c).RowEnter += FModeloHome_RowEnter;
                }
            }
        }

        private void FModeloHome_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LinhaSelecionada.Clear();
            if ((((DataGridView)sender).Rows != null) && (((DataGridView)sender).Rows.Count > 0))
                if (((DataGridView)sender).SelectedRows.Count > 0)
                {
                    foreach (DataGridViewColumn c in ((DataGridView)sender).Columns)
                    {
                        if (((DataGridView)sender).SelectedRows[0].Cells[c.Name].Value != null)
                            LinhaSelecionada.Add(c.Name.ToLower(), ((DataGridView)sender).SelectedRows[0].Cells[c.Name].Value);                            
                    }
                }
        }

        public string ValorSelecionado(string coluna)
        {
            if (LinhaSelecionada.ContainsKey(coluna.ToLower()))
                return LinhaSelecionada[coluna.ToLower()].ToString();
            else
                return string.Empty;
        }

        private void FModeloHome_Resize(object sender, EventArgs e)
        {
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(
                0,
                0,
                Width,
                Height,
                16,
                16
                ));
        }

        public void Inicializa()
        {
            PersonalizaForm.Instancia.Personaliza(this, "UNICO");
            AddEventGridView();
        }

        private void FModeloHome_Activated(object sender, EventArgs e)
        {
            pCabecalho.BackColor = Color.SteelBlue;
            btMinimize.FlatAppearance.BorderColor = Color.SteelBlue;
            btCloseForm.FlatAppearance.BorderColor = Color.SteelBlue;
        }

        private void FModeloHome_Deactivate(object sender, EventArgs e)
        {
            pCabecalho.BackColor = Color.LightSlateGray;
            btMinimize.FlatAppearance.BorderColor = Color.LightSlateGray;
            btCloseForm.FlatAppearance.BorderColor = Color.LightSlateGray;
        }

        public string[] ObterValoresTipoAnonimo(Object objeto)
        {
            if (objeto == null)
                return new string[] { };
            var lista = new List<string>();
            var p = objeto.GetType().GetProperties();

            foreach (PropertyInfo pop in p)
            {
                var valor = pop.GetValue(objeto, null);
                if (valor != null)
                    lista.Add(valor.ToString());
            }

            return lista.ToArray();
        }
    }
}
