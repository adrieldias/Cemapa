using Cliente.Forms.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliente.POCO;

namespace Cliente.Forms
{
    public partial class FPropriedadeCad : FModeloCad
    {
        #region Properties
        public TB_PROPRIEDADE Propriedade { get; set; }
        #endregion

        public FPropriedadeCad(string layout) : base(layout)
        {
            InitializeComponent();
            this.Personaliza();
            this.lbNome.Text = "FORMULÁRIO DE PROPRIEDADE";
        }
    }
}
