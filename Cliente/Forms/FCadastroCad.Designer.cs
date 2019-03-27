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
            this.tabControlLateral1 = new TabControlLateral.TabControlLateral(this.components);
            this.slickBlueTabControl1 = new SlickBlueTabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbsTipoCadastro = new Componentes.ComboBoxSimples();
            this.cbsFisicaJuridica = new Componentes.ComboBoxSimples();
            this.txbsNome = new Componentes.TextBoxSimples();
            this.txbsNomeFantasia = new Componentes.TextBoxSimples();
            this.txbsTelefone = new Componentes.TextBoxSimples();
            this.txbsCelular = new Componentes.TextBoxSimples();
            this.txbsEmailXML = new Componentes.TextBoxSimples();
            this.txbsEmailContato = new Componentes.TextBoxSimples();
            this.txbsCodigo = new Componentes.TextBoxSimples();
            this.cbsClassificacao = new Componentes.ComboBoxSimples();
            this.cbsVendedor = new Componentes.ComboBoxSimples();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbsEstado = new Componentes.ComboBoxSimples();
            this.pCabecalho.SuspendLayout();
            this.slickBlueTabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            // slickBlueTabControl1
            // 
            this.slickBlueTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.slickBlueTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.slickBlueTabControl1.Controls.Add(this.tabPage2);
            this.slickBlueTabControl1.Controls.Add(this.tabPage4);
            this.slickBlueTabControl1.Font = new System.Drawing.Font("Calibri Light", 10F);
            this.slickBlueTabControl1.ItemSize = new System.Drawing.Size(40, 130);
            this.slickBlueTabControl1.Location = new System.Drawing.Point(12, 108);
            this.slickBlueTabControl1.Multiline = true;
            this.slickBlueTabControl1.Name = "slickBlueTabControl1";
            this.slickBlueTabControl1.SelectedIndex = 0;
            this.slickBlueTabControl1.Size = new System.Drawing.Size(876, 468);
            this.slickBlueTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.slickBlueTabControl1.TabIndex = 13;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(134, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(738, 460);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Dados Principais";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.tableLayoutPanel1.Controls.Add(this.txbsTelefone, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txbsCelular, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.txbsEmailXML, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.txbsEmailContato, 7, 5);
            this.tableLayoutPanel1.Controls.Add(this.txbsCodigo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbsClassificacao, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cbsVendedor, 3, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(732, 458);
            this.tableLayoutPanel1.TabIndex = 2;
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
            this.pictureBox1.Size = new System.Drawing.Size(140, 144);
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
            this.cbsTipoCadastro.Location = new System.Drawing.Point(3, 220);
            this.cbsTipoCadastro.Name = "cbsTipoCadastro";
            this.cbsTipoCadastro.SelectedText = null;
            this.cbsTipoCadastro.SelectedValue = null;
            this.cbsTipoCadastro.Size = new System.Drawing.Size(359, 51);
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
            this.cbsFisicaJuridica.Location = new System.Drawing.Point(368, 220);
            this.cbsFisicaJuridica.Name = "cbsFisicaJuridica";
            this.cbsFisicaJuridica.SelectedText = null;
            this.cbsFisicaJuridica.SelectedValue = null;
            this.cbsFisicaJuridica.Size = new System.Drawing.Size(361, 51);
            this.cbsFisicaJuridica.TabIndex = 25;
            this.cbsFisicaJuridica.ValueMember = "IND_FISICA_JURIDICA";
            // 
            // txbsNome
            // 
            this.txbsNome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbsNome, 5);
            this.txbsNome.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsNome.Label = "NOME";
            this.txbsNome.Location = new System.Drawing.Point(4, 281);
            this.txbsNome.Margin = new System.Windows.Forms.Padding(4);
            this.txbsNome.Name = "txbsNome";
            this.txbsNome.PasswordChar = '\0';
            this.txbsNome.Size = new System.Drawing.Size(357, 42);
            this.txbsNome.TabIndex = 26;
            this.txbsNome.Value = "";
            // 
            // txbsNomeFantasia
            // 
            this.txbsNomeFantasia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbsNomeFantasia, 5);
            this.txbsNomeFantasia.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsNomeFantasia.Label = "NOME FANTASIA";
            this.txbsNomeFantasia.Location = new System.Drawing.Point(369, 281);
            this.txbsNomeFantasia.Margin = new System.Windows.Forms.Padding(4);
            this.txbsNomeFantasia.Name = "txbsNomeFantasia";
            this.txbsNomeFantasia.PasswordChar = '\0';
            this.txbsNomeFantasia.Size = new System.Drawing.Size(359, 42);
            this.txbsNomeFantasia.TabIndex = 27;
            this.txbsNomeFantasia.Value = "";
            // 
            // txbsTelefone
            // 
            this.txbsTelefone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbsTelefone, 2);
            this.txbsTelefone.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsTelefone.Label = "TELEFONE";
            this.txbsTelefone.Location = new System.Drawing.Point(4, 338);
            this.txbsTelefone.Margin = new System.Windows.Forms.Padding(4);
            this.txbsTelefone.Name = "txbsTelefone";
            this.txbsTelefone.PasswordChar = '\0';
            this.txbsTelefone.Size = new System.Drawing.Size(138, 42);
            this.txbsTelefone.TabIndex = 29;
            this.txbsTelefone.Value = "";
            // 
            // txbsCelular
            // 
            this.txbsCelular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbsCelular, 2);
            this.txbsCelular.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsCelular.Label = "CELULAR";
            this.txbsCelular.Location = new System.Drawing.Point(150, 338);
            this.txbsCelular.Margin = new System.Windows.Forms.Padding(4);
            this.txbsCelular.Name = "txbsCelular";
            this.txbsCelular.PasswordChar = '\0';
            this.txbsCelular.Size = new System.Drawing.Size(138, 42);
            this.txbsCelular.TabIndex = 30;
            this.txbsCelular.Value = "";
            // 
            // txbsEmailXML
            // 
            this.txbsEmailXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbsEmailXML, 3);
            this.txbsEmailXML.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsEmailXML.Label = "E-MAIL PARA XML";
            this.txbsEmailXML.Location = new System.Drawing.Point(296, 338);
            this.txbsEmailXML.Margin = new System.Windows.Forms.Padding(4);
            this.txbsEmailXML.Name = "txbsEmailXML";
            this.txbsEmailXML.PasswordChar = '\0';
            this.txbsEmailXML.Size = new System.Drawing.Size(211, 42);
            this.txbsEmailXML.TabIndex = 31;
            this.txbsEmailXML.Value = "";
            // 
            // txbsEmailContato
            // 
            this.txbsEmailContato.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbsEmailContato, 3);
            this.txbsEmailContato.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsEmailContato.Label = "E-MAIL PARA CONTATO";
            this.txbsEmailContato.Location = new System.Drawing.Point(515, 338);
            this.txbsEmailContato.Margin = new System.Windows.Forms.Padding(4);
            this.txbsEmailContato.Name = "txbsEmailContato";
            this.txbsEmailContato.PasswordChar = '\0';
            this.txbsEmailContato.Size = new System.Drawing.Size(213, 42);
            this.txbsEmailContato.TabIndex = 32;
            this.txbsEmailContato.Value = "";
            // 
            // txbsCodigo
            // 
            this.txbsCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txbsCodigo, 2);
            this.txbsCodigo.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsCodigo.Label = "CÓDIGO";
            this.txbsCodigo.Location = new System.Drawing.Point(4, 167);
            this.txbsCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txbsCodigo.Name = "txbsCodigo";
            this.txbsCodigo.PasswordChar = '\0';
            this.txbsCodigo.Size = new System.Drawing.Size(138, 42);
            this.txbsCodigo.TabIndex = 35;
            this.txbsCodigo.Value = "";
            // 
            // cbsClassificacao
            // 
            this.cbsClassificacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsClassificacao.BindingSource = null;
            this.tableLayoutPanel1.SetColumnSpan(this.cbsClassificacao, 3);
            this.cbsClassificacao.DisplayMember = "";
            this.cbsClassificacao.Label = "CLASSIFICAÇÃO";
            this.cbsClassificacao.Location = new System.Drawing.Point(3, 391);
            this.cbsClassificacao.Name = "cbsClassificacao";
            this.cbsClassificacao.SelectedText = null;
            this.cbsClassificacao.SelectedValue = null;
            this.cbsClassificacao.Size = new System.Drawing.Size(213, 51);
            this.cbsClassificacao.TabIndex = 36;
            this.cbsClassificacao.ValueMember = "";
            // 
            // cbsVendedor
            // 
            this.cbsVendedor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsVendedor.BindingSource = null;
            this.tableLayoutPanel1.SetColumnSpan(this.cbsVendedor, 4);
            this.cbsVendedor.DisplayMember = "";
            this.cbsVendedor.Label = "VENDEDOR";
            this.cbsVendedor.Location = new System.Drawing.Point(222, 391);
            this.cbsVendedor.Name = "cbsVendedor";
            this.cbsVendedor.SelectedText = null;
            this.cbsVendedor.SelectedValue = null;
            this.cbsVendedor.Size = new System.Drawing.Size(286, 51);
            this.cbsVendedor.TabIndex = 37;
            this.cbsVendedor.ValueMember = "";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tableLayoutPanel2);
            this.tabPage4.Location = new System.Drawing.Point(134, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(738, 460);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Endereço";
            this.tabPage4.UseVisualStyleBackColor = true;
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
            this.tableLayoutPanel2.Controls.Add(this.cbsEstado, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(732, 454);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cbsEstado
            // 
            this.cbsEstado.BindingSource = null;
            this.tableLayoutPanel2.SetColumnSpan(this.cbsEstado, 3);
            this.cbsEstado.DisplayMember = "";
            this.cbsEstado.Label = "ESTADO";
            this.cbsEstado.Location = new System.Drawing.Point(3, 3);
            this.cbsEstado.Name = "cbsEstado";
            this.cbsEstado.SelectedText = null;
            this.cbsEstado.SelectedValue = null;
            this.cbsEstado.Size = new System.Drawing.Size(213, 48);
            this.cbsEstado.TabIndex = 0;
            this.cbsEstado.ValueMember = "";
            // 
            // FCadastroCad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.slickBlueTabControl1);
            this.Name = "FCadastroCad";
            this.Text = "FCadastroCad";
            this.Load += new System.EventHandler(this.FCadastroCad_Load);
            this.Controls.SetChildIndex(this.btSalvar, 0);
            this.Controls.SetChildIndex(this.btCancelar, 0);
            this.Controls.SetChildIndex(this.btFechar, 0);
            this.Controls.SetChildIndex(this.pCabecalho, 0);
            this.Controls.SetChildIndex(this.slickBlueTabControl1, 0);
            this.pCabecalho.ResumeLayout(false);
            this.pCabecalho.PerformLayout();
            this.slickBlueTabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TabControlLateral.TabControlLateral tabControlLateral1;
        private SlickBlueTabControl slickBlueTabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Componentes.ComboBoxSimples cbsTipoCadastro;
        private Componentes.ComboBoxSimples cbsFisicaJuridica;
        private Componentes.TextBoxSimples txbsNome;
        private Componentes.TextBoxSimples txbsNomeFantasia;
        private Componentes.TextBoxSimples txbsTelefone;
        private Componentes.TextBoxSimples txbsCelular;
        private Componentes.TextBoxSimples txbsEmailXML;
        private Componentes.TextBoxSimples txbsEmailContato;
        private Componentes.TextBoxSimples txbsCodigo;
        private Componentes.ComboBoxSimples cbsClassificacao;
        private Componentes.ComboBoxSimples cbsVendedor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Componentes.ComboBoxSimples cbsEstado;
    }
}