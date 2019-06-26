namespace Cliente.Forms
{
    partial class FCadastroHome
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.busca1 = new Componentes.Busca();
            this.pCabecalho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbNome
            // 
            this.lbNome.Size = new System.Drawing.Size(79, 19);
            this.lbNome.Text = "CADASTRO";
            // 
            // btAlterar
            // 
            this.btAlterar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btAlterar.FlatAppearance.BorderSize = 0;
            this.btAlterar.Click += new System.EventHandler(this.btAlterar_Click);
            // 
            // btExcluir
            // 
            this.btExcluir.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btExcluir.FlatAppearance.BorderSize = 0;
            // 
            // btVisualizar
            // 
            this.btVisualizar.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btVisualizar.FlatAppearance.BorderSize = 0;
            this.btVisualizar.Click += new System.EventHandler(this.btVisualizar_Click);
            // 
            // btNovo
            // 
            this.btNovo.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btNovo.FlatAppearance.BorderSize = 0;
            this.btNovo.Click += new System.EventHandler(this.btNovo_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 151);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(829, 263);
            this.dataGridView1.TabIndex = 7;
            // 
            // busca1
            // 
            this.busca1.Location = new System.Drawing.Point(25, 106);
            this.busca1.Name = "busca1";
            this.busca1.Size = new System.Drawing.Size(691, 34);
            this.busca1.TabIndex = 8;
            // 
            // FCadastroHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 437);
            this.Controls.Add(this.busca1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "FCadastroHome";
            this.Text = "FCadastroHome";
            this.Load += new System.EventHandler(this.FCadastroHome_Load);
            this.Controls.SetChildIndex(this.btNovo, 0);
            this.Controls.SetChildIndex(this.btAlterar, 0);
            this.Controls.SetChildIndex(this.btExcluir, 0);
            this.Controls.SetChildIndex(this.btVisualizar, 0);
            this.Controls.SetChildIndex(this.pCabecalho, 0);
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            this.Controls.SetChildIndex(this.busca1, 0);
            this.pCabecalho.ResumeLayout(false);
            this.pCabecalho.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private Componentes.Busca busca1;
    }
}