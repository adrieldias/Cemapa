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

        public event EventHandler ComboBoxDropDown;
        public event EventHandler ComboBoxDropDownClosed;
        public event EventHandler SelectedValueChanged;

        private void HandleComboBoxDropDown(object sender, EventArgs e)
        {
            this.OnComboboxDropDown(EventArgs.Empty);
        }

        private void HandleComboBoxDropDownClosed(object sender, EventArgs e)
        {
            this.OnComboboxDropDownClosed(EventArgs.Empty);
        }

        private void HandleSelectedValueChanged(object sender, EventArgs e)
        {
            this.OnSelectedValueChanged(EventArgs.Empty);
        }        
               
        protected virtual void OnComboboxDropDown(EventArgs e)
        {
            EventHandler handler = this.ComboBoxDropDown;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnComboboxDropDownClosed(EventArgs e)
        {
            EventHandler handler = this.ComboBoxDropDownClosed;            
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSelectedValueChanged(EventArgs e)
        {
            EventHandler handler = this.SelectedValueChanged;
            if (handler != null)
            {
                this.SelectedValue = comboBox1.SelectedValue.ToString();
                this.SelectedText = comboBox1.SelectedText;
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

        public string Text
        {
            get => this.comboBox1.Text;            
        }

        [Description("Label do ComboBox"), Category("Cemapa")]
        public string Label
        {   
            get => this.lbNome.Text;
            set => this.lbNome.Text = value.ToUpper();
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

        #endregion

        public ComboBoxSimples()
        {
            InitializeComponent();
            comboBox1.DropDown += this.HandleComboBoxDropDown;
            comboBox1.DropDownClosed += this.HandleComboBoxDropDownClosed;
            comboBox1.SelectedValueChanged += this.HandleSelectedValueChanged;           

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.SteelBlue, ButtonBorderStyle.Solid);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            int Pos = comboBox1.SelectionStart;
            comboBox1.Text = comboBox1.Text.ToUpper();
            comboBox1.SelectionStart = Pos;
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
            this.SelectedValue = comboBox1.SelectedValue.ToString();
            this.SelectedText = comboBox1.SelectedText;
        }
    }
}
