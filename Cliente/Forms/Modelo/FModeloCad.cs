using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Net.Http;

namespace Cliente.Forms.Modelo
{
    public partial class FModeloCad : Form
    {
        public const int cGrip = 16;      // Grip size
        public const int cCaption = 32;   // Caption bar height;

        public bool mover;
        public int cX, cY;

        public Hashtable ChaveConsulta = new Hashtable();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottonRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        /// <summary>
        /// Muda layout da tela conforme string passada por parâmetro
        /// INSERIR, ALTERAR, VISUALIZAR
        /// </summary>
        public string LayoutTela { get; set; }

        protected FModeloCad() { InitializeComponent(); }

        public FModeloCad(string layout)
        {            
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.LayoutTela = layout;            
        }

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

        #endregion

        private void btMinimize_Click(object sender, EventArgs e)
        {            
            this.WindowState = FormWindowState.Minimized;            
        }

        private void btCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pCabecalho_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                cX = e.X;
                cY = e.Y;
                mover = true;
            }
        }

        private void pCabecalho_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mover = false;
        }

        private void pCabecalho_MouseMove(object sender, MouseEventArgs e)
        {
            if (mover)
            {
                this.Left += e.X - (cX - pCabecalho.Left);
                this.Top += e.Y - (cY - pCabecalho.Top);
            }
        }

        private void FModeloCad_Paint(object sender, PaintEventArgs e)
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

        private void FModeloCad_Resize(object sender, EventArgs e)
        {
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(
                0,
                0,
                Width,
                Height,
                16,
                16
                ));

            //this.Refresh();
            Application.DoEvents();
        }

        public void Personaliza()
        {   
            PersonalizaForm.Instancia.Personaliza(this, LayoutTela);            
        }

        private void FModeloCad_Activated(object sender, EventArgs e)
        {
            pCabecalho.BackColor = Color.SteelBlue;
            btMinimize.FlatAppearance.BorderColor = Color.SteelBlue;
            btCloseForm.FlatAppearance.BorderColor = Color.SteelBlue;
        }

        private void FModeloCad_Deactivate(object sender, EventArgs e)
        {
            pCabecalho.BackColor = Color.LightSlateGray;
            btMinimize.FlatAppearance.BorderColor = Color.LightSlateGray;
            btCloseForm.FlatAppearance.BorderColor = Color.LightSlateGray;
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            this.Close();
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

        public string ObterValoresTipoAnonimo(Object objeto, string propriedade)
        {
            var retorno = string.Empty;
            if (objeto == null)
                return retorno;            
            var p = objeto.GetType().GetProperties();
            foreach (PropertyInfo pop in p)
            {
                var valor = pop.GetValue(objeto, null);
                if (valor != null && pop.Name == propriedade)
                {
                    retorno = valor.ToString();
                    break;
                }
            }
            return retorno;
        }        

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}
