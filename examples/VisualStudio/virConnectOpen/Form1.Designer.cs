namespace virConnectOpen
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
            this.gbDomains = new System.Windows.Forms.GroupBox();
            this.lbStoragePool = new System.Windows.Forms.ListBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbURI = new System.Windows.Forms.TextBox();
            this.gbDomains.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDomains
            // 
            this.gbDomains.Controls.Add(this.lbStoragePool);
            this.gbDomains.Location = new System.Drawing.Point(15, 67);
            this.gbDomains.Name = "gbDomains";
            this.gbDomains.Size = new System.Drawing.Size(257, 131);
            this.gbDomains.TabIndex = 15;
            this.gbDomains.TabStop = false;
            this.gbDomains.Text = "Storage Pools";
            // 
            // lbStoragePool
            // 
            this.lbStoragePool.FormattingEnabled = true;
            this.lbStoragePool.Location = new System.Drawing.Point(6, 19);
            this.lbStoragePool.Name = "lbStoragePool";
            this.lbStoragePool.Size = new System.Drawing.Size(245, 95);
            this.lbStoragePool.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(197, 38);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 14;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "URI :";
            // 
            // tbURI
            // 
            this.tbURI.Location = new System.Drawing.Point(77, 12);
            this.tbURI.Name = "tbURI";
            this.tbURI.Size = new System.Drawing.Size(195, 20);
            this.tbURI.TabIndex = 8;
            this.tbURI.Text = "qemu+tcp://192.168.220.198/session";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.gbDomains);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbURI);
            this.Name = "Form1";
            this.Text = "Form1";
            this.gbDomains.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDomains;
        private System.Windows.Forms.ListBox lbStoragePool;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbURI;
    }
}

