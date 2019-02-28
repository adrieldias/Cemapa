namespace Componentes
{
    partial class FormParaTeste
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
            this.comboBoxSimples1 = new Componentes.ComboBoxSimples();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.comboBoxSimples1);
            this.panel1.Location = new System.Drawing.Point(62, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 100);
            this.panel1.TabIndex = 1;
            // 
            // comboBoxSimples1
            // 
            this.comboBoxSimples1.AutoSize = true;
            this.comboBoxSimples1.BindingSource = null;
            this.comboBoxSimples1.DisplayMember = "";
            this.comboBoxSimples1.Label = "NOME DO CAMPO";
            this.comboBoxSimples1.Location = new System.Drawing.Point(95, 33);
            this.comboBoxSimples1.Name = "comboBoxSimples1";
            this.comboBoxSimples1.SelectedText = null;
            this.comboBoxSimples1.SelectedValue = null;
            this.comboBoxSimples1.Size = new System.Drawing.Size(401, 49);
            this.comboBoxSimples1.TabIndex = 0;
            this.comboBoxSimples1.ValueMember = "";
            this.comboBoxSimples1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.comboBoxSimples1_MouseUp);
            // 
            // FormParaTeste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FormParaTeste";
            this.Text = "FormParaTeste";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ComboBoxSimples comboBoxSimples1;
    }
}