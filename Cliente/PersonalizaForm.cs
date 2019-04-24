using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Security;
using Componentes;

#region Informações
/*---------Informações sobre configurações das personalizações que esta classe efetua---------*/


//**** TEXTBOX ****
// Padrões
// CharCase Upper
// Fonte Calibri Light 10pt

// Configurações



#endregion

namespace Cliente
{
    public sealed class PersonalizaForm
    {
        static PersonalizaForm _instancia;

        private const string FONTE = "Calibri Light";
        private const int TAMANHO_FONTE = 12;


        public static PersonalizaForm Instancia
        {
            get { return _instancia ?? (_instancia = new PersonalizaForm()); }
        }

        private PersonalizaForm() { }

        private void PersonalizaControles(Control c)
        {
            if (c.GetType().Name.Equals("TextBox"))
            {
                PersonalizaTextBox((TextBox)c);
            }

            if (c.GetType().Name.Equals("TextBoxSimples"))
            {
                PersonalizaTextBoxSimples((TextBoxSimples)c);
            }

            if (c.GetType().Name.Equals("DataGridView"))
            {
                PersonalizaGridView((DataGridView)c);
            }

            if (c.GetType().Name.Equals("Label"))
            {
                PersonalizaLabel((Label)c);
            }

            if (c.GetType().Name.Equals("Button"))
            {
                PersonalizaButton((Button)c);
            }

            if (c.GetType().Name.Equals("ComboBox"))
            {
                PersonalizaComboBox((ComboBox)c);
            }

            if (c.HasChildren)
            {
                foreach (Control ci in c.Controls)
                {
                    PersonalizaControles(ci);
                }
            }
        }

        public void Personaliza(Form formulario)
        {
            formulario.FormBorderStyle = FormBorderStyle.None;

            // Personaliza em até três níveis (controle dentro de controle dentro de controle)
            foreach(Control c in formulario.Controls)
            {
                //foreach(Control ci in c.Controls)
                //{
                //    foreach (Control cii in ci.Controls)
                //    {
                //        foreach (Control ciii in cii.Controls)
                //        {
                //            PersonalizaControles(ciii);
                //        }
                //        PersonalizaControles(cii);
                //    }
                //    PersonalizaControles(ci);
                //}
                PersonalizaControles(c);
            }       
        }

        #region TextBox
        private void PersonalizaTextBox(TextBox textBox)
        {
            textBox.CharacterCasing = CharacterCasing.Upper;
            textBox.Font = new Font(FONTE, TAMANHO_FONTE);            
            textBox.Refresh();            
        }
        #endregion

        #region TextBoxSimples
        private void PersonalizaTextBoxSimples(TextBoxSimples textBoxSimples)
        {
            textBoxSimples.Leave += TextBoxSimples_Leave;
        }

        private void TextBoxSimples_Leave(object sender, EventArgs e)
        {            
            if ((sender as TextBoxSimples).DataBindings != null && (sender as TextBoxSimples).Text != string.Empty)
            {
                var value = ((sender as TextBoxSimples).Text);
                var ds = (sender as TextBoxSimples).DataBindings[0].DataSource;
                var dataMember = (sender as TextBoxSimples).DataBindings[0].BindingMemberInfo.BindingMember;
                PropertyInfo propertyInfo = ds.GetType().GetProperty(dataMember);
                if (propertyInfo == null)
                {
                    MessageBox.Show(string.Format(
                        "O controle {0} em {1} não possui datamember"
                        , (sender as TextBoxSimples).Name
                        , (sender as TextBoxSimples).ParentForm.Name
                        )
                        , "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (propertyInfo.GetValue(ds, null).ToString() != value)
                {
                    Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                    object safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                    propertyInfo.SetValue(ds, safeValue, null);
                }
            }
        }
        #endregion

        #region GridView
        private void PersonalizaGridView(DataGridView dataGridView)
        {
            dataGridView.AllowUserToAddRows = true;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.CellValueChanged += PersonalizaForm_CellValueChanged;
            dataGridView.DataBindingComplete += PersonalizaForm_DataBindingComplete;
            dataGridView.CellFormatting += DataGridView_CellFormatting;
            dataGridView.Font = new Font(FONTE, TAMANHO_FONTE);
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.BackgroundColor = Color.White;

            dataGridView.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView.AdvancedCellBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView.AllowUserToResizeRows = false;
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.SelectionBackColor = Color.SteelBlue;
            if((e.Value != null) 
                && ((e.Value.ToString().ToUpper().Contains("SEPROCAD"))
                    || (e.Value.ToString().ToUpper().Contains("CANCELAD"))
                    || (e.Value.ToString().ToUpper().Contains("REPROVAD"))
                    || (e.Value.ToString().ToUpper().Contains("RUIM"))
                    || (e.Value.ToString().ToUpper().Contains("PENDENTE"))
                    || (e.Value.ToString().ToUpper().Contains("INVALID"))
                    || (e.Value.ToString().ToUpper().Contains("DENEGAD"))
                )
            )
            {
                //e.CellStyle.ForeColor = Color.Tomato;
                ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Tomato;                
            }

            //if (((DataGridView)sender).Columns[e.ColumnIndex].Name.Equals("Classificacao"))
            //{  
            //if (((DataGridView)sender).Columns[e.ColumnIndex].Name != null)
            //    if (e.Value.ToString().Equals("SEPROCADO"))
            //    {
            //        e.CellStyle.BackColor = Color.Red;
            //        e.CellStyle.SelectionBackColor = Color.DarkRed;
            //    }
            //}
        }

        private void PersonalizaForm_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((DataGridView)sender).AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void PersonalizaForm_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {   
            try
            {
                var valor = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if(valor.GetType().Equals(typeof(string)))
                {
                       ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value
                        = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToUpper();
                }                
            }
            catch(Exception ex)
            { }
        }
        #endregion

        #region Label
        private void PersonalizaLabel(Label label)
        {
            label.Text = label.Text.ToUpper();
            if (label.Name.Equals("lbNome"))
            {

            }
            else
            {

                label.Margin = new Padding(0, 0, 3, 0);
                label.Dock = DockStyle.Bottom;
            }
        }
        #endregion

        #region Button
        private void PersonalizaButton(Button button)
        {
            if (button.Name.Equals("btMinimize") || button.Name.Equals("btCloseForm"))
                return;
            if (button.Text.ToUpper().Contains("EXCLUI")
                || button.Text.ToUpper().Contains("CANCEL")
                || button.Text.ToUpper().Contains("DELET")
                || button.Text.ToUpper().Contains("ABORT"))
            {
                button.BackColor = Color.Tomato;
                button.ForeColor = Color.WhiteSmoke;
            }
            else
            {                
                button.BackColor = Color.WhiteSmoke;
                //button.ForeColor = Color.Gray;
                button.ForeColor = Color.DarkSlateGray;
            }
            
            button.FlatStyle = FlatStyle.Flat;            
            button.FlatAppearance.BorderSize = 0;
            //button.ForeColor = Color.White;
            
            button.Font = new Font(FONTE, 10);
            button.Text = button.Text.ToUpper();
        }
        #endregion

        #region ComboBox
        private void PersonalizaComboBox(ComboBox comboBox)
        {   
            comboBox.Font = new Font(FONTE, TAMANHO_FONTE);
            comboBox.Refresh();
        }
        #endregion


    }
}
