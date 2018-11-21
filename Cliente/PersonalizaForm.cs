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
                // Seta personalizações do TextBox
                if (c.GetType().Name.Equals("TextBox"))
                {
                    ((TextBox)c).CharacterCasing = CharacterCasing.Upper;
                    ((TextBox)c).Font = new Font(FONTE, TAMANHO_FONTE);
                }

                // Seta personalizações do GridView
                if (c.GetType().Name.Equals("DataGridView"))
                {
                    ((DataGridView)c).AllowUserToAddRows = true;
                    ((DataGridView)c).CellValueChanged += PersonalizaForm_CellValueChanged;                   
                    ((DataGridView)c).Font = new Font(FONTE, TAMANHO_FONTE);
                }
            }
            //    if (formulario.Name.Substring(formulario.Name.Length - 4).Equals("Home"))
            //    {  
            //        //formulario.BackColor = System.Drawing.Color.Azure;
            //    }
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
    }
}
