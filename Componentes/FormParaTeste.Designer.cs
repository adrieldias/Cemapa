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
            this.slickBlueTabControl1 = new SlickBlueTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxSimples2 = new Componentes.TextBoxSimples();
            this.textBoxSimples1 = new Componentes.TextBoxSimples();
            this.comboBoxSimples1 = new Componentes.ComboBoxSimples();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.slickBlueTabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.textBoxSimples1);
            this.panel1.Controls.Add(this.comboBoxSimples1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 160);
            this.panel1.TabIndex = 1;
            // 
            // slickBlueTabControl1
            // 
            this.slickBlueTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.slickBlueTabControl1.Controls.Add(this.tabPage1);
            this.slickBlueTabControl1.Controls.Add(this.tabPage2);
            this.slickBlueTabControl1.Controls.Add(this.tabPage3);
            this.slickBlueTabControl1.Controls.Add(this.tabPage4);
            this.slickBlueTabControl1.Font = new System.Drawing.Font("Segoe UI Semilight", 9F);
            this.slickBlueTabControl1.ItemSize = new System.Drawing.Size(40, 130);
            this.slickBlueTabControl1.Location = new System.Drawing.Point(13, 179);
            this.slickBlueTabControl1.Multiline = true;
            this.slickBlueTabControl1.Name = "slickBlueTabControl1";
            this.slickBlueTabControl1.SelectedIndex = 0;
            this.slickBlueTabControl1.Size = new System.Drawing.Size(554, 322);
            this.slickBlueTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.slickBlueTabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(134, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(377, 314);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxSimples2);
            this.tabPage2.Location = new System.Drawing.Point(134, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(377, 314);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxSimples2
            // 
            this.textBoxSimples2.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.textBoxSimples2.Label = "NOME DO CAMPO";
            this.textBoxSimples2.Location = new System.Drawing.Point(7, 7);
            this.textBoxSimples2.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSimples2.Name = "textBoxSimples2";
            this.textBoxSimples2.PasswordChar = '\0';
            this.textBoxSimples2.Size = new System.Drawing.Size(250, 49);
            this.textBoxSimples2.TabIndex = 0;
            this.textBoxSimples2.Text = "";
            // 
            // textBoxSimples1
            // 
            this.textBoxSimples1.Font = new System.Drawing.Font("Calibri Light", 12F);
            this.textBoxSimples1.Label = "NOME DO CAMPO";
            this.textBoxSimples1.Location = new System.Drawing.Point(95, 105);
            this.textBoxSimples1.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSimples1.Name = "textBoxSimples1";
            this.textBoxSimples1.PasswordChar = '\0';
            this.textBoxSimples1.Size = new System.Drawing.Size(401, 49);
            this.textBoxSimples1.TabIndex = 1;
            this.textBoxSimples1.Text = "";
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
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(134, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(377, 314);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(134, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(416, 314);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "teste";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // FormParaTeste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 513);
            this.Controls.Add(this.slickBlueTabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "FormParaTeste";
            this.Text = "FormParaTeste";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.slickBlueTabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ComboBoxSimples comboBoxSimples1;
        private TextBoxSimples textBoxSimples1;
        private SlickBlueTabControl slickBlueTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private TextBoxSimples textBoxSimples2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
    }
}