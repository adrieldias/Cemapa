namespace Cliente.Forms
{
    partial class FDialogBox
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
            this.lbCaption = new System.Windows.Forms.Label();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancelar = new System.Windows.Forms.Button();
            this.lbMensagem = new System.Windows.Forms.Label();
            this.pCabecalho.SuspendLayout();
            this.SuspendLayout();
            // 
            // pCabecalho
            // 
            this.pCabecalho.BackColor = System.Drawing.Color.SteelBlue;
            this.pCabecalho.Controls.Add(this.lbCaption);
            this.pCabecalho.Dock = System.Windows.Forms.DockStyle.Top;
            this.pCabecalho.Location = new System.Drawing.Point(0, 0);
            this.pCabecalho.Name = "pCabecalho";
            this.pCabecalho.Size = new System.Drawing.Size(434, 36);
            this.pCabecalho.TabIndex = 4;
            this.pCabecalho.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pCabecalho_MouseDown);
            this.pCabecalho.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pCabecalho_MouseMove);
            this.pCabecalho.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pCabecalho_MouseUp);
            // 
            // lbCaption
            // 
            this.lbCaption.AutoSize = true;
            this.lbCaption.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCaption.ForeColor = System.Drawing.Color.White;
            this.lbCaption.Location = new System.Drawing.Point(12, 9);
            this.lbCaption.Name = "lbCaption";
            this.lbCaption.Size = new System.Drawing.Size(66, 19);
            this.lbCaption.TabIndex = 0;
            this.lbCaption.Text = "CAPTION";
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.AutoSize = true;
            this.btOk.BackColor = System.Drawing.Color.LightGray;
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btOk.FlatAppearance.BorderSize = 0;
            this.btOk.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btOk.Location = new System.Drawing.Point(348, 168);
            this.btOk.Margin = new System.Windows.Forms.Padding(1);
            this.btOk.Name = "btOk";
            this.btOk.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btOk.Size = new System.Drawing.Size(86, 42);
            this.btOk.TabIndex = 9;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = false;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancelar
            // 
            this.btCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancelar.AutoSize = true;
            this.btCancelar.BackColor = System.Drawing.Color.LightGray;
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btCancelar.FlatAppearance.BorderSize = 0;
            this.btCancelar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btCancelar.Location = new System.Drawing.Point(260, 168);
            this.btCancelar.Margin = new System.Windows.Forms.Padding(1);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btCancelar.Size = new System.Drawing.Size(86, 42);
            this.btCancelar.TabIndex = 10;
            this.btCancelar.Text = "CANCELAR";
            this.btCancelar.UseVisualStyleBackColor = false;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // lbMensagem
            // 
            this.lbMensagem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMensagem.BackColor = System.Drawing.Color.White;
            this.lbMensagem.Location = new System.Drawing.Point(-3, 37);
            this.lbMensagem.Name = "lbMensagem";
            this.lbMensagem.Size = new System.Drawing.Size(437, 130);
            this.lbMensagem.TabIndex = 11;
            this.lbMensagem.Text = "label1";
            this.lbMensagem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FDialogBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(434, 211);
            this.Controls.Add(this.lbMensagem);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.pCabecalho);
            this.Name = "FDialogBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FDialogBox";
            this.Resize += new System.EventHandler(this.FDialogBox_Resize);
            this.pCabecalho.ResumeLayout(false);
            this.pCabecalho.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel pCabecalho;
        public System.Windows.Forms.Label lbCaption;
        protected System.Windows.Forms.Button btOk;
        protected System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Label lbMensagem;
    }
}