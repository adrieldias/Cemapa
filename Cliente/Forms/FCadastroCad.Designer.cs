namespace Cliente.Forms
{
    partial class FCadastroCad
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCadastroCad));
            this.tabControlLateral2 = new TabControlLateral.TabControlLateral(this.components);
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbsTipoCadastro = new Componentes.ComboBoxSimples();
            this.cbsFisicaJuridica = new Componentes.ComboBoxSimples();
            this.txbsNome = new Componentes.TextBoxSimples();
            this.txbsNomeFantasia = new Componentes.TextBoxSimples();
            this.txbsCodigo = new Componentes.TextBoxSimples();
            this.txbsTelefone = new Componentes.TextBoxSimples();
            this.txbsCelular = new Componentes.TextBoxSimples();
            this.txbsEmailXML = new Componentes.TextBoxSimples();
            this.txbsEmailContato = new Componentes.TextBoxSimples();
            this.cbsClassificacao = new Componentes.ComboBoxSimples();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControlLateral1 = new TabControlLateral.TabControlLateral(this.components);
            this.cbsVendedor = new Componentes.ComboBoxSimples();
            this.pCabecalho.SuspendLayout();
            this.tabControlLateral2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pCabecalho
            // 
            this.pCabecalho.Size = new System.Drawing.Size(900, 52);
            // 
            // btFechar
            // 
            this.btFechar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            // 
            // btCancelar
            // 
            this.btCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btSalvar
            // 
            this.btSalvar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btSalvar.Click += new System.EventHandler(this.btSalvar_Click);
            // 
            // tabControlLateral2
            // 
            this.tabControlLateral2.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlLateral2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlLateral2.Controls.Add(this.tabPage1);
            this.tabControlLateral2.Controls.Add(this.tabPage3);
            this.tabControlLateral2.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlLateral2.ItemSize = new System.Drawing.Size(30, 130);
            this.tabControlLateral2.Location = new System.Drawing.Point(0, 109);
            this.tabControlLateral2.Multiline = true;
            this.tabControlLateral2.Name = "tabControlLateral2";
            this.tabControlLateral2.SelectedIndex = 0;
            this.tabControlLateral2.Size = new System.Drawing.Size(900, 470);
            this.tabControlLateral2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlLateral2.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(134, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(762, 462);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dados";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbsTipoCadastro, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbsFisicaJuridica, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.txbsNome, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txbsNomeFantasia, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.txbsCodigo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbsTelefone, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txbsCelular, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.txbsEmailXML, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.txbsEmailContato, 7, 5);
            this.tableLayoutPanel1.Controls.Add(this.cbsClassificacao, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cbsVendedor, 3, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(750, 456);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox1, 2);
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(144, 144);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // cbsTipoCadastro
            // 
            this.cbsTipoCadastro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsTipoCadastro.AutoSize = true;
            this.cbsTipoCadastro.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cbsTipoCadastro.BindingSource = null;
            this.tableLayoutPanel1.SetColumnSpan(this.cbsTipoCadastro, 5);
            this.cbsTipoCadastro.DisplayMember = "";
            this.cbsTipoCadastro.Label = "TIPO DE CADASTRO";
            this.cbsTipoCadastro.Location = new System.Drawing.Point(3, 213);
            this.cbsTipoCadastro.Name = "cbsTipoCadastro";
            this.cbsTipoCadastro.SelectedText = null;
            this.cbsTipoCadastro.SelectedValue = null;
            this.cbsTipoCadastro.Size = new System.Drawing.Size(369, 44);
            this.cbsTipoCadastro.TabIndex = 24;
            this.cbsTipoCadastro.ValueMember = "";
            // 
            // cbsFisicaJuridica
            // 
            this.cbsFisicaJuridica.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsFisicaJuridica.AutoSize = true;
            this.cbsFisicaJuridica.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cbsFisicaJuridica.BindingSource = null;
            this.tableLayoutPanel1.SetColumnSpan(this.cbsFisicaJuridica, 5);
            this.cbsFisicaJuridica.DisplayMember = "DESC_FISICA_JURIDICA";
            this.cbsFisicaJuridica.Label = "PESSOA FÍSICA/JURÍDICA";
            this.cbsFisicaJuridica.Location = new System.Drawing.Point(378, 213);
            this.cbsFisicaJuridica.Name = "cbsFisicaJuridica";
            this.cbsFisicaJuridica.SelectedText = null;
            this.cbsFisicaJuridica.SelectedValue = null;
            this.cbsFisicaJuridica.Size = new System.Drawing.Size(369, 44);
            this.cbsFisicaJuridica.TabIndex = 25;
            this.cbsFisicaJuridica.ValueMember = "IND_FISICA_JURIDICA";
            this.cbsFisicaJuridica.ComboBoxDropDownClosed += new System.EventHandler(this.cbsFisicaJuridica_ComboBoxDropDownClosed);
            this.cbsFisicaJuridica.SelectedValueChanged += new System.EventHandler(this.cbsFisicaJuridica_SelectedValueChanged);
            this.cbsFisicaJuridica.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbsFisicaJuridica_KeyUp);
            this.cbsFisicaJuridica.Leave += new System.EventHandler(this.cbsFisicaJuridica_Leave);
            // 
            // txbsNome
            // 
            this.txbsNome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbsNome, 5);
            this.txbsNome.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsNome.Label = "NOME";
            this.txbsNome.Location = new System.Drawing.Point(4, 264);
            this.txbsNome.Margin = new System.Windows.Forms.Padding(4);
            this.txbsNome.Name = "txbsNome";
            this.txbsNome.PasswordChar = '\0';
            this.txbsNome.Size = new System.Drawing.Size(367, 42);
            this.txbsNome.TabIndex = 26;
            this.txbsNome.Value = "";
            // 
            // txbsNomeFantasia
            // 
            this.txbsNomeFantasia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbsNomeFantasia, 5);
            this.txbsNomeFantasia.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsNomeFantasia.Label = "NOME FANTASIA";
            this.txbsNomeFantasia.Location = new System.Drawing.Point(379, 264);
            this.txbsNomeFantasia.Margin = new System.Windows.Forms.Padding(4);
            this.txbsNomeFantasia.Name = "txbsNomeFantasia";
            this.txbsNomeFantasia.PasswordChar = '\0';
            this.txbsNomeFantasia.Size = new System.Drawing.Size(367, 42);
            this.txbsNomeFantasia.TabIndex = 27;
            this.txbsNomeFantasia.Value = "";
            // 
            // txbsCodigo
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txbsCodigo, 2);
            this.txbsCodigo.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsCodigo.Label = "CÓDIGO";
            this.txbsCodigo.Location = new System.Drawing.Point(4, 164);
            this.txbsCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txbsCodigo.Name = "txbsCodigo";
            this.txbsCodigo.PasswordChar = '\0';
            this.txbsCodigo.Size = new System.Drawing.Size(142, 42);
            this.txbsCodigo.TabIndex = 28;
            this.txbsCodigo.Value = "";
            // 
            // txbsTelefone
            // 
            this.txbsTelefone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbsTelefone, 2);
            this.txbsTelefone.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsTelefone.Label = "TELEFONE";
            this.txbsTelefone.Location = new System.Drawing.Point(4, 314);
            this.txbsTelefone.Margin = new System.Windows.Forms.Padding(4);
            this.txbsTelefone.Name = "txbsTelefone";
            this.txbsTelefone.PasswordChar = '\0';
            this.txbsTelefone.Size = new System.Drawing.Size(142, 42);
            this.txbsTelefone.TabIndex = 29;
            this.txbsTelefone.Value = "";
            // 
            // txbsCelular
            // 
            this.txbsCelular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbsCelular, 2);
            this.txbsCelular.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsCelular.Label = "CELULAR";
            this.txbsCelular.Location = new System.Drawing.Point(154, 314);
            this.txbsCelular.Margin = new System.Windows.Forms.Padding(4);
            this.txbsCelular.Name = "txbsCelular";
            this.txbsCelular.PasswordChar = '\0';
            this.txbsCelular.Size = new System.Drawing.Size(142, 42);
            this.txbsCelular.TabIndex = 30;
            this.txbsCelular.Value = "";
            // 
            // txbsEmailXML
            // 
            this.txbsEmailXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbsEmailXML, 3);
            this.txbsEmailXML.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsEmailXML.Label = "E-MAIL PARA XML";
            this.txbsEmailXML.Location = new System.Drawing.Point(304, 314);
            this.txbsEmailXML.Margin = new System.Windows.Forms.Padding(4);
            this.txbsEmailXML.Name = "txbsEmailXML";
            this.txbsEmailXML.PasswordChar = '\0';
            this.txbsEmailXML.Size = new System.Drawing.Size(217, 42);
            this.txbsEmailXML.TabIndex = 31;
            this.txbsEmailXML.Value = "";
            // 
            // txbsEmailContato
            // 
            this.txbsEmailContato.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbsEmailContato, 3);
            this.txbsEmailContato.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsEmailContato.Label = "E-MAIL PARA CONTATO";
            this.txbsEmailContato.Location = new System.Drawing.Point(529, 314);
            this.txbsEmailContato.Margin = new System.Windows.Forms.Padding(4);
            this.txbsEmailContato.Name = "txbsEmailContato";
            this.txbsEmailContato.PasswordChar = '\0';
            this.txbsEmailContato.Size = new System.Drawing.Size(217, 42);
            this.txbsEmailContato.TabIndex = 32;
            this.txbsEmailContato.Value = "";
            // 
            // cbsClassificacao
            // 
            this.cbsClassificacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsClassificacao.BindingSource = null;
            this.tableLayoutPanel1.SetColumnSpan(this.cbsClassificacao, 3);
            this.cbsClassificacao.DisplayMember = "";
            this.cbsClassificacao.Label = "CLASSIFICAÇÃO";
            this.cbsClassificacao.Location = new System.Drawing.Point(3, 363);
            this.cbsClassificacao.Name = "cbsClassificacao";
            this.cbsClassificacao.SelectedText = null;
            this.cbsClassificacao.SelectedValue = null;
            this.cbsClassificacao.Size = new System.Drawing.Size(219, 44);
            this.cbsClassificacao.TabIndex = 33;
            this.cbsClassificacao.ValueMember = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(134, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(762, 462);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Endereço";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabControlLateral1
            // 
            this.tabControlLateral1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlLateral1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlLateral1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlLateral1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlLateral1.Font = new System.Drawing.Font("Segoe UI Semilight", 9F);
            this.tabControlLateral1.ItemSize = new System.Drawing.Size(140, 20);
            this.tabControlLateral1.Location = new System.Drawing.Point(0, 0);
            this.tabControlLateral1.Multiline = true;
            this.tabControlLateral1.Name = "tabControlLateral1";
            this.tabControlLateral1.SelectedIndex = 0;
            this.tabControlLateral1.Size = new System.Drawing.Size(200, 100);
            this.tabControlLateral1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlLateral1.TabIndex = 0;
            // 
            // cbsVendedor
            // 
            this.cbsVendedor.BindingSource = null;
            this.tableLayoutPanel1.SetColumnSpan(this.cbsVendedor, 4);
            this.cbsVendedor.DisplayMember = "";
            this.cbsVendedor.Label = "VENDEDOR";
            this.cbsVendedor.Location = new System.Drawing.Point(228, 363);
            this.cbsVendedor.Name = "cbsVendedor";
            this.cbsVendedor.SelectedText = null;
            this.cbsVendedor.SelectedValue = null;
            this.cbsVendedor.Size = new System.Drawing.Size(294, 44);
            this.cbsVendedor.TabIndex = 34;
            this.cbsVendedor.ValueMember = "";
            // 
            // FCadastroCad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.tabControlLateral2);
            this.Name = "FCadastroCad";
            this.Text = "FCadastroCad";
            this.Load += new System.EventHandler(this.FCadastroCad_Load);
            this.Controls.SetChildIndex(this.tabControlLateral2, 0);
            this.Controls.SetChildIndex(this.btSalvar, 0);
            this.Controls.SetChildIndex(this.btCancelar, 0);
            this.Controls.SetChildIndex(this.btFechar, 0);
            this.Controls.SetChildIndex(this.pCabecalho, 0);
            this.pCabecalho.ResumeLayout(false);
            this.pCabecalho.PerformLayout();
            this.tabControlLateral2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TabControlLateral.TabControlLateral tabControlLateral1;
        private TabControlLateral.TabControlLateral tabControlLateral2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Componentes.ComboBoxSimples cbsTipoCadastro;
        private Componentes.ComboBoxSimples cbsFisicaJuridica;
        private Componentes.TextBoxSimples txbsNome;
        private Componentes.TextBoxSimples txbsNomeFantasia;
        private Componentes.TextBoxSimples txbsCodigo;
        private Componentes.TextBoxSimples txbsTelefone;
        private Componentes.TextBoxSimples txbsCelular;
        private Componentes.TextBoxSimples txbsEmailXML;
        private Componentes.TextBoxSimples txbsEmailContato;
        private Componentes.ComboBoxSimples cbsClassificacao;
        private Componentes.ComboBoxSimples cbsVendedor;
    }
}