namespace virDomainStats
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbURI = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbDomains = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbErrors = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbWriteBytes = new System.Windows.Forms.TextBox();
            this.tbWriteRequests = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbReadBytes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbReadRequest = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbBlockDev = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbCpuTime = new System.Windows.Forms.TextBox();
            this.tbNrVirtCpu = new System.Windows.Forms.TextBox();
            this.tbMemory = new System.Windows.Forms.TextBox();
            this.tbMaxMem = new System.Windows.Forms.TextBox();
            this.tbState = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbTxDrop = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbTxErrs = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.tbTxPackets = new System.Windows.Forms.TextBox();
            this.tbTxBytes = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tbRxDrop = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbRxErrs = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbRxPackets = new System.Windows.Forms.TextBox();
            this.tbRxBytes = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cbInterfaces = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "URI :";
            //
            // tbURI
            //
            this.tbURI.Location = new System.Drawing.Point(50, 12);
            this.tbURI.Name = "tbURI";
            this.tbURI.Size = new System.Drawing.Size(280, 20);
            this.tbURI.TabIndex = 1;
            this.tbURI.Text = "qemu+tcp://192.168.220.198/session";
            //
            // btnConnect
            //
            this.btnConnect.Location = new System.Drawing.Point(255, 38);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.Connect_Click);
            //
            // cbDomains
            //
            this.cbDomains.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDomains.FormattingEnabled = true;
            this.cbDomains.Location = new System.Drawing.Point(67, 67);
            this.cbDomains.Name = "cbDomains";
            this.cbDomains.Size = new System.Drawing.Size(263, 21);
            this.cbDomains.TabIndex = 3;
            this.cbDomains.SelectedIndexChanged += new System.EventHandler(this.cbDomains_SelectedIndexChanged);
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Domain :";
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbErrors);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbWriteBytes);
            this.groupBox1.Controls.Add(this.tbWriteRequests);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbReadBytes);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbReadRequest);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbBlockDev);
            this.groupBox1.Location = new System.Drawing.Point(221, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 182);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Block statistics";
            //
            // label8
            //
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Errors :";
            //
            // tbErrors
            //
            this.tbErrors.Location = new System.Drawing.Point(94, 150);
            this.tbErrors.Name = "tbErrors";
            this.tbErrors.ReadOnly = true;
            this.tbErrors.Size = new System.Drawing.Size(100, 20);
            this.tbErrors.TabIndex = 13;
            //
            // label7
            //
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Write bytes :";
            //
            // label6
            //
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Write requests :";
            //
            // tbWriteBytes
            //
            this.tbWriteBytes.Location = new System.Drawing.Point(94, 124);
            this.tbWriteBytes.Name = "tbWriteBytes";
            this.tbWriteBytes.ReadOnly = true;
            this.tbWriteBytes.Size = new System.Drawing.Size(100, 20);
            this.tbWriteBytes.TabIndex = 10;
            //
            // tbWriteRequests
            //
            this.tbWriteRequests.Location = new System.Drawing.Point(94, 98);
            this.tbWriteRequests.Name = "tbWriteRequests";
            this.tbWriteRequests.ReadOnly = true;
            this.tbWriteRequests.Size = new System.Drawing.Size(100, 20);
            this.tbWriteRequests.TabIndex = 9;
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Read bytes :";
            //
            // tbReadBytes
            //
            this.tbReadBytes.Location = new System.Drawing.Point(94, 72);
            this.tbReadBytes.Name = "tbReadBytes";
            this.tbReadBytes.ReadOnly = true;
            this.tbReadBytes.Size = new System.Drawing.Size(100, 20);
            this.tbReadBytes.TabIndex = 8;
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Read requests :";
            //
            // tbReadRequest
            //
            this.tbReadRequest.Location = new System.Drawing.Point(94, 46);
            this.tbReadRequest.Name = "tbReadRequest";
            this.tbReadRequest.ReadOnly = true;
            this.tbReadRequest.Size = new System.Drawing.Size(100, 20);
            this.tbReadRequest.TabIndex = 6;
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Block device ;";
            //
            // cbBlockDev
            //
            this.cbBlockDev.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlockDev.FormattingEnabled = true;
            this.cbBlockDev.Location = new System.Drawing.Point(87, 19);
            this.cbBlockDev.Name = "cbBlockDev";
            this.cbBlockDev.Size = new System.Drawing.Size(107, 21);
            this.cbBlockDev.TabIndex = 6;
            this.cbBlockDev.SelectedIndexChanged += new System.EventHandler(this.cbBlockDev_SelectedIndexChanged);
            //
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbCpuTime);
            this.groupBox2.Controls.Add(this.tbNrVirtCpu);
            this.groupBox2.Controls.Add(this.tbMemory);
            this.groupBox2.Controls.Add(this.tbMaxMem);
            this.groupBox2.Controls.Add(this.tbState);
            this.groupBox2.Location = new System.Drawing.Point(15, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 182);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Domain Infos";
            //
            // label13
            //
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 127);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "CPU Time :";
            //
            // label12
            //
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 101);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Num. Virt. CPUS :";
            //
            // label11
            //
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Memory :";
            //
            // label10
            //
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Max. Memory :";
            //
            // label9
            //
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "State :";
            //
            // tbCpuTime
            //
            this.tbCpuTime.Location = new System.Drawing.Point(103, 124);
            this.tbCpuTime.Name = "tbCpuTime";
            this.tbCpuTime.ReadOnly = true;
            this.tbCpuTime.Size = new System.Drawing.Size(91, 20);
            this.tbCpuTime.TabIndex = 11;
            //
            // tbNrVirtCpu
            //
            this.tbNrVirtCpu.Location = new System.Drawing.Point(103, 98);
            this.tbNrVirtCpu.Name = "tbNrVirtCpu";
            this.tbNrVirtCpu.ReadOnly = true;
            this.tbNrVirtCpu.Size = new System.Drawing.Size(91, 20);
            this.tbNrVirtCpu.TabIndex = 10;
            //
            // tbMemory
            //
            this.tbMemory.Location = new System.Drawing.Point(103, 72);
            this.tbMemory.Name = "tbMemory";
            this.tbMemory.ReadOnly = true;
            this.tbMemory.Size = new System.Drawing.Size(91, 20);
            this.tbMemory.TabIndex = 9;
            //
            // tbMaxMem
            //
            this.tbMaxMem.Location = new System.Drawing.Point(103, 46);
            this.tbMaxMem.Name = "tbMaxMem";
            this.tbMaxMem.ReadOnly = true;
            this.tbMaxMem.Size = new System.Drawing.Size(91, 20);
            this.tbMaxMem.TabIndex = 8;
            //
            // tbState
            //
            this.tbState.Location = new System.Drawing.Point(103, 19);
            this.tbState.Name = "tbState";
            this.tbState.ReadOnly = true;
            this.tbState.Size = new System.Drawing.Size(91, 20);
            this.tbState.TabIndex = 7;
            //
            // groupBox3
            //
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.tbTxDrop);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.tbTxErrs);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.tbTxPackets);
            this.groupBox3.Controls.Add(this.tbTxBytes);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.tbRxDrop);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.tbRxErrs);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.tbRxPackets);
            this.groupBox3.Controls.Add(this.tbRxBytes);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.cbInterfaces);
            this.groupBox3.Location = new System.Drawing.Point(15, 282);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(410, 155);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Interface satistics";
            //
            // label19
            //
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(212, 124);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(48, 13);
            this.label19.TabIndex = 26;
            this.label19.Text = "TxDrop :";
            //
            // tbTxDrop
            //
            this.tbTxDrop.Location = new System.Drawing.Point(279, 121);
            this.tbTxDrop.Name = "tbTxDrop";
            this.tbTxDrop.ReadOnly = true;
            this.tbTxDrop.Size = new System.Drawing.Size(121, 20);
            this.tbTxDrop.TabIndex = 25;
            //
            // label20
            //
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(212, 98);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(43, 13);
            this.label20.TabIndex = 24;
            this.label20.Text = "TxErrs :";
            //
            // tbTxErrs
            //
            this.tbTxErrs.Location = new System.Drawing.Point(279, 95);
            this.tbTxErrs.Name = "tbTxErrs";
            this.tbTxErrs.ReadOnly = true;
            this.tbTxErrs.Size = new System.Drawing.Size(121, 20);
            this.tbTxErrs.TabIndex = 23;
            //
            // label21
            //
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(212, 72);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(64, 13);
            this.label21.TabIndex = 22;
            this.label21.Text = "TxPackets :";
            //
            // tbTxPackets
            //
            this.tbTxPackets.Location = new System.Drawing.Point(279, 69);
            this.tbTxPackets.Name = "tbTxPackets";
            this.tbTxPackets.ReadOnly = true;
            this.tbTxPackets.Size = new System.Drawing.Size(121, 20);
            this.tbTxPackets.TabIndex = 21;
            //
            // tbTxBytes
            //
            this.tbTxBytes.Location = new System.Drawing.Point(279, 43);
            this.tbTxBytes.Name = "tbTxBytes";
            this.tbTxBytes.ReadOnly = true;
            this.tbTxBytes.Size = new System.Drawing.Size(121, 20);
            this.tbTxBytes.TabIndex = 20;
            //
            // label22
            //
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(212, 46);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(51, 13);
            this.label22.TabIndex = 19;
            this.label22.Text = "TxBytes :";
            //
            // label18
            //
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 124);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 13);
            this.label18.TabIndex = 18;
            this.label18.Text = "RxDrop :";
            //
            // tbRxDrop
            //
            this.tbRxDrop.Location = new System.Drawing.Point(73, 121);
            this.tbRxDrop.Name = "tbRxDrop";
            this.tbRxDrop.ReadOnly = true;
            this.tbRxDrop.Size = new System.Drawing.Size(121, 20);
            this.tbRxDrop.TabIndex = 17;
            //
            // label17
            //
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 98);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 13);
            this.label17.TabIndex = 16;
            this.label17.Text = "RxErrs :";
            //
            // tbRxErrs
            //
            this.tbRxErrs.Location = new System.Drawing.Point(73, 95);
            this.tbRxErrs.Name = "tbRxErrs";
            this.tbRxErrs.ReadOnly = true;
            this.tbRxErrs.Size = new System.Drawing.Size(121, 20);
            this.tbRxErrs.TabIndex = 15;
            //
            // label16
            //
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 13);
            this.label16.TabIndex = 14;
            this.label16.Text = "RxPackets :";
            //
            // tbRxPackets
            //
            this.tbRxPackets.Location = new System.Drawing.Point(73, 69);
            this.tbRxPackets.Name = "tbRxPackets";
            this.tbRxPackets.ReadOnly = true;
            this.tbRxPackets.Size = new System.Drawing.Size(121, 20);
            this.tbRxPackets.TabIndex = 13;
            //
            // tbRxBytes
            //
            this.tbRxBytes.Location = new System.Drawing.Point(73, 43);
            this.tbRxBytes.Name = "tbRxBytes";
            this.tbRxBytes.ReadOnly = true;
            this.tbRxBytes.Size = new System.Drawing.Size(121, 20);
            this.tbRxBytes.TabIndex = 12;
            //
            // label15
            //
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 46);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 13);
            this.label15.TabIndex = 8;
            this.label15.Text = "RxBytes :";
            //
            // label14
            //
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "Interface :";
            //
            // cbInterfaces
            //
            this.cbInterfaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterfaces.FormattingEnabled = true;
            this.cbInterfaces.Location = new System.Drawing.Point(73, 19);
            this.cbInterfaces.Name = "cbInterfaces";
            this.cbInterfaces.Size = new System.Drawing.Size(121, 21);
            this.cbInterfaces.TabIndex = 8;
            this.cbInterfaces.SelectedIndexChanged += new System.EventHandler(this.cbInterfaces_SelectedIndexChanged);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 442);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbDomains);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbURI);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbURI;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbDomains;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbBlockDev;
        private System.Windows.Forms.TextBox tbReadRequest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbReadBytes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbWriteBytes;
        private System.Windows.Forms.TextBox tbWriteRequests;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbErrors;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbCpuTime;
        private System.Windows.Forms.TextBox tbNrVirtCpu;
        private System.Windows.Forms.TextBox tbMemory;
        private System.Windows.Forms.TextBox tbMaxMem;
        private System.Windows.Forms.TextBox tbState;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbInterfaces;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbTxDrop;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbTxErrs;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tbTxPackets;
        private System.Windows.Forms.TextBox tbTxBytes;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbRxDrop;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbRxErrs;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbRxPackets;
        private System.Windows.Forms.TextBox tbRxBytes;
        private System.Windows.Forms.Label label15;
    }
}
