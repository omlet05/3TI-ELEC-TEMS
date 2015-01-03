namespace TEMS___Serial_TEST
{
    partial class SerialForm
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
            this.components = new System.ComponentModel.Container();
            this.writeTextBox = new System.Windows.Forms.RichTextBox();
            this.readTextBox = new System.Windows.Forms.RichTextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.newLibeBut = new System.Windows.Forms.Button();
            this.closePort = new System.Windows.Forms.Button();
            this.WichCom = new System.Windows.Forms.TextBox();
            this.cboPorts = new System.Windows.Forms.ComboBox();
            this.openPort = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // writeTextBox
            // 
            this.writeTextBox.Location = new System.Drawing.Point(111, 205);
            this.writeTextBox.Name = "writeTextBox";
            this.writeTextBox.Size = new System.Drawing.Size(292, 62);
            this.writeTextBox.TabIndex = 0;
            this.writeTextBox.Text = "";
            this.writeTextBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.writeTextBox_MouseDoubleClick);
            // 
            // readTextBox
            // 
            this.readTextBox.Location = new System.Drawing.Point(111, 12);
            this.readTextBox.Name = "readTextBox";
            this.readTextBox.ReadOnly = true;
            this.readTextBox.Size = new System.Drawing.Size(378, 169);
            this.readTextBox.TabIndex = 1;
            this.readTextBox.Text = "";
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(397, 18);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(74, 28);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(397, 52);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(74, 28);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // newLibeBut
            // 
            this.newLibeBut.Location = new System.Drawing.Point(6, 31);
            this.newLibeBut.Name = "newLibeBut";
            this.newLibeBut.Size = new System.Drawing.Size(74, 37);
            this.newLibeBut.TabIndex = 6;
            this.newLibeBut.Text = "Switching Relay";
            this.newLibeBut.UseVisualStyleBackColor = true;
            this.newLibeBut.Click += new System.EventHandler(this.button1_Click);
            // 
            // closePort
            // 
            this.closePort.Location = new System.Drawing.Point(6, 46);
            this.closePort.Name = "closePort";
            this.closePort.Size = new System.Drawing.Size(72, 21);
            this.closePort.TabIndex = 17;
            this.closePort.Text = "ClosePort";
            this.closePort.UseVisualStyleBackColor = true;
            this.closePort.Click += new System.EventHandler(this.closePort_Click);
            // 
            // WichCom
            // 
            this.WichCom.Location = new System.Drawing.Point(6, 107);
            this.WichCom.Name = "WichCom";
            this.WichCom.ReadOnly = true;
            this.WichCom.Size = new System.Drawing.Size(72, 20);
            this.WichCom.TabIndex = 16;
            // 
            // cboPorts
            // 
            this.cboPorts.FormattingEnabled = true;
            this.cboPorts.Location = new System.Drawing.Point(6, 133);
            this.cboPorts.Name = "cboPorts";
            this.cboPorts.Size = new System.Drawing.Size(72, 21);
            this.cboPorts.TabIndex = 15;
            this.cboPorts.SelectedIndexChanged += new System.EventHandler(this.cboPorts_SelectedIndexChanged);
            // 
            // openPort
            // 
            this.openPort.Location = new System.Drawing.Point(6, 19);
            this.openPort.Name = "openPort";
            this.openPort.Size = new System.Drawing.Size(72, 21);
            this.openPort.TabIndex = 14;
            this.openPort.Text = "OpenPort";
            this.openPort.UseVisualStyleBackColor = true;
            this.openPort.Click += new System.EventHandler(this.openPort_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboPorts);
            this.groupBox1.Controls.Add(this.openPort);
            this.groupBox1.Controls.Add(this.closePort);
            this.groupBox1.Controls.Add(this.WichCom);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(90, 169);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SERIAL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "COM port:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.newLibeBut);
            this.groupBox2.Controls.Add(this.sendButton);
            this.groupBox2.Controls.Add(this.clearButton);
            this.groupBox2.Location = new System.Drawing.Point(12, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(477, 95);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Command:";
            // 
            // SerialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 294);
            this.Controls.Add(this.readTextBox);
            this.Controls.Add(this.writeTextBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SerialForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TEMS - Manager -  Serial Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox writeTextBox;
        private System.Windows.Forms.RichTextBox readTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button newLibeBut;
        private System.Windows.Forms.Button closePort;
        private System.Windows.Forms.TextBox WichCom;
        private System.Windows.Forms.ComboBox cboPorts;
        private System.Windows.Forms.Button openPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

