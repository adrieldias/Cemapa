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
            this.txtBusca = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtBusca
            // 
            this.txtBusca.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusca.BackColor = System.Drawing.Color.LightGray;
            this.txtBusca.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBusca.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusca.Location = new System.Drawing.Point(0, 12);
            this.txtBusca.Name = "txtBusca";
            this.txtBusca.Size = new System.Drawing.Size(688, 15);
            this.txtBusca.TabIndex = 0;
            this.txtBusca.Text = "DIGITE PARA PROCURAR";
            this.txtBusca.TextChanged += new System.EventHandler(this.txtBusca_TextChanged);
            this.txtBusca.Enter += new System.EventHandler(this.txtBusca_Enter);
            this.txtBusca.Leave += new System.EventHandler(this.txtBusca_Leave);
            // 
            // Busca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtBusca);
            this.Name = "Busca";
            this.Size = new System.Drawing.Size(691, 34);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBusca;
    }
}
