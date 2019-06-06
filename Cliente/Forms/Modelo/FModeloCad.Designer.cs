namespace Cliente.Forms.Modelo
{
    partial class FModeloCad
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
            this.pCabecalho = new System.Windows.Forms.Panel();
            this.btMinimize = new System.Windows.Forms.Button();
            this.btCloseForm = new System.Windows.Forms.Button();
            this.lbNome = new System.Windows.Forms.Label();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btSalvar = new System.Windows.Forms.Button();
            this.btFechar = new System.Windows.Forms.Button();
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
            this.pCabecalho.TabIndex = 3;
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
            this.btMinimize.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btMinimize.ForeColor = System.Drawing.Color.White;
            this.btMinimize.Location = new System.Drawing.Point(724, 0);
            this.btMinimize.Name = "btMinimize";
            this.btMinimize.Size = new System.Drawing.Size(53, 52);
            this.btMinimize.TabIndex = 9;
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
            this.btCloseForm.TabIndex = 8;
            this.btCloseForm.Text = "X";
            this.btCloseForm.UseVisualStyleBackColor = true;
            this.btCloseForm.Click += new System.EventHandler(this.btCloseForm_Click);
            // 
            // lbNome
            // 
            this.lbNome.AutoSize = true;
            this.lbNome.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNome.ForeColor = System.Drawing.Color.White;
            this.lbNome.Location = new System.Drawing.Point(12, 18);
            this.lbNome.Name = "lbNome";
            this.lbNome.Size = new System.Drawing.Size(168, 19);
            this.lbNome.TabIndex = 0;
            this.lbNome.Text = "NOME DO FORMULÁRIO";
            // 
            // btCancelar
            // 
            this.btCancelar.AutoSize = true;
            this.btCancelar.BackColor = System.Drawing.Color.LightGray;
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btCancelar.FlatAppearance.BorderSize = 0;
            this.btCancelar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btCancelar.Location = new System.Drawing.Point(88, 56);
            this.btCancelar.Margin = new System.Windows.Forms.Padding(1);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btCancelar.Size = new System.Drawing.Size(86, 42);
            this.btCancelar.TabIndex = 9;
            this.btCancelar.Text = "CANCELAR";
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btSalvar
            // 
            this.btSalvar.AutoSize = true;
            this.btSalvar.BackColor = System.Drawing.Color.LightGray;
            this.btSalvar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btSalvar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btSalvar.FlatAppearance.BorderSize = 0;
            this.btSalvar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btSalvar.Location = new System.Drawing.Point(0, 56);
            this.btSalvar.Margin = new System.Windows.Forms.Padding(1);
            this.btSalvar.Name = "btSalvar";
            this.btSalvar.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btSalvar.Size = new System.Drawing.Size(86, 42);
            this.btSalvar.TabIndex = 8;
            this.btSalvar.Text = "SALVAR";
            this.btSalvar.UseVisualStyleBackColor = true;
            this.btSalvar.Click += new System.EventHandler(this.btSalvar_Click);
            // 
            // btFechar
            // 
            this.btFechar.Location = new System.Drawing.Point(178, 56);
            this.btFechar.Name = "btFechar";
            this.btFechar.Size = new System.Drawing.Size(86, 42);
            this.btFechar.TabIndex = 10;
            this.btFechar.Text = "FECHAR";
            this.btFechar.UseVisualStyleBackColor = true;
            this.btFechar.Click += new System.EventHandler(this.btFechar_Click);
            // 
            // FModeloCad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(830, 376);
            this.Controls.Add(this.btFechar);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.btSalvar);
            this.Controls.Add(this.pCabecalho);
            this.Name = "FModeloCad";
            this.Text = "FModeloCad";
            this.Activated += new System.EventHandler(this.FModeloCad_Activated);
            this.Deactivate += new System.EventHandler(this.FModeloCad_Deactivate);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FModeloCad_Paint);
            this.Resize += new System.EventHandler(this.FModeloCad_Resize);
            this.pCabecalho.ResumeLayout(false);
            this.pCabecalho.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel pCabecalho;
        public System.Windows.Forms.Label lbNome;
        private System.Windows.Forms.Button btCloseForm;
        private System.Windows.Forms.Button btMinimize;
        protected System.Windows.Forms.Button btCancelar;
        protected System.Windows.Forms.Button btSalvar;
        private System.Windows.Forms.Button btFechar;
    }
}