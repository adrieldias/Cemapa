using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Componentes
{
    public partial class ComboBoxSimples : UserControl
    {
        #region Eventos
        
        public event EventHandler SelectedIndexChanged;

        private void HandleSelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnSelectedIndexChanged(EventArgs.Empty);
        }

        public virtual void OnSelectedIndexChanged(EventArgs e)
        {
            EventHandler handler = this.SelectedIndexChanged;
            if (handler != null)
            {                
                this.SelectedText = comboBox1.Text;
                handler(this, e);
            }
        }

        #endregion

        #region propriedades

        public object Current
        {
            //get => this.BindingSource.Current;
            get => ((BindingSource)this.comboBox1.DataSource).Current;
        }

        public string SelectedValue { get; set; }

        public string SelectedText { get; set; }

        public new string Text
        {
            get => comboBox1.Text;            
        }

        [Description("Label do ComboBox"), Category("Cemapa")]
        public string Label
        {   
            get => this.lbNome.Text;
            set => this.lbNome.Text = value;
        }

        [Description("BindingSource"), Category("Cemapa")]
        public BindingSource BindingSource
        {
            get => (BindingSource)this.comboBox1.DataSource;
            set => this.comboBox1.DataSource = value;
        }

        [Description("DisplayMember"), Category("Cemapa")]
        public string DisplayMember
        {
            get => this.comboBox1.DisplayMember;
            set => this.comboBox1.DisplayMember = value;
        }

        [Description("ValueMember"), Category("Cemapa")]
        public string ValueMember
        {
            get => this.comboBox1.ValueMember;
            set => this.comboBox1.ValueMember = value;
        }

        [Description("SelectedIndex"), Category("Cemapa")]
        public int SelectedIndex
        {
            get => this.comboBox1.SelectedIndex;
            set => this.comboBox1.SelectedIndex = value;
        }

        public new bool Enabled
        {
            get => this.comboBox1.Enabled;
            set
            {
                this.comboBox1.Enabled = value;
                if (value)
                {
                    this.lbNome.Font = new Font(this.lbNome.Font.FontFamily, this.lbNome.Font.Size, FontStyle.Regular);                    
                    pDesabilitado.Dock = DockStyle.None;
                    pDesabilitado.SendToBack();
                }
                else
                {   
                    this.lbNome.Font = new Font(this.lbNome.Font.FontFamily, this.lbNome.Font.Size, FontStyle.Strikeout);
                    comboBox1.SelectedIndex = -1;
                    pDesabilitado.Dock = DockStyle.Fill;
                    pDesabilitado.BringToFront();

                }
            }
        }

        #endregion

        public ComboBoxSimples()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += this.HandleSelectedIndexChanged;
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

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);            
        }

        /// <summary>
        /// Handle necessário para atualizar as properties SelectedValue e SelectedText antes de chamar
        ///  o handle OnComboBoxDropDownClosed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {   
            if (comboBox1.SelectedValue != null)
                this.SelectedValue = comboBox1.SelectedValue.ToString();
            this.SelectedText = comboBox1.SelectedText;            
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = e.KeyChar.ToString().ToUpper()[0];            
        }

        private void ComboBoxSimples_Resize(object sender, EventArgs e)
        {
            comboBox1.SelectionLength = 0;
            Refresh();
        }
    }
}
