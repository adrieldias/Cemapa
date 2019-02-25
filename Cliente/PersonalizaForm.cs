using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Security;

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
        private const int TAMANHO_FONTE = 10;


        public static PersonalizaForm Instancia
        {
            get { return _instancia ?? (_instancia = new PersonalizaForm()); }
        }

        private PersonalizaForm() { }

        public void Personaliza(Form formulario)
        {
            formulario.FormBorderStyle = FormBorderStyle.None;

            foreach (Control c in formulario.Controls)
            {
                
                if (c.GetType().Name.Equals("TextBox"))
                {
                    PersonalizaTextBox((TextBox)c);
                }
                
                if (c.GetType().Name.Equals("DataGridView"))
                {
                    PersonalizaGridView((DataGridView)c);
                }                

                if(c.GetType().Name.Equals("Label"))
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

                if (c.GetType().Name.Equals("Panel"))
                {                    
                    foreach(Control ci in c.Controls)
                    {   
                        if (ci.GetType().Name.Equals("TextBox"))
                        {
                            PersonalizaTextBox((TextBox)ci);
                        }

                        if (ci.GetType().Name.Equals("DataGridView"))
                        {
                            PersonalizaGridView((DataGridView)ci);
                        }

                        if (ci.GetType().Name.Equals("Label"))
                        {
                            PersonalizaLabel((Label)ci);
                        }

                        if (ci.GetType().Name.Equals("Button"))
                        {
                            PersonalizaButton((Button)ci);
                        }

                        if (ci.GetType().Name.Equals("ComboBox"))
                        {
                            PersonalizaComboBox((ComboBox)ci);
                        }
                    }
                }

                if(c.GetType().Name.Equals("TabControl"))
                {
                    foreach (Control ci in c.Controls) // TabControl
                    {
                       foreach (Control cii in ci.Controls) //TabPanel
                       {
                            foreach (Control ciii in cii.Controls) //TableLayout
                            {       
                                if (ciii.GetType().Name.Equals("TextBox"))
                                {
                                    PersonalizaTextBox((TextBox)ciii);
                                }

                                if (ciii.GetType().Name.Equals("DataGridView"))
                                {
                                    PersonalizaGridView((DataGridView)ciii);
                                }

                                if (ciii.GetType().Name.Equals("Label"))
                                {
                                    PersonalizaLabel((Label)ciii);
                                }

                                if (ciii.GetType().Name.Equals("Button"))
                                {
                                    PersonalizaButton((Button)ciii);
                                }

                                if (ciii.GetType().Name.Equals("ComboBox"))
                                {
                                    PersonalizaComboBox((ComboBox)ciii);
                                }
                            }
                       }
                    }
                }

                if (c.GetType().Name.Equals("TabControlLateral"))
                {
                    foreach (Control ci in c.Controls) // TabControl
                    {
                        foreach (Control cii in ci.Controls) //TabPanel
                        {
                            foreach (Control ciii in cii.Controls) //TableLayout
                            {
                                if (ciii.GetType().Name.Equals("TextBox"))
                                {
                                    PersonalizaTextBox((TextBox)ciii);
                                }

                                if (ciii.GetType().Name.Equals("DataGridView"))
                                {
                                    PersonalizaGridView((DataGridView)ciii);
                                }

                                if (ciii.GetType().Name.Equals("Label"))
                                {
                                    PersonalizaLabel((Label)ciii);
                                }

                                if (ciii.GetType().Name.Equals("Button"))
                                {
                                    PersonalizaButton((Button)ciii);
                                }

                                if (ciii.GetType().Name.Equals("ComboBox"))
                                {
                                    PersonalizaComboBox((ComboBox)ciii);
                                }
                            }
                        }
                    }
                }
            }
            //    if (formulario.Name.Substring(formulario.Name.Length - 4).Equals("Home"))
            //    {  
            //        //formulario.BackColor = System.Drawing.Color.Azure;
            //    }
        }

        #region TextBox
        private void PersonalizaTextBox(TextBox textBox)
        {
            textBox.CharacterCasing = CharacterCasing.Upper;
            textBox.Font = new Font(FONTE, TAMANHO_FONTE);
            textBox.Refresh();
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
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.SelectionBackColor = Color.LightSlateGray;
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
                //button.BackColor = Color.LightSlateGray;
                button.BackColor = Color.WhiteSmoke;
                button.ForeColor = Color.Gray;
            }
                
                
            
            button.FlatStyle = FlatStyle.Flat;            
            button.FlatAppearance.BorderSize = 0;
            //button.ForeColor = Color.White;
            
            button.Font = new Font(FONTE, TAMANHO_FONTE);
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
