using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente.Forms
{
    public partial class FDialogBox : Form
    {
        public static string Informacao = "informacao";
        public static string Erro = "erro";
        public static string Questionamento = "questao";
        public static string Alerta = "alerta";

        public const int cGrip = 16;      // Grip size
        public const int cCaption = 32;   // Caption bar height;
        public static Size TamPequeno = new Size(450, 250);
        public static Size TamMedio = new Size(600, 400);
        public static Size TamGrande = new Size(800, 600);

        public bool mover;
        public int cX, cY;        

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottonRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

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

        private void FDialogBox_Resize(object sender, EventArgs e)
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

        private void pCabecalho_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                cX = e.X;
                cY = e.Y;
                mover = true;
            }
        }

        private void pCabecalho_MouseMove(object sender, MouseEventArgs e)
        {
            if (mover)
            {
                this.Left += e.X - (cX - pCabecalho.Left);
                this.Top += e.Y - (cY - pCabecalho.Top);
            }
        }

        private void pCabecalho_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mover = false;
        }

        public FDialogBox()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            PersonalizaForm.Instancia.Personaliza(this, "UNICO");
        }

        private void btOk_Click(object sender, EventArgs e)
        {

        }

        private void btCancelar_Click(object sender, EventArgs e)
        {

        }

        private void layout(string layout)
        {
            
            if (layout == Questionamento)
            {
                btCancelar.Visible = true;
                btOk.Visible = true;
            }
            else
            {
                btCancelar.Visible = false;
                btOk.Visible = true;
            }
            
        }


        public static DialogResult Message(string layout, string caption, string message)
        {
            var f = new FDialogBox();
            f.lbCaption.Text = caption;
            f.lbMensagem.Text = message;
            f.Size = TamPequeno;
            f.layout(layout);
            f.ShowDialog();
            return f.DialogResult;
        }

        public static DialogResult Message(string layout, string caption, string message, Size tamanho)
        {
            var f = new FDialogBox();
            f.lbCaption.Text = caption;
            f.lbMensagem.Text = message;
            f.Size = tamanho;
            f.layout(layout);
            f.ShowDialog();
            return f.DialogResult;
        }


    }
}
