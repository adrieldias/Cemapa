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
            arrumaLayout();
        }

        private void arrumaLayout()
        {
            if (LayoutTela.Equals("VISUALIZAR"))
            {
                btFechar.Left = btSalvar.Left;                
                btSalvar.Visible = false;
                btCancelar.Visible = false;
            }
        }

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

            this.Refresh();
            Application.DoEvents();
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Inicializa()
        {
            PersonalizaForm.Instancia.Personaliza(this);            
        }
    }
}
