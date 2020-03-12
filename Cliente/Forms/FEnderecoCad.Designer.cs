namespace Cliente.Forms
{
    partial class FEnderecoCad
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tbsCep = new Componentes.TextBoxSimples();
            this.cbsPais = new Componentes.ComboBoxSimples();
            this.cbsEstado = new Componentes.ComboBoxSimples();
            this.cbsCidade = new Componentes.ComboBoxSimples();
            this.tbsBairro = new Componentes.TextBoxSimples();
            this.tbsEndereco = new Componentes.TextBoxSimples();
            this.tbsComplemento = new Componentes.TextBoxSimples();
            this.cbsTipoEndereco = new Componentes.ComboBoxSimples();
            this.tbsCaixaPostal = new Componentes.TextBoxSimples();
            this.tbsDistrito = new Componentes.TextBoxSimples();
            this.pCabecalho.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pCabecalho
            // 
            this.pCabecalho.Size = new System.Drawing.Size(853, 52);
            // 
            // btCancelar
            // 
            this.btCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btCancelar.FlatAppearance.BorderSize = 0;
            this.btCancelar.Location = new System.Drawing.Point(134, 56);
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btSalvar
            // 
            this.btSalvar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btSalvar.FlatAppearance.BorderSize = 0;
            this.btSalvar.Size = new System.Drawing.Size(122, 42);
            this.btSalvar.Text = "CONFIRMAR";
            this.btSalvar.Click += new System.EventHandler(this.btSalvar_Click);
            // 
            // btFechar
            // 
            this.btFechar.Location = new System.Drawing.Point(236, 56);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Location = new System.Drawing.Point(0, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 331);
            this.panel1.TabIndex = 12;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 10;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Controls.Add(this.cbsEstado, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cbsCidade, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.tbsBairro, 7, 2);
            this.tableLayoutPanel2.Controls.Add(this.tbsEndereco, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbsComplemento, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbsTipoEndereco, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbsPais, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tbsCep, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.tbsCaixaPostal, 5, 3);
            this.tableLayoutPanel2.Controls.Add(this.tbsDistrito, 7, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(852, 331);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tbsCep
            // 
            this.tbsCep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsCep.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.tbsCep, 2);
            this.tbsCep.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsCep.Label = "CEP";
            this.tbsCep.Location = new System.Drawing.Point(258, 153);
            this.tbsCep.Name = "tbsCep";
            this.tbsCep.PasswordChar = '\0';
            this.tbsCep.Size = new System.Drawing.Size(164, 44);
            this.tbsCep.TabIndex = 9;
            // 
            // cbsPais
            // 
            this.cbsPais.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsPais.AutoSize = true;
            this.cbsPais.BindingSource = null;
            this.tableLayoutPanel2.SetColumnSpan(this.cbsPais, 3);
            this.cbsPais.DisplayMember = "";
            this.cbsPais.DroppedDown = false;
            this.cbsPais.Label = "País";
            this.cbsPais.Location = new System.Drawing.Point(3, 153);
            this.cbsPais.Name = "cbsPais";
            this.cbsPais.SelectedIndex = -1;
            this.cbsPais.SelectedText = null;
            this.cbsPais.SelectedValue = null;
            this.cbsPais.Size = new System.Drawing.Size(249, 44);
            this.cbsPais.TabIndex = 10;
            this.cbsPais.ValueMember = "";
            // 
            // cbsEstado
            // 
            this.cbsEstado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsEstado.AutoSize = true;
            this.cbsEstado.BindingSource = null;
            this.tableLayoutPanel2.SetColumnSpan(this.cbsEstado, 3);
            this.cbsEstado.DisplayMember = "";
            this.cbsEstado.DroppedDown = false;
            this.cbsEstado.Label = "Estado";
            this.cbsEstado.Location = new System.Drawing.Point(3, 103);
            this.cbsEstado.Name = "cbsEstado";
            this.cbsEstado.SelectedIndex = -1;
            this.cbsEstado.SelectedText = "DESC_ESTADO";
            this.cbsEstado.SelectedValue = "COD_ESTADO";
            this.cbsEstado.Size = new System.Drawing.Size(249, 44);
            this.cbsEstado.TabIndex = 4;
            this.cbsEstado.ValueMember = "";
            // 
            // cbsCidade
            // 
            this.cbsCidade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsCidade.AutoSize = true;
            this.cbsCidade.BindingSource = null;
            this.tableLayoutPanel2.SetColumnSpan(this.cbsCidade, 4);
            this.cbsCidade.DisplayMember = "";
            this.cbsCidade.DroppedDown = false;
            this.cbsCidade.Label = "Cidade";
            this.cbsCidade.Location = new System.Drawing.Point(258, 103);
            this.cbsCidade.Name = "cbsCidade";
            this.cbsCidade.SelectedIndex = -1;
            this.cbsCidade.SelectedText = null;
            this.cbsCidade.SelectedValue = null;
            this.cbsCidade.Size = new System.Drawing.Size(334, 44);
            this.cbsCidade.TabIndex = 11;
            this.cbsCidade.ValueMember = "";
            // 
            // tbsBairro
            // 
            this.tbsBairro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsBairro.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.tbsBairro, 3);
            this.tbsBairro.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsBairro.Label = "Bairro";
            this.tbsBairro.Location = new System.Drawing.Point(598, 103);
            this.tbsBairro.Name = "tbsBairro";
            this.tbsBairro.PasswordChar = '\0';
            this.tbsBairro.Size = new System.Drawing.Size(251, 44);
            this.tbsBairro.TabIndex = 6;
            // 
            // tbsEndereco
            // 
            this.tbsEndereco.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsEndereco.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.tbsEndereco, 5);
            this.tbsEndereco.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsEndereco.Label = "Endereço (Informar Número Após Vírgula)";
            this.tbsEndereco.Location = new System.Drawing.Point(3, 53);
            this.tbsEndereco.Name = "tbsEndereco";
            this.tbsEndereco.PasswordChar = '\0';
            this.tbsEndereco.Size = new System.Drawing.Size(419, 44);
            this.tbsEndereco.TabIndex = 7;
            // 
            // tbsComplemento
            // 
            this.tbsComplemento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsComplemento.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.tbsComplemento, 5);
            this.tbsComplemento.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsComplemento.Label = "Complemento";
            this.tbsComplemento.Location = new System.Drawing.Point(428, 53);
            this.tbsComplemento.Name = "tbsComplemento";
            this.tbsComplemento.PasswordChar = '\0';
            this.tbsComplemento.Size = new System.Drawing.Size(421, 44);
            this.tbsComplemento.TabIndex = 8;
            // 
            // cbsTipoEndereco
            // 
            this.cbsTipoEndereco.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsTipoEndereco.AutoSize = true;
            this.cbsTipoEndereco.BindingSource = null;
            this.tableLayoutPanel2.SetColumnSpan(this.cbsTipoEndereco, 5);
            this.cbsTipoEndereco.DisplayMember = "";
            this.cbsTipoEndereco.DroppedDown = false;
            this.cbsTipoEndereco.Label = "Tipo de Endereço";
            this.cbsTipoEndereco.Location = new System.Drawing.Point(3, 3);
            this.cbsTipoEndereco.Name = "cbsTipoEndereco";
            this.cbsTipoEndereco.SelectedIndex = -1;
            this.cbsTipoEndereco.SelectedText = null;
            this.cbsTipoEndereco.SelectedValue = null;
            this.cbsTipoEndereco.Size = new System.Drawing.Size(419, 44);
            this.cbsTipoEndereco.TabIndex = 12;
            this.cbsTipoEndereco.ValueMember = "";
            // 
            // tbsCaixaPostal
            // 
            this.tbsCaixaPostal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsCaixaPostal.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.tbsCaixaPostal, 2);
            this.tbsCaixaPostal.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsCaixaPostal.Label = "Caixa Postal";
            this.tbsCaixaPostal.Location = new System.Drawing.Point(428, 153);
            this.tbsCaixaPostal.Name = "tbsCaixaPostal";
            this.tbsCaixaPostal.PasswordChar = '\0';
            this.tbsCaixaPostal.Size = new System.Drawing.Size(164, 44);
            this.tbsCaixaPostal.TabIndex = 13;
            // 
            // tbsDistrito
            // 
            this.tbsDistrito.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsDistrito.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.tbsDistrito, 3);
            this.tbsDistrito.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsDistrito.Label = "Distrito";
            this.tbsDistrito.Location = new System.Drawing.Point(598, 153);
            this.tbsDistrito.Name = "tbsDistrito";
            this.tbsDistrito.PasswordChar = '\0';
            this.tbsDistrito.Size = new System.Drawing.Size(251, 44);
            this.tbsDistrito.TabIndex = 14;
            // 
            // FEnderecoCad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 453);
            this.Controls.Add(this.panel1);
            this.Name = "FEnderecoCad";
            this.Text = "FEnderecoCadcs";
            this.Load += new System.EventHandler(this.FEnderecoCad_Load);
            this.Controls.SetChildIndex(this.btFechar, 0);
            this.Controls.SetChildIndex(this.pCabecalho, 0);
            this.Controls.SetChildIndex(this.btSalvar, 0);
            this.Controls.SetChildIndex(this.btCancelar, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.pCabecalho.ResumeLayout(false);
            this.pCabecalho.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Componentes.TextBoxSimples tbsCep;
        private Componentes.ComboBoxSimples cbsPais;
        private Componentes.TextBoxSimples tbsEndereco;
        private Componentes.TextBoxSimples tbsComplemento;
        private Componentes.ComboBoxSimples cbsEstado;
        private Componentes.ComboBoxSimples cbsCidade;
        private Componentes.TextBoxSimples tbsBairro;
        private Componentes.ComboBoxSimples cbsTipoEndereco;
        private Componentes.TextBoxSimples tbsCaixaPostal;
        private Componentes.TextBoxSimples tbsDistrito;
    }
}