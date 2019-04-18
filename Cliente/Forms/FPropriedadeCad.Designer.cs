namespace Cliente.Forms
{
    partial class FPropriedadeCad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tbsNomePropriedade = new Componentes.TextBoxSimples();
            this.tbsEnderecoPropriedade = new Componentes.TextBoxSimples();
            this.cbsEstado = new Componentes.ComboBoxSimples();
            this.cbsCidade = new Componentes.ComboBoxSimples();
            this.tbsBairroPropriedade = new Componentes.TextBoxSimples();
            this.tbsCepPropriedade = new Componentes.TextBoxSimples();
            this.tbsArea = new Componentes.TextBoxSimples();
            this.tbsMatriculaPropriedade = new Componentes.TextBoxSimples();
            this.tbsValorPropriedade = new Componentes.TextBoxSimples();
            this.tbsCri = new Componentes.TextBoxSimples();
            this.cbsTipoPropriedade = new Componentes.ComboBoxSimples();
            this.cbsPropriedadePropria = new Componentes.ComboBoxSimples();
            this.pCabecalho.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pCabecalho
            // 
            this.pCabecalho.Size = new System.Drawing.Size(868, 52);
            // 
            // btFechar
            // 
            this.btFechar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btFechar.FlatAppearance.BorderSize = 0;
            // 
            // btCancelar
            // 
            this.btCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btCancelar.FlatAppearance.BorderSize = 0;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btSalvar
            // 
            this.btSalvar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btSalvar.FlatAppearance.BorderSize = 0;
            this.btSalvar.Click += new System.EventHandler(this.btSalvar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Location = new System.Drawing.Point(0, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(868, 378);
            this.panel1.TabIndex = 11;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 10;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.Controls.Add(this.tbsNomePropriedade, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tbsEnderecoPropriedade, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.cbsEstado, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.cbsCidade, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.tbsBairroPropriedade, 7, 1);
            this.tableLayoutPanel3.Controls.Add(this.tbsCepPropriedade, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.tbsArea, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.tbsMatriculaPropriedade, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.tbsValorPropriedade, 6, 2);
            this.tableLayoutPanel3.Controls.Add(this.tbsCri, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.cbsTipoPropriedade, 6, 3);
            this.tableLayoutPanel3.Controls.Add(this.cbsPropriedadePropria, 0, 4);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(868, 378);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tbsNomePropriedade
            // 
            this.tbsNomePropriedade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsNomePropriedade.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.tbsNomePropriedade, 5);
            this.tbsNomePropriedade.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsNomePropriedade.Label = "NOME DA PROPRIEDADE";
            this.tbsNomePropriedade.Location = new System.Drawing.Point(3, 3);
            this.tbsNomePropriedade.Name = "tbsNomePropriedade";
            this.tbsNomePropriedade.PasswordChar = '\0';
            this.tbsNomePropriedade.Size = new System.Drawing.Size(424, 44);
            this.tbsNomePropriedade.TabIndex = 0;
            this.tbsNomePropriedade.Value = "";
            // 
            // tbsEnderecoPropriedade
            // 
            this.tbsEnderecoPropriedade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsEnderecoPropriedade.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.tbsEnderecoPropriedade, 5);
            this.tbsEnderecoPropriedade.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsEnderecoPropriedade.Label = "ENDEREÇO (INFORMAR NÚMERO APÓS VÍRGULA)";
            this.tbsEnderecoPropriedade.Location = new System.Drawing.Point(433, 3);
            this.tbsEnderecoPropriedade.Name = "tbsEnderecoPropriedade";
            this.tbsEnderecoPropriedade.PasswordChar = '\0';
            this.tbsEnderecoPropriedade.Size = new System.Drawing.Size(432, 44);
            this.tbsEnderecoPropriedade.TabIndex = 1;
            this.tbsEnderecoPropriedade.Value = "";
            // 
            // cbsEstado
            // 
            this.cbsEstado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsEstado.AutoSize = true;
            this.cbsEstado.BindingSource = null;
            this.tableLayoutPanel3.SetColumnSpan(this.cbsEstado, 3);
            this.cbsEstado.DisplayMember = "";
            this.cbsEstado.Label = "ESTADO";
            this.cbsEstado.Location = new System.Drawing.Point(3, 53);
            this.cbsEstado.Name = "cbsEstado";
            this.cbsEstado.SelectedText = null;
            this.cbsEstado.SelectedValue = null;
            this.cbsEstado.Size = new System.Drawing.Size(252, 44);
            this.cbsEstado.TabIndex = 2;
            this.cbsEstado.ValueMember = "";
            // 
            // cbsCidade
            // 
            this.cbsCidade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsCidade.AutoSize = true;
            this.cbsCidade.BindingSource = null;
            this.tableLayoutPanel3.SetColumnSpan(this.cbsCidade, 4);
            this.cbsCidade.DisplayMember = "";
            this.cbsCidade.Label = "CIDADE";
            this.cbsCidade.Location = new System.Drawing.Point(261, 53);
            this.cbsCidade.Name = "cbsCidade";
            this.cbsCidade.SelectedText = null;
            this.cbsCidade.SelectedValue = null;
            this.cbsCidade.Size = new System.Drawing.Size(338, 44);
            this.cbsCidade.TabIndex = 3;
            this.cbsCidade.ValueMember = "";
            // 
            // tbsBairroPropriedade
            // 
            this.tbsBairroPropriedade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsBairroPropriedade.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.tbsBairroPropriedade, 3);
            this.tbsBairroPropriedade.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsBairroPropriedade.Label = "BAIRRO";
            this.tbsBairroPropriedade.Location = new System.Drawing.Point(605, 53);
            this.tbsBairroPropriedade.Name = "tbsBairroPropriedade";
            this.tbsBairroPropriedade.PasswordChar = '\0';
            this.tbsBairroPropriedade.Size = new System.Drawing.Size(260, 44);
            this.tbsBairroPropriedade.TabIndex = 4;
            this.tbsBairroPropriedade.Value = "";
            // 
            // tbsCepPropriedade
            // 
            this.tbsCepPropriedade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsCepPropriedade.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.tbsCepPropriedade, 3);
            this.tbsCepPropriedade.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsCepPropriedade.Label = "CEP";
            this.tbsCepPropriedade.Location = new System.Drawing.Point(3, 103);
            this.tbsCepPropriedade.Name = "tbsCepPropriedade";
            this.tbsCepPropriedade.PasswordChar = '\0';
            this.tbsCepPropriedade.Size = new System.Drawing.Size(252, 44);
            this.tbsCepPropriedade.TabIndex = 5;
            this.tbsCepPropriedade.Value = "";
            // 
            // tbsArea
            // 
            this.tbsArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsArea.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.tbsArea, 3);
            this.tbsArea.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsArea.Label = "ÁREA";
            this.tbsArea.Location = new System.Drawing.Point(261, 103);
            this.tbsArea.Name = "tbsArea";
            this.tbsArea.PasswordChar = '\0';
            this.tbsArea.Size = new System.Drawing.Size(252, 44);
            this.tbsArea.TabIndex = 7;
            this.tbsArea.Value = "";
            // 
            // tbsMatriculaPropriedade
            // 
            this.tbsMatriculaPropriedade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsMatriculaPropriedade.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.tbsMatriculaPropriedade, 3);
            this.tbsMatriculaPropriedade.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsMatriculaPropriedade.Label = "MATRÍCULA";
            this.tbsMatriculaPropriedade.Location = new System.Drawing.Point(3, 153);
            this.tbsMatriculaPropriedade.Name = "tbsMatriculaPropriedade";
            this.tbsMatriculaPropriedade.PasswordChar = '\0';
            this.tbsMatriculaPropriedade.Size = new System.Drawing.Size(252, 44);
            this.tbsMatriculaPropriedade.TabIndex = 6;
            this.tbsMatriculaPropriedade.Value = "";
            // 
            // tbsValorPropriedade
            // 
            this.tbsValorPropriedade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsValorPropriedade.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.tbsValorPropriedade, 4);
            this.tbsValorPropriedade.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsValorPropriedade.Label = "VALOR";
            this.tbsValorPropriedade.Location = new System.Drawing.Point(519, 103);
            this.tbsValorPropriedade.Name = "tbsValorPropriedade";
            this.tbsValorPropriedade.PasswordChar = '\0';
            this.tbsValorPropriedade.Size = new System.Drawing.Size(346, 44);
            this.tbsValorPropriedade.TabIndex = 8;
            this.tbsValorPropriedade.Value = "";
            // 
            // tbsCri
            // 
            this.tbsCri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsCri.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.tbsCri, 3);
            this.tbsCri.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsCri.Label = "CRI";
            this.tbsCri.Location = new System.Drawing.Point(261, 153);
            this.tbsCri.Name = "tbsCri";
            this.tbsCri.PasswordChar = '\0';
            this.tbsCri.Size = new System.Drawing.Size(252, 44);
            this.tbsCri.TabIndex = 9;
            this.tbsCri.Value = "";
            // 
            // cbsTipoPropriedade
            // 
            this.cbsTipoPropriedade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsTipoPropriedade.AutoSize = true;
            this.cbsTipoPropriedade.BindingSource = null;
            this.tableLayoutPanel3.SetColumnSpan(this.cbsTipoPropriedade, 4);
            this.cbsTipoPropriedade.DisplayMember = "";
            this.cbsTipoPropriedade.Label = "TIPO DE PROPRIEDADE";
            this.cbsTipoPropriedade.Location = new System.Drawing.Point(519, 153);
            this.cbsTipoPropriedade.Name = "cbsTipoPropriedade";
            this.cbsTipoPropriedade.SelectedText = null;
            this.cbsTipoPropriedade.SelectedValue = null;
            this.cbsTipoPropriedade.Size = new System.Drawing.Size(346, 44);
            this.cbsTipoPropriedade.TabIndex = 10;
            this.cbsTipoPropriedade.ValueMember = "";
            // 
            // cbsPropriedadePropria
            // 
            this.cbsPropriedadePropria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsPropriedadePropria.AutoSize = true;
            this.cbsPropriedadePropria.BindingSource = null;
            this.tableLayoutPanel3.SetColumnSpan(this.cbsPropriedadePropria, 2);
            this.cbsPropriedadePropria.DisplayMember = "";
            this.cbsPropriedadePropria.Label = "PRÓPRIA";
            this.cbsPropriedadePropria.Location = new System.Drawing.Point(3, 203);
            this.cbsPropriedadePropria.Name = "cbsPropriedadePropria";
            this.cbsPropriedadePropria.SelectedText = null;
            this.cbsPropriedadePropria.SelectedValue = null;
            this.cbsPropriedadePropria.Size = new System.Drawing.Size(166, 44);
            this.cbsPropriedadePropria.TabIndex = 11;
            this.cbsPropriedadePropria.ValueMember = "";
            // 
            // FPropriedadeCad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 498);
            this.Controls.Add(this.panel1);
            this.Name = "FPropriedadeCad";
            this.Text = "FPropriedadeCad";
            this.Load += new System.EventHandler(this.FPropriedadeCad_Load);
            this.Controls.SetChildIndex(this.pCabecalho, 0);
            this.Controls.SetChildIndex(this.btSalvar, 0);
            this.Controls.SetChildIndex(this.btCancelar, 0);
            this.Controls.SetChildIndex(this.btFechar, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.pCabecalho.ResumeLayout(false);
            this.pCabecalho.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Componentes.TextBoxSimples tbsNomePropriedade;
        private Componentes.TextBoxSimples tbsEnderecoPropriedade;
        private Componentes.ComboBoxSimples cbsEstado;
        private Componentes.ComboBoxSimples cbsCidade;
        private Componentes.TextBoxSimples tbsBairroPropriedade;
        private Componentes.TextBoxSimples tbsCepPropriedade;
        private Componentes.TextBoxSimples tbsArea;
        private Componentes.TextBoxSimples tbsMatriculaPropriedade;
        private Componentes.TextBoxSimples tbsValorPropriedade;
        private Componentes.TextBoxSimples tbsCri;
        private Componentes.ComboBoxSimples cbsTipoPropriedade;
        private Componentes.ComboBoxSimples cbsPropriedadePropria;
    }
}