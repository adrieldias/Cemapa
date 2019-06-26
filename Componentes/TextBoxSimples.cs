using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.UI;

namespace Componentes
{
    public partial class TextBoxSimples : System.Windows.Forms.UserControl
    {

        #region Propriedades
        [Description("Valor do TextBoxSimples"), Category("Cemapa")]
        public override string Text
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        [Description("Label do TextBoxSimples"), Category("Cemapa")]
        public string Label
        {
            get => this.lbNome.Text;
            set => this.lbNome.Text = value.ToUpper();
        }

        [Description("BindingSource"), Category("Cemapa")]
        public new ControlBindingsCollection DataBindings => textBox1.DataBindings;

        [Description("Password Character"), Category("Cemapa")]
        public char PasswordChar
        {
            get => this.textBox1.PasswordChar;
            set => this.textBox1.PasswordChar = value;
        }

        #endregion

        public TextBoxSimples()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.SteelBlue, ButtonBorderStyle.Solid);

            //var borderColor = Color.SteelBlue;
            //var borderStyle = ButtonBorderStyle.Solid;
            //var borderWidth = 0;

            //ControlPaint.DrawBorder(
            //                    e.Graphics,
            //                    this.panel1.ClientRectangle,
            //                    borderColor,
            //                    borderWidth,
            //                    borderStyle,
            //                    borderColor,
            //                    borderWidth,
            //                    borderStyle,
            //                    borderColor,
            //                    borderWidth,
            //                    borderStyle,
            //                    borderColor,
            //                    borderWidth,
            //                    borderStyle);
        }

        private void TextBoxSimples_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void TextBoxSimples_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
