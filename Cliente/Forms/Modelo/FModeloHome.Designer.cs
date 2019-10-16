namespace Cliente.Forms.Modelo
{
    partial class FModeloHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FModeloHome));
            this.pCabecalho = new System.Windows.Forms.Panel();
            this.btMinimize = new System.Windows.Forms.Button();
            this.btCloseForm = new System.Windows.Forms.Button();
            this.lbNome = new System.Windows.Forms.Label();
            this.btNovo = new System.Windows.Forms.Button();
            this.btAlterar = new System.Windows.Forms.Button();
            this.btExcluir = new System.Windows.Forms.Button();
            this.btVisualizar = new System.Windows.Forms.Button();
            this.pCabecalho.SuspendLayout();
            this.SuspendLayout();
            // 
            // pCabecalho
            // 
            this.pCabecalho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pCabecalho.BackColor = System.Drawing.Color.SteelBlue;
            this.pCabecalho.Controls.Add(this.btMinimize);
            this.pCabecalho.Controls.Add(this.btCloseForm);
            this.pCabecalho.Controls.Add(this.lbNome);
            this.pCabecalho.Location = new System.Drawing.Point(0, 0);
            this.pCabecalho.Name = "pCabecalho";
            this.pCabecalho.Size = new System.Drawing.Size(830, 52);
            this.pCabecalho.TabIndex = 2;
            this.pCabecalho.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pCabecalho_MouseDown);
            this.pCabecalho.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pCabecalho_MouseMove);
            this.pCabecalho.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pCabecalho_MouseUp);
            // 
            // btMinimize
            // 
            this.btMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btMinimize.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.btMinimize.FlatAppearance.BorderSize = 0;
            this.btMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btMinimize.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btMinimize.ForeColor = System.Drawing.Color.White;
            this.btMinimize.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btMinimize.Location = new System.Drawing.Point(724, 0);
            this.btMinimize.Name = "btMinimize";
            this.btMinimize.Size = new System.Drawing.Size(53, 52);
            this.btMinimize.TabIndex = 4;
            this.btMinimize.Text = "_";
            this.btMinimize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btMinimize.UseVisualStyleBackColor = true;
            this.btMinimize.Click += new System.EventHandler(this.btMinimize_Click);
            // 
            // btCloseForm
            // 
            this.btCloseForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btCloseForm.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.btCloseForm.FlatAppearance.BorderSize = 0;
            this.btCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCloseForm.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCloseForm.ForeColor = System.Drawing.Color.White;
            this.btCloseForm.Location = new System.Drawing.Point(777, 0);
            this.btCloseForm.Name = "btCloseForm";
            this.btCloseForm.Size = new System.Drawing.Size(53, 52);
            this.btCloseForm.TabIndex = 3;
            this.btCloseForm.Text = "X";
            this.btCloseForm.UseVisualStyleBackColor = true;
            this.btCloseForm.Click += new System.EventHandler(this.btCloseForm_Click);
            // 
            // lbNome
            // 
            this.lbNome.AutoSize = true;
            this.lbNome.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNome.ForeColor = System.Drawing.Color.White;
            this.lbNome.Location = new System.Drawing.Point(12, 18);
            this.lbNome.Name = "lbNome";
            this.lbNome.Size = new System.Drawing.Size(140, 19);
            this.lbNome.TabIndex = 0;
            this.lbNome.Text = "Nome do Formulário";
            // 
            // btNovo
            // 
            this.btNovo.AutoSize = true;
            this.btNovo.BackColor = System.Drawing.Color.LightGray;
            this.btNovo.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btNovo.FlatAppearance.BorderSize = 0;
            this.btNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btNovo.ForeColor = System.Drawing.Color.SteelBlue;
            this.btNovo.Image = ((System.Drawing.Image)(resources.GetObject("btNovo.Image")));
            this.btNovo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btNovo.Location = new System.Drawing.Point(8, 58);
            this.btNovo.Name = "btNovo";
            this.btNovo.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btNovo.Size = new System.Drawing.Size(74, 42);
            this.btNovo.TabIndex = 3;
            this.btNovo.Text = "Novo";
            this.btNovo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btNovo.UseVisualStyleBackColor = false;
            // 
            // btAlterar
            // 
            this.btAlterar.AutoSize = true;
            this.btAlterar.BackColor = System.Drawing.Color.LightGray;
            this.btAlterar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btAlterar.FlatAppearance.BorderSize = 0;
            this.btAlterar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAlterar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btAlterar.Image = ((System.Drawing.Image)(resources.GetObject("btAlterar.Image")));
            this.btAlterar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAlterar.Location = new System.Drawing.Point(88, 58);
            this.btAlterar.Name = "btAlterar";
            this.btAlterar.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btAlterar.Size = new System.Drawing.Size(84, 42);
            this.btAlterar.TabIndex = 4;
            this.btAlterar.Text = "Alterar";
            this.btAlterar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btAlterar.UseVisualStyleBackColor = false;
            // 
            // btExcluir
            // 
            this.btExcluir.AutoSize = true;
            this.btExcluir.BackColor = System.Drawing.Color.LightGray;
            this.btExcluir.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btExcluir.FlatAppearance.BorderSize = 0;
            this.btExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExcluir.ForeColor = System.Drawing.Color.SteelBlue;
            this.btExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btExcluir.Image")));
            this.btExcluir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btExcluir.Location = new System.Drawing.Point(289, 58);
            this.btExcluir.Name = "btExcluir";
            this.btExcluir.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btExcluir.Size = new System.Drawing.Size(86, 42);
            this.btExcluir.TabIndex = 5;
            this.btExcluir.Text = "Excluir";
            this.btExcluir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btExcluir.UseVisualStyleBackColor = false;
            // 
            // btVisualizar
            // 
            this.btVisualizar.AutoSize = true;
            this.btVisualizar.BackColor = System.Drawing.Color.LightGray;
            this.btVisualizar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btVisualizar.FlatAppearance.BorderSize = 0;
            this.btVisualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btVisualizar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btVisualizar.Image = ((System.Drawing.Image)(resources.GetObject("btVisualizar.Image")));
            this.btVisualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btVisualizar.Location = new System.Drawing.Point(178, 58);
            this.btVisualizar.Name = "btVisualizar";
            this.btVisualizar.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btVisualizar.Size = new System.Drawing.Size(105, 42);
            this.btVisualizar.TabIndex = 6;
            this.btVisualizar.Text = "Visualizar";
            this.btVisualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btVisualizar.UseVisualStyleBackColor = false;
            // 
            // FModeloHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(830, 437);
            this.Controls.Add(this.btVisualizar);
            this.Controls.Add(this.btExcluir);
            this.Controls.Add(this.btAlterar);
            this.Controls.Add(this.btNovo);
            this.Controls.Add(this.pCabecalho);
            this.Name = "FModeloHome";
            this.Text = "FModeloHome";
            this.Activated += new System.EventHandler(this.FModeloHome_Activated);
            this.Deactivate += new System.EventHandler(this.FModeloHome_Deactivate);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FModeloHome_Paint);
            this.Resize += new System.EventHandler(this.FModeloHome_Resize);
            this.pCabecalho.ResumeLayout(false);
            this.pCabecalho.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btMinimize;
        private System.Windows.Forms.Button btCloseForm;
        public System.Windows.Forms.Label lbNome;
        public System.Windows.Forms.Panel pCabecalho;
        protected System.Windows.Forms.Button btAlterar;
        protected System.Windows.Forms.Button btExcluir;
        protected System.Windows.Forms.Button btVisualizar;
        protected System.Windows.Forms.Button btNovo;
    }
}