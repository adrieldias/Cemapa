namespace Componentes
{
    partial class Busca
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Busca));
            this.txtBusca = new System.Windows.Forms.TextBox();
            this.btPesquisa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBusca
            // 
            this.txtBusca.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusca.BackColor = System.Drawing.Color.LightGray;
            this.txtBusca.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBusca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBusca.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusca.ForeColor = System.Drawing.Color.DimGray;
            this.txtBusca.Location = new System.Drawing.Point(3, 10);
            this.txtBusca.Name = "txtBusca";
            this.txtBusca.Size = new System.Drawing.Size(648, 15);
            this.txtBusca.TabIndex = 0;
            this.txtBusca.Tag = "search";
            this.txtBusca.Text = "DIGITE PARA PROCURAR";
            this.txtBusca.Enter += new System.EventHandler(this.txtBusca_Enter);
            this.txtBusca.Leave += new System.EventHandler(this.txtBusca_Leave);
            // 
            // btPesquisa
            // 
            this.btPesquisa.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btPesquisa.BackColor = System.Drawing.Color.SteelBlue;
            this.btPesquisa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btPesquisa.Image")));
            this.btPesquisa.Location = new System.Drawing.Point(657, -1);
            this.btPesquisa.Name = "btPesquisa";
            this.btPesquisa.Size = new System.Drawing.Size(32, 34);
            this.btPesquisa.TabIndex = 2;
            this.btPesquisa.UseVisualStyleBackColor = false;
            this.btPesquisa.Click += new System.EventHandler(this.btPesquisa_Click);
            // 
            // Busca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btPesquisa);
            this.Controls.Add(this.txtBusca);
            this.Name = "Busca";
            this.Size = new System.Drawing.Size(689, 32);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBusca;
        private System.Windows.Forms.Button btPesquisa;
    }
}
