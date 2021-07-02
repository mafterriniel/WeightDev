namespace Tester
{
    partial class frmTester
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
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.cboCType = new System.Windows.Forms.ComboBox();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtIPPort = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtIPAddr = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEndCharacter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDevice = new System.Windows.Forms.ComboBox();
            this.chkDIscardOutBuffer = new System.Windows.Forms.CheckBox();
            this.txtDataLength = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCommPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReceivedBytesThreshold = new System.Windows.Forms.TextBox();
            this.txtReadBufferSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtSignal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSignalLength = new System.Windows.Forms.TextBox();
            this.weightIndicator1 = new WeightDev.WeightIndicator();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(293, 437);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(115, 41);
            this.btnStop.TabIndex = 15;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(171, 437);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(115, 41);
            this.btnStart.TabIndex = 16;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSignalLength);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.cboCType);
            this.panel1.Controls.Add(this.txtTimeOut);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtIPPort);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtIPAddr);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtEndCharacter);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cboDevice);
            this.panel1.Controls.Add(this.chkDIscardOutBuffer);
            this.panel1.Controls.Add(this.txtDataLength);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtCommPort);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.txtReceivedBytesThreshold);
            this.panel1.Controls.Add(this.txtReadBufferSize);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(421, 491);
            this.panel1.TabIndex = 18;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 246);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(139, 17);
            this.label14.TabIndex = 40;
            this.label14.Text = "CONNECTION TYPE";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboCType
            // 
            this.cboCType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCType.FormattingEnabled = true;
            this.cboCType.Items.AddRange(new object[] {
            "COMM",
            "IP"});
            this.cboCType.Location = new System.Drawing.Point(232, 242);
            this.cboCType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboCType.Name = "cboCType";
            this.cboCType.Size = new System.Drawing.Size(175, 24);
            this.cboCType.TabIndex = 39;
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.Location = new System.Drawing.Point(232, 329);
            this.txtTimeOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(175, 22);
            this.txtTimeOut.TabIndex = 38;
            this.txtTimeOut.Text = "1000";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 330);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 17);
            this.label13.TabIndex = 37;
            this.label13.Text = "TIMEOUT";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIPPort
            // 
            this.txtIPPort.Location = new System.Drawing.Point(232, 302);
            this.txtIPPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtIPPort.Name = "txtIPPort";
            this.txtIPPort.Size = new System.Drawing.Size(175, 22);
            this.txtIPPort.TabIndex = 36;
            this.txtIPPort.Text = "1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 302);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 17);
            this.label12.TabIndex = 35;
            this.label12.Text = "IP PORT";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIPAddr
            // 
            this.txtIPAddr.Location = new System.Drawing.Point(232, 273);
            this.txtIPAddr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtIPAddr.Name = "txtIPAddr";
            this.txtIPAddr.Size = new System.Drawing.Size(175, 22);
            this.txtIPAddr.TabIndex = 34;
            this.txtIPAddr.Text = "192.168.1.41";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 273);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 17);
            this.label11.TabIndex = 33;
            this.label11.Text = "IP ADDRESS";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEndCharacter
            // 
            this.txtEndCharacter.Location = new System.Drawing.Point(232, 191);
            this.txtEndCharacter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEndCharacter.Name = "txtEndCharacter";
            this.txtEndCharacter.Size = new System.Drawing.Size(175, 22);
            this.txtEndCharacter.TabIndex = 32;
            this.txtEndCharacter.Text = "\\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 192);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 31;
            this.label1.Text = "END CHARACTER";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 30;
            this.label4.Text = "DEVICE";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboDevice
            // 
            this.cboDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDevice.FormattingEnabled = true;
            this.cboDevice.Items.AddRange(new object[] {
            "GSE460",
            "GSE460V2",
            "RINSTRUMR323",
            "RINSTRUMR323V2",
            "RINSTRUMR323V3",
            "ZM405V2"});
            this.cboDevice.Location = new System.Drawing.Point(232, 15);
            this.cboDevice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboDevice.Name = "cboDevice";
            this.cboDevice.Size = new System.Drawing.Size(175, 24);
            this.cboDevice.TabIndex = 29;
            this.cboDevice.SelectedIndexChanged += new System.EventHandler(this.cboDevice_SelectedIndexChanged);
            // 
            // chkDIscardOutBuffer
            // 
            this.chkDIscardOutBuffer.AutoSize = true;
            this.chkDIscardOutBuffer.Location = new System.Drawing.Point(12, 165);
            this.chkDIscardOutBuffer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkDIscardOutBuffer.Name = "chkDIscardOutBuffer";
            this.chkDIscardOutBuffer.Size = new System.Drawing.Size(182, 21);
            this.chkDIscardOutBuffer.TabIndex = 28;
            this.chkDIscardOutBuffer.Text = "DISCARD OUT BUFFER";
            this.chkDIscardOutBuffer.UseVisualStyleBackColor = true;
            // 
            // txtDataLength
            // 
            this.txtDataLength.Location = new System.Drawing.Point(232, 78);
            this.txtDataLength.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDataLength.Name = "txtDataLength";
            this.txtDataLength.Size = new System.Drawing.Size(175, 22);
            this.txtDataLength.TabIndex = 27;
            this.txtDataLength.Text = "8";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 78);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 17);
            this.label10.TabIndex = 26;
            this.label10.Text = "DATA LENGTH:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 388);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 17);
            this.label8.TabIndex = 25;
            this.label8.Text = "LENGTH:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 359);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(199, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "RECEIVED DATA DETAILS";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCommPort
            // 
            this.txtCommPort.Location = new System.Drawing.Point(232, 47);
            this.txtCommPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCommPort.Name = "txtCommPort";
            this.txtCommPort.Size = new System.Drawing.Size(175, 22);
            this.txtCommPort.TabIndex = 21;
            this.txtCommPort.Text = "COM5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "COMM PORT";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReceivedBytesThreshold
            // 
            this.txtReceivedBytesThreshold.Location = new System.Drawing.Point(232, 135);
            this.txtReceivedBytesThreshold.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtReceivedBytesThreshold.Name = "txtReceivedBytesThreshold";
            this.txtReceivedBytesThreshold.Size = new System.Drawing.Size(175, 22);
            this.txtReceivedBytesThreshold.TabIndex = 18;
            this.txtReceivedBytesThreshold.Text = "1";
            this.txtReceivedBytesThreshold.TextChanged += new System.EventHandler(this.txtReceivedBytesThreshold_TextChanged);
            // 
            // txtReadBufferSize
            // 
            this.txtReadBufferSize.Location = new System.Drawing.Point(232, 105);
            this.txtReadBufferSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtReadBufferSize.Name = "txtReadBufferSize";
            this.txtReadBufferSize.Size = new System.Drawing.Size(175, 22);
            this.txtReadBufferSize.TabIndex = 17;
            this.txtReadBufferSize.Text = "4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 135);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "RECEIVED BYTES THRESHOLD";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 106);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "READ BUFFER SIZE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(8, 7);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(558, 37);
            this.label6.TabIndex = 22;
            this.label6.Text = "DEVICE DATA";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.weightIndicator1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(421, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.panel2.Size = new System.Drawing.Size(574, 97);
            this.panel2.TabIndex = 19;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtSignal);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(421, 97);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.panel3.Size = new System.Drawing.Size(574, 394);
            this.panel3.TabIndex = 20;
            // 
            // txtSignal
            // 
            this.txtSignal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSignal.Location = new System.Drawing.Point(8, 40);
            this.txtSignal.Margin = new System.Windows.Forms.Padding(4);
            this.txtSignal.Multiline = true;
            this.txtSignal.Name = "txtSignal";
            this.txtSignal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSignal.Size = new System.Drawing.Size(558, 347);
            this.txtSignal.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(8, 7);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(558, 33);
            this.label9.TabIndex = 23;
            this.label9.Text = "SIGNAL";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSignalLength
            // 
            this.txtSignalLength.Location = new System.Drawing.Point(223, 388);
            this.txtSignalLength.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSignalLength.Name = "txtSignalLength";
            this.txtSignalLength.ReadOnly = true;
            this.txtSignalLength.Size = new System.Drawing.Size(175, 22);
            this.txtSignalLength.TabIndex = 41;
            this.txtSignalLength.Text = "0";
            // 
            // weightIndicator1
            // 
            this.weightIndicator1.AccessPwd = "mijochanel09041990";
            this.weightIndicator1.BackColor = System.Drawing.Color.Black;
            this.weightIndicator1.CommBaudRate = 9600;
            this.weightIndicator1.CommDataBits = 8;
            this.weightIndicator1.CommDTREnable = true;
            this.weightIndicator1.CommNewLine = "\n";
            this.weightIndicator1.CommParity = "None";
            this.weightIndicator1.CommPortName = "COM1";
            this.weightIndicator1.CommReadBufferSize = 10;
            this.weightIndicator1.CommReadTimeout = 0;
            this.weightIndicator1.CommReceivedBytesThreshold = 10;
            this.weightIndicator1.CommRTSEnable = false;
            this.weightIndicator1.CommStopBits = "1";
            this.weightIndicator1.CommWriteTimeout = 0;
            this.weightIndicator1.ConnectionType = null;
            this.weightIndicator1.DataLength = 0;
            this.weightIndicator1.DataSent = 0;
            this.weightIndicator1.DiscardInBuffer = false;
            this.weightIndicator1.DiscardOutBuffer = false;
            this.weightIndicator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.weightIndicator1.EndCharacter = null;
            this.weightIndicator1.ExtStartIndex = 0;
            this.weightIndicator1.Font = new System.Drawing.Font("Segoe UI", 16.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weightIndicator1.ForeColor = System.Drawing.Color.YellowGreen;
            this.weightIndicator1.IPAddress = "192.168.1.41";
            this.weightIndicator1.IPPort = "1";
            this.weightIndicator1.IPReadTimeOut = 1000;
            this.weightIndicator1.LengthViewer = null;
            this.weightIndicator1.Location = new System.Drawing.Point(8, 44);
            this.weightIndicator1.Margin = new System.Windows.Forms.Padding(4);
            this.weightIndicator1.Name = "weightIndicator1";
            this.weightIndicator1.ReadingInterval = 100;
            this.weightIndicator1.ReadOnly = true;
            this.weightIndicator1.Sensitivity = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.weightIndicator1.SignalViewer = null;
            this.weightIndicator1.SimulationIncrement = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.weightIndicator1.SimulationSpeed = 100;
            this.weightIndicator1.Size = new System.Drawing.Size(558, 44);
            this.weightIndicator1.TabIndex = 10;
            this.weightIndicator1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.weightIndicator1.WeighingDevice = "PTLTD";
            // 
            // frmTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 491);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmTester";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TESTER";
            this.Load += new System.EventHandler(this.frmTester_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private WeightDev.WeightIndicator weightIndicator1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCommPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtReceivedBytesThreshold;
        private System.Windows.Forms.TextBox txtReadBufferSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSignal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDataLength;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkDIscardOutBuffer;
        private System.Windows.Forms.ComboBox cboDevice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEndCharacter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIPPort;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtIPAddr;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboCType;
        private System.Windows.Forms.TextBox txtSignalLength;
    }
}