namespace virConnectOpenAuth
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbURI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.gbDomains = new System.Windows.Forms.GroupBox();
            this.lbDomains = new System.Windows.Forms.ListBox();
            this.gbDomains.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbURI
            // 
            this.tbURI.Location = new System.Drawing.Point(77, 12);
            this.tbURI.Name = "tbURI";
            this.tbURI.Size = new System.Drawing.Size(195, 20);
            this.tbURI.TabIndex = 0;
            this.tbURI.Text = "esx://192.168.220.194?transport=http";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "URI :";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(77, 38);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(195, 20);
            this.tbUsername.TabIndex = 2;
            this.tbUsername.Text = "root";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Username :";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(77, 64);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(195, 20);
            this.tbPassword.TabIndex = 4;
            this.tbPassword.Text = "nmrojvbnpG2B";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password :";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(197, 90);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // gbDomains
            // 
            this.gbDomains.Controls.Add(this.lbDomains);
            this.gbDomains.Location = new System.Drawing.Point(15, 119);
            this.gbDomains.Name = "gbDomains";
            this.gbDomains.Size = new System.Drawing.Size(257, 131);
            this.gbDomains.TabIndex = 7;
            this.gbDomains.TabStop = false;
            this.gbDomains.Text = "Domains";
            // 
            // lbDomains
            // 
            this.lbDomains.FormattingEnabled = true;
            this.lbDomains.Location = new System.Drawing.Point(6, 19);
            this.lbDomains.Name = "lbDomains";
            this.lbDomains.Size = new System.Drawing.Size(245, 95);
            this.lbDomains.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.gbDomains);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbURI);
            this.Name = "Form1";
            this.Text = "virConnectOpenAuth sample";
            this.gbDomains.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbURI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox gbDomains;
        private System.Windows.Forms.ListBox lbDomains;
    }
}

