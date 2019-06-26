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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCadastroCad));
            this.slickBlueTabControl1 = new SlickBlueTabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txbsNome = new Componentes.TextBoxSimples();
            this.txbsNomeFantasia = new Componentes.TextBoxSimples();
            this.txbsTelefone = new Componentes.TextBoxSimples();
            this.txbsCelular = new Componentes.TextBoxSimples();
            this.txbsEmailXML = new Componentes.TextBoxSimples();
            this.txbsEmailContato = new Componentes.TextBoxSimples();
            this.txbsCodigo = new Componentes.TextBoxSimples();
            this.cbsTipoCadastro = new Componentes.ComboBoxSimples();
            this.cbsFisicaJuridica = new Componentes.ComboBoxSimples();
            this.cbsClassificacao = new Componentes.ComboBoxSimples();
            this.cbsVendedor = new Componentes.ComboBoxSimples();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btExcluirEndereco = new System.Windows.Forms.Button();
            this.btAlterarEndereco = new System.Windows.Forms.Button();
            this.btNovoEndereco = new System.Windows.Forms.Button();
            this.dgvEndereco = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btExcluirPropriedade = new System.Windows.Forms.Button();
            this.btAlterarProriedade = new System.Windows.Forms.Button();
            this.btNovaPropriedade = new System.Windows.Forms.Button();
            this.dgvPropriedades = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tbsEmpresa = new Componentes.TextBoxSimples();
            this.tbsFuncao = new Componentes.TextBoxSimples();
            this.tbsTrabalhoTelefone = new Componentes.TextBoxSimples();
            this.tbsCrc = new Componentes.TextBoxSimples();
            this.tbsSequencial = new Componentes.TextBoxSimples();
            this.tbsValidadeCrc = new Componentes.TextBoxSimples();
            this.label1 = new System.Windows.Forms.Label();
            this.tbsDtFimSociedade = new Componentes.TextBoxSimples();
            this.cbsQualificacaoSocio = new Componentes.ComboBoxSimples();
            this.tbsPercParticCapitalTotal = new Componentes.TextBoxSimples();
            this.tbsPercParticCapitalVolante = new Componentes.TextBoxSimples();
            this.pCabecalho.SuspendLayout();
            this.slickBlueTabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEndereco)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropriedades)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pCabecalho
            // 
            this.pCabecalho.Size = new System.Drawing.Size(900, 52);
            // 
            // btCancelar
            // 
            this.btCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btCancelar.FlatAppearance.BorderSize = 0;
            this.btCancelar.Location = new System.Drawing.Point(89, 56);
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btSalvar
            // 
            this.btSalvar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btSalvar.FlatAppearance.BorderSize = 0;
            this.btSalvar.Location = new System.Drawing.Point(1, 56);
            this.btSalvar.Click += new System.EventHandler(this.btSalvar_Click);
            // 
            // slickBlueTabControl1
            // 
            this.slickBlueTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.slickBlueTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.slickBlueTabControl1.Controls.Add(this.tabPage2);
            this.slickBlueTabControl1.Controls.Add(this.tabPage4);
            this.slickBlueTabControl1.Controls.Add(this.tabPage1);
            this.slickBlueTabControl1.Controls.Add(this.tabPage3);
            this.slickBlueTabControl1.Font = new System.Drawing.Font("Calibri Light", 10F);
            this.slickBlueTabControl1.ItemSize = new System.Drawing.Size(40, 130);
            this.slickBlueTabControl1.Location = new System.Drawing.Point(0, 102);
            this.slickBlueTabControl1.Multiline = true;
            this.slickBlueTabControl1.Name = "slickBlueTabControl1";
            this.slickBlueTabControl1.SelectedIndex = 0;
            this.slickBlueTabControl1.Size = new System.Drawing.Size(900, 473);
            this.slickBlueTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.slickBlueTabControl1.TabIndex = 13;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(134, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(762, 465);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Dados Principais";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
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
            this.tableLayoutPanel1.Controls.Add(this.txbsNome, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txbsNomeFantasia, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.txbsTelefone, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txbsCelular, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.txbsEmailXML, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.txbsEmailContato, 7, 5);
            this.tableLayoutPanel1.Controls.Add(this.txbsCodigo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbsTipoCadastro, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbsFisicaJuridica, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbsClassificacao, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cbsVendedor, 2, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(756, 459);
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
            this.pictureBox1.Size = new System.Drawing.Size(144, 144);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // txbsNome
            // 
            this.txbsNome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbsNome.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.txbsNome, 5);
            this.txbsNome.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsNome.Label = "NOME";
            this.txbsNome.Location = new System.Drawing.Point(4, 264);
            this.txbsNome.Margin = new System.Windows.Forms.Padding(4);
            this.txbsNome.Name = "txbsNome";
            this.txbsNome.PasswordChar = '\0';
            this.txbsNome.Size = new System.Drawing.Size(367, 42);
            this.txbsNome.TabIndex = 26;
            // 
            // txbsNomeFantasia
            // 
            this.txbsNomeFantasia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbsNomeFantasia.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.txbsNomeFantasia, 5);
            this.txbsNomeFantasia.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsNomeFantasia.Label = "NOME FANTASIA";
            this.txbsNomeFantasia.Location = new System.Drawing.Point(379, 264);
            this.txbsNomeFantasia.Margin = new System.Windows.Forms.Padding(4);
            this.txbsNomeFantasia.Name = "txbsNomeFantasia";
            this.txbsNomeFantasia.PasswordChar = '\0';
            this.txbsNomeFantasia.Size = new System.Drawing.Size(373, 42);
            this.txbsNomeFantasia.TabIndex = 27;
            // 
            // txbsTelefone
            // 
            this.txbsTelefone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbsTelefone.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.txbsTelefone, 2);
            this.txbsTelefone.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsTelefone.Label = "TELEFONE";
            this.txbsTelefone.Location = new System.Drawing.Point(4, 314);
            this.txbsTelefone.Margin = new System.Windows.Forms.Padding(4);
            this.txbsTelefone.Name = "txbsTelefone";
            this.txbsTelefone.PasswordChar = '\0';
            this.txbsTelefone.Size = new System.Drawing.Size(142, 42);
            this.txbsTelefone.TabIndex = 29;
            // 
            // txbsCelular
            // 
            this.txbsCelular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbsCelular.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.txbsCelular, 2);
            this.txbsCelular.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsCelular.Label = "CELULAR";
            this.txbsCelular.Location = new System.Drawing.Point(154, 314);
            this.txbsCelular.Margin = new System.Windows.Forms.Padding(4);
            this.txbsCelular.Name = "txbsCelular";
            this.txbsCelular.PasswordChar = '\0';
            this.txbsCelular.Size = new System.Drawing.Size(142, 42);
            this.txbsCelular.TabIndex = 30;
            // 
            // txbsEmailXML
            // 
            this.txbsEmailXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbsEmailXML.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.txbsEmailXML, 3);
            this.txbsEmailXML.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsEmailXML.Label = "E-MAIL PARA XML";
            this.txbsEmailXML.Location = new System.Drawing.Point(304, 314);
            this.txbsEmailXML.Margin = new System.Windows.Forms.Padding(4);
            this.txbsEmailXML.Name = "txbsEmailXML";
            this.txbsEmailXML.PasswordChar = '\0';
            this.txbsEmailXML.Size = new System.Drawing.Size(217, 42);
            this.txbsEmailXML.TabIndex = 31;
            // 
            // txbsEmailContato
            // 
            this.txbsEmailContato.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbsEmailContato.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.txbsEmailContato, 3);
            this.txbsEmailContato.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsEmailContato.Label = "E-MAIL PARA CONTATO";
            this.txbsEmailContato.Location = new System.Drawing.Point(529, 314);
            this.txbsEmailContato.Margin = new System.Windows.Forms.Padding(4);
            this.txbsEmailContato.Name = "txbsEmailContato";
            this.txbsEmailContato.PasswordChar = '\0';
            this.txbsEmailContato.Size = new System.Drawing.Size(223, 42);
            this.txbsEmailContato.TabIndex = 32;
            // 
            // txbsCodigo
            // 
            this.txbsCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbsCodigo.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.txbsCodigo, 2);
            this.txbsCodigo.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.txbsCodigo.Label = "CÓDIGO";
            this.txbsCodigo.Location = new System.Drawing.Point(4, 164);
            this.txbsCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txbsCodigo.Name = "txbsCodigo";
            this.txbsCodigo.PasswordChar = '\0';
            this.txbsCodigo.Size = new System.Drawing.Size(142, 42);
            this.txbsCodigo.TabIndex = 35;
            // 
            // cbsTipoCadastro
            // 
            this.cbsTipoCadastro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsTipoCadastro.AutoSize = true;
            this.cbsTipoCadastro.BindingSource = null;
            this.tableLayoutPanel1.SetColumnSpan(this.cbsTipoCadastro, 5);
            this.cbsTipoCadastro.DisplayMember = "";
            this.cbsTipoCadastro.Label = "TIPO";
            this.cbsTipoCadastro.Location = new System.Drawing.Point(3, 213);
            this.cbsTipoCadastro.Name = "cbsTipoCadastro";
            this.cbsTipoCadastro.SelectedIndex = -1;
            this.cbsTipoCadastro.SelectedText = null;
            this.cbsTipoCadastro.SelectedValue = null;
            this.cbsTipoCadastro.Size = new System.Drawing.Size(369, 44);
            this.cbsTipoCadastro.TabIndex = 38;
            this.cbsTipoCadastro.ValueMember = "";
            // 
            // cbsFisicaJuridica
            // 
            this.cbsFisicaJuridica.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsFisicaJuridica.AutoSize = true;
            this.cbsFisicaJuridica.BindingSource = null;
            this.tableLayoutPanel1.SetColumnSpan(this.cbsFisicaJuridica, 5);
            this.cbsFisicaJuridica.DisplayMember = "";
            this.cbsFisicaJuridica.Label = "PESSOA FÍSICA/JURÍDICA";
            this.cbsFisicaJuridica.Location = new System.Drawing.Point(378, 213);
            this.cbsFisicaJuridica.Name = "cbsFisicaJuridica";
            this.cbsFisicaJuridica.SelectedIndex = -1;
            this.cbsFisicaJuridica.SelectedText = null;
            this.cbsFisicaJuridica.SelectedValue = null;
            this.cbsFisicaJuridica.Size = new System.Drawing.Size(375, 44);
            this.cbsFisicaJuridica.TabIndex = 39;
            this.cbsFisicaJuridica.ValueMember = "";
            // 
            // cbsClassificacao
            // 
            this.cbsClassificacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsClassificacao.AutoSize = true;
            this.cbsClassificacao.BindingSource = null;
            this.tableLayoutPanel1.SetColumnSpan(this.cbsClassificacao, 2);
            this.cbsClassificacao.DisplayMember = "";
            this.cbsClassificacao.Label = "CLASSIFICAÇÃO";
            this.cbsClassificacao.Location = new System.Drawing.Point(3, 363);
            this.cbsClassificacao.Name = "cbsClassificacao";
            this.cbsClassificacao.SelectedIndex = -1;
            this.cbsClassificacao.SelectedText = null;
            this.cbsClassificacao.SelectedValue = null;
            this.cbsClassificacao.Size = new System.Drawing.Size(144, 44);
            this.cbsClassificacao.TabIndex = 40;
            this.cbsClassificacao.ValueMember = "";
            // 
            // cbsVendedor
            // 
            this.cbsVendedor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsVendedor.AutoSize = true;
            this.cbsVendedor.BindingSource = null;
            this.tableLayoutPanel1.SetColumnSpan(this.cbsVendedor, 5);
            this.cbsVendedor.DisplayMember = "";
            this.cbsVendedor.Label = "VENDEDOR";
            this.cbsVendedor.Location = new System.Drawing.Point(153, 363);
            this.cbsVendedor.Name = "cbsVendedor";
            this.cbsVendedor.SelectedIndex = -1;
            this.cbsVendedor.SelectedText = null;
            this.cbsVendedor.SelectedValue = null;
            this.cbsVendedor.Size = new System.Drawing.Size(369, 44);
            this.cbsVendedor.TabIndex = 41;
            this.cbsVendedor.ValueMember = "";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btExcluirEndereco);
            this.tabPage4.Controls.Add(this.btAlterarEndereco);
            this.tabPage4.Controls.Add(this.btNovoEndereco);
            this.tabPage4.Controls.Add(this.dgvEndereco);
            this.tabPage4.Location = new System.Drawing.Point(134, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(762, 465);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Endereço";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btExcluirEndereco
            // 
            this.btExcluirEndereco.Location = new System.Drawing.Point(190, 6);
            this.btExcluirEndereco.Name = "btExcluirEndereco";
            this.btExcluirEndereco.Size = new System.Drawing.Size(86, 42);
            this.btExcluirEndereco.TabIndex = 19;
            this.btExcluirEndereco.Text = "Excluir";
            this.btExcluirEndereco.UseVisualStyleBackColor = true;
            // 
            // btAlterarEndereco
            // 
            this.btAlterarEndereco.Location = new System.Drawing.Point(98, 6);
            this.btAlterarEndereco.Name = "btAlterarEndereco";
            this.btAlterarEndereco.Size = new System.Drawing.Size(86, 42);
            this.btAlterarEndereco.TabIndex = 18;
            this.btAlterarEndereco.Text = "Alterar";
            this.btAlterarEndereco.UseVisualStyleBackColor = true;
            this.btAlterarEndereco.Click += new System.EventHandler(this.btAlterarEndereco_Click);
            // 
            // btNovoEndereco
            // 
            this.btNovoEndereco.Location = new System.Drawing.Point(6, 6);
            this.btNovoEndereco.Name = "btNovoEndereco";
            this.btNovoEndereco.Size = new System.Drawing.Size(86, 42);
            this.btNovoEndereco.TabIndex = 17;
            this.btNovoEndereco.Text = "Novo";
            this.btNovoEndereco.UseVisualStyleBackColor = true;
            // 
            // dgvEndereco
            // 
            this.dgvEndereco.AllowUserToAddRows = false;
            this.dgvEndereco.AllowUserToDeleteRows = false;
            this.dgvEndereco.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEndereco.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEndereco.Location = new System.Drawing.Point(6, 54);
            this.dgvEndereco.Name = "dgvEndereco";
            this.dgvEndereco.Size = new System.Drawing.Size(748, 408);
            this.dgvEndereco.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btExcluirPropriedade);
            this.tabPage1.Controls.Add(this.btAlterarProriedade);
            this.tabPage1.Controls.Add(this.btNovaPropriedade);
            this.tabPage1.Controls.Add(this.dgvPropriedades);
            this.tabPage1.Location = new System.Drawing.Point(134, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(762, 465);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Propriedade";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btExcluirPropriedade
            // 
            this.btExcluirPropriedade.Location = new System.Drawing.Point(190, 6);
            this.btExcluirPropriedade.Name = "btExcluirPropriedade";
            this.btExcluirPropriedade.Size = new System.Drawing.Size(86, 42);
            this.btExcluirPropriedade.TabIndex = 16;
            this.btExcluirPropriedade.Text = "Excluir";
            this.btExcluirPropriedade.UseVisualStyleBackColor = true;
            // 
            // btAlterarProriedade
            // 
            this.btAlterarProriedade.Location = new System.Drawing.Point(98, 6);
            this.btAlterarProriedade.Name = "btAlterarProriedade";
            this.btAlterarProriedade.Size = new System.Drawing.Size(86, 42);
            this.btAlterarProriedade.TabIndex = 15;
            this.btAlterarProriedade.Text = "Alterar";
            this.btAlterarProriedade.UseVisualStyleBackColor = true;
            this.btAlterarProriedade.Click += new System.EventHandler(this.btAlterarProriedade_Click);
            // 
            // btNovaPropriedade
            // 
            this.btNovaPropriedade.Location = new System.Drawing.Point(6, 6);
            this.btNovaPropriedade.Name = "btNovaPropriedade";
            this.btNovaPropriedade.Size = new System.Drawing.Size(86, 42);
            this.btNovaPropriedade.TabIndex = 14;
            this.btNovaPropriedade.Text = "Novo";
            this.btNovaPropriedade.UseVisualStyleBackColor = true;
            this.btNovaPropriedade.Click += new System.EventHandler(this.btNovaPropriedade_Click);
            // 
            // dgvPropriedades
            // 
            this.dgvPropriedades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPropriedades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPropriedades.Location = new System.Drawing.Point(6, 54);
            this.dgvPropriedades.Name = "dgvPropriedades";
            this.dgvPropriedades.Size = new System.Drawing.Size(748, 408);
            this.dgvPropriedades.TabIndex = 13;
            this.dgvPropriedades.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPropriedades_CellFormatting);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel4);
            this.tabPage3.Location = new System.Drawing.Point(134, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(762, 465);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Trabalho/Aut";
            this.tabPage3.ToolTipText = "Trabalho/Autorização";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 10;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.Controls.Add(this.tbsEmpresa, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tbsFuncao, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.tbsTrabalhoTelefone, 7, 0);
            this.tableLayoutPanel4.Controls.Add(this.tbsCrc, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tbsSequencial, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.tbsValidadeCrc, 7, 1);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.tbsDtFimSociedade, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.cbsQualificacaoSocio, 3, 3);
            this.tableLayoutPanel4.Controls.Add(this.tbsPercParticCapitalTotal, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.tbsPercParticCapitalVolante, 3, 4);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 6;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(756, 459);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tbsEmpresa
            // 
            this.tbsEmpresa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsEmpresa.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.tbsEmpresa, 4);
            this.tbsEmpresa.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsEmpresa.Label = "EMPRESA";
            this.tbsEmpresa.Location = new System.Drawing.Point(3, 3);
            this.tbsEmpresa.Name = "tbsEmpresa";
            this.tbsEmpresa.PasswordChar = '\0';
            this.tbsEmpresa.Size = new System.Drawing.Size(294, 44);
            this.tbsEmpresa.TabIndex = 0;
            // 
            // tbsFuncao
            // 
            this.tbsFuncao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsFuncao.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.tbsFuncao, 3);
            this.tbsFuncao.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsFuncao.Label = "FUNÇÃO";
            this.tbsFuncao.Location = new System.Drawing.Point(303, 3);
            this.tbsFuncao.Name = "tbsFuncao";
            this.tbsFuncao.PasswordChar = '\0';
            this.tbsFuncao.Size = new System.Drawing.Size(219, 44);
            this.tbsFuncao.TabIndex = 1;
            // 
            // tbsTrabalhoTelefone
            // 
            this.tbsTrabalhoTelefone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsTrabalhoTelefone.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.tbsTrabalhoTelefone, 3);
            this.tbsTrabalhoTelefone.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsTrabalhoTelefone.Label = "TELEFONE";
            this.tbsTrabalhoTelefone.Location = new System.Drawing.Point(528, 3);
            this.tbsTrabalhoTelefone.Name = "tbsTrabalhoTelefone";
            this.tbsTrabalhoTelefone.PasswordChar = '\0';
            this.tbsTrabalhoTelefone.Size = new System.Drawing.Size(225, 44);
            this.tbsTrabalhoTelefone.TabIndex = 2;
            // 
            // tbsCrc
            // 
            this.tbsCrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsCrc.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.tbsCrc, 3);
            this.tbsCrc.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsCrc.Label = "CRC CONTABILISTA";
            this.tbsCrc.Location = new System.Drawing.Point(3, 53);
            this.tbsCrc.Name = "tbsCrc";
            this.tbsCrc.PasswordChar = '\0';
            this.tbsCrc.Size = new System.Drawing.Size(219, 44);
            this.tbsCrc.TabIndex = 3;
            // 
            // tbsSequencial
            // 
            this.tbsSequencial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsSequencial.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.tbsSequencial, 4);
            this.tbsSequencial.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsSequencial.Label = "SEQUENCIAL UF/ANO/NÚMERO";
            this.tbsSequencial.Location = new System.Drawing.Point(228, 53);
            this.tbsSequencial.Name = "tbsSequencial";
            this.tbsSequencial.PasswordChar = '\0';
            this.tbsSequencial.Size = new System.Drawing.Size(294, 44);
            this.tbsSequencial.TabIndex = 4;
            // 
            // tbsValidadeCrc
            // 
            this.tbsValidadeCrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsValidadeCrc.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.tbsValidadeCrc, 3);
            this.tbsValidadeCrc.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsValidadeCrc.Label = "VALIDADE CRC";
            this.tbsValidadeCrc.Location = new System.Drawing.Point(528, 53);
            this.tbsValidadeCrc.Name = "tbsValidadeCrc";
            this.tbsValidadeCrc.PasswordChar = '\0';
            this.tbsValidadeCrc.Size = new System.Drawing.Size(225, 44);
            this.tbsValidadeCrc.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.label1, 3);
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(3, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "INFORMAÇÕES PARA SPED ECF";
            // 
            // tbsDtFimSociedade
            // 
            this.tbsDtFimSociedade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsDtFimSociedade.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.tbsDtFimSociedade, 3);
            this.tbsDtFimSociedade.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsDtFimSociedade.Label = "DATA FIM SOCIEDADE";
            this.tbsDtFimSociedade.Location = new System.Drawing.Point(3, 133);
            this.tbsDtFimSociedade.Name = "tbsDtFimSociedade";
            this.tbsDtFimSociedade.PasswordChar = '\0';
            this.tbsDtFimSociedade.Size = new System.Drawing.Size(219, 44);
            this.tbsDtFimSociedade.TabIndex = 7;
            // 
            // cbsQualificacaoSocio
            // 
            this.cbsQualificacaoSocio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbsQualificacaoSocio.AutoSize = true;
            this.cbsQualificacaoSocio.BindingSource = null;
            this.tableLayoutPanel4.SetColumnSpan(this.cbsQualificacaoSocio, 7);
            this.cbsQualificacaoSocio.DisplayMember = "";
            this.cbsQualificacaoSocio.Label = "QUALIFICAÇAO DO SÓCIO OU TITULAR";
            this.cbsQualificacaoSocio.Location = new System.Drawing.Point(228, 133);
            this.cbsQualificacaoSocio.Name = "cbsQualificacaoSocio";
            this.cbsQualificacaoSocio.SelectedIndex = -1;
            this.cbsQualificacaoSocio.SelectedText = null;
            this.cbsQualificacaoSocio.SelectedValue = null;
            this.cbsQualificacaoSocio.Size = new System.Drawing.Size(525, 44);
            this.cbsQualificacaoSocio.TabIndex = 8;
            this.cbsQualificacaoSocio.ValueMember = "";
            // 
            // tbsPercParticCapitalTotal
            // 
            this.tbsPercParticCapitalTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsPercParticCapitalTotal.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.tbsPercParticCapitalTotal, 3);
            this.tbsPercParticCapitalTotal.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsPercParticCapitalTotal.Label = "% PARTICIPAÇÃO CAPITAL TOTAL";
            this.tbsPercParticCapitalTotal.Location = new System.Drawing.Point(3, 183);
            this.tbsPercParticCapitalTotal.Name = "tbsPercParticCapitalTotal";
            this.tbsPercParticCapitalTotal.PasswordChar = '\0';
            this.tbsPercParticCapitalTotal.Size = new System.Drawing.Size(219, 44);
            this.tbsPercParticCapitalTotal.TabIndex = 9;
            // 
            // tbsPercParticCapitalVolante
            // 
            this.tbsPercParticCapitalVolante.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbsPercParticCapitalVolante.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.tbsPercParticCapitalVolante, 3);
            this.tbsPercParticCapitalVolante.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.tbsPercParticCapitalVolante.Label = "% PARTICIPAÇÃO CAPITAL VOLANTE";
            this.tbsPercParticCapitalVolante.Location = new System.Drawing.Point(228, 183);
            this.tbsPercParticCapitalVolante.Name = "tbsPercParticCapitalVolante";
            this.tbsPercParticCapitalVolante.PasswordChar = '\0';
            this.tbsPercParticCapitalVolante.Size = new System.Drawing.Size(219, 44);
            this.tbsPercParticCapitalVolante.TabIndex = 10;
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
            this.Controls.SetChildIndex(this.pCabecalho, 0);
            this.Controls.SetChildIndex(this.slickBlueTabControl1, 0);
            this.pCabecalho.ResumeLayout(false);
            this.pCabecalho.PerformLayout();
            this.slickBlueTabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEndereco)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropriedades)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private SlickBlueTabControl slickBlueTabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Componentes.TextBoxSimples txbsNome;
        private Componentes.TextBoxSimples txbsNomeFantasia;
        private Componentes.TextBoxSimples txbsTelefone;
        private Componentes.TextBoxSimples txbsCelular;
        private Componentes.TextBoxSimples txbsEmailXML;
        private Componentes.TextBoxSimples txbsEmailContato;
        private Componentes.TextBoxSimples txbsCodigo;
        private Componentes.ComboBoxSimples cbsTipoCadastro;
        private Componentes.ComboBoxSimples cbsFisicaJuridica;
        private Componentes.ComboBoxSimples cbsClassificacao;
        private Componentes.ComboBoxSimples cbsVendedor;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Componentes.TextBoxSimples tbsEmpresa;
        private Componentes.TextBoxSimples tbsFuncao;
        private Componentes.TextBoxSimples tbsTrabalhoTelefone;
        private Componentes.TextBoxSimples tbsCrc;
        private Componentes.TextBoxSimples tbsSequencial;
        private Componentes.TextBoxSimples tbsValidadeCrc;
        private System.Windows.Forms.Label label1;
        private Componentes.TextBoxSimples tbsDtFimSociedade;
        private Componentes.ComboBoxSimples cbsQualificacaoSocio;
        private Componentes.TextBoxSimples tbsPercParticCapitalTotal;
        private Componentes.TextBoxSimples tbsPercParticCapitalVolante;
        private System.Windows.Forms.DataGridView dgvPropriedades;
        private System.Windows.Forms.Button btNovaPropriedade;
        private System.Windows.Forms.Button btExcluirPropriedade;
        private System.Windows.Forms.Button btAlterarProriedade;
        private System.Windows.Forms.DataGridView dgvEndereco;
        private System.Windows.Forms.Button btExcluirEndereco;
        private System.Windows.Forms.Button btAlterarEndereco;
        private System.Windows.Forms.Button btNovoEndereco;
    }
}