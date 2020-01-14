﻿namespace AgOpenGPS
{
    partial class FormCommSet
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnRescan = new System.Windows.Forms.Button();
            this.btnSerialOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCurrentBaud2 = new System.Windows.Forms.Label();
            this.lblCurrentPort2 = new System.Windows.Forms.Label();
            this.btnCloseSerial2 = new System.Windows.Forms.Button();
            this.btnOpenSerial2 = new System.Windows.Forms.Button();
            this.cboxBaud2 = new System.Windows.Forms.ComboBox();
            this.cboxPort2 = new System.Windows.Forms.ComboBox();
            this.cboxPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboxBaud = new System.Windows.Forms.ComboBox();
            this.cboxNMEAHz = new System.Windows.Forms.ComboBox();
            this.lblCurrentPort = new System.Windows.Forms.Label();
            this.lblCurrentBaud = new System.Windows.Forms.Label();
            this.btnCloseSerial = new System.Windows.Forms.Button();
            this.textBoxRcv = new System.Windows.Forms.TextBox();
            this.btnOpenSerial = new System.Windows.Forms.Button();
            this.btnCloseSerialArduino = new System.Windows.Forms.Button();
            this.btnOpenSerialArduino = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.lblCurrentArduinoPort = new System.Windows.Forms.Label();
            this.txtBoxRecvArduino = new System.Windows.Forms.TextBox();
            this.cboxArdPort = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxSendArduino = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.usejrk = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxSendAutoSteer = new System.Windows.Forms.TextBox();
            this.cboxASPort = new System.Windows.Forms.ComboBox();
            this.txtBoxRecvAutoSteer = new System.Windows.Forms.TextBox();
            this.lblCurrentAutoSteerPort = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOpenSerialAutoSteer = new System.Windows.Forms.Button();
            this.btnCloseSerialAutoSteer = new System.Windows.Forms.Button();
            this.rbtnGGA = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbtnUBX = new System.Windows.Forms.RadioButton();
            this.rbtnOGI = new System.Windows.Forms.RadioButton();
            this.rbtnRMC = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnRescan
            // 
            this.btnRescan.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnRescan.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRescan.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRescan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRescan.Location = new System.Drawing.Point(788, 294);
            this.btnRescan.Name = "btnRescan";
            this.btnRescan.Size = new System.Drawing.Size(142, 72);
            this.btnRescan.TabIndex = 58;
            this.btnRescan.Text = "Rescan Ports";
            this.btnRescan.UseVisualStyleBackColor = false;
            this.btnRescan.Click += new System.EventHandler(this.btnRescan_Click);
            // 
            // btnSerialOK
            // 
            this.btnSerialOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSerialOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSerialOK.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSerialOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSerialOK.Image = global::AgOpenGPS.Properties.Resources.OK64;
            this.btnSerialOK.Location = new System.Drawing.Point(774, 563);
            this.btnSerialOK.Name = "btnSerialOK";
            this.btnSerialOK.Size = new System.Drawing.Size(156, 72);
            this.btnSerialOK.TabIndex = 59;
            this.btnSerialOK.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSerialOK.UseVisualStyleBackColor = true;
            this.btnSerialOK.Click += new System.EventHandler(this.btnSerialOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.lblCurrentBaud2);
            this.groupBox1.Controls.Add(this.lblCurrentPort2);
            this.groupBox1.Controls.Add(this.btnCloseSerial2);
            this.groupBox1.Controls.Add(this.btnOpenSerial2);
            this.groupBox1.Controls.Add(this.cboxBaud2);
            this.groupBox1.Controls.Add(this.cboxPort2);
            this.groupBox1.Controls.Add(this.cboxPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cboxBaud);
            this.groupBox1.Controls.Add(this.cboxNMEAHz);
            this.groupBox1.Controls.Add(this.lblCurrentPort);
            this.groupBox1.Controls.Add(this.lblCurrentBaud);
            this.groupBox1.Controls.Add(this.btnCloseSerial);
            this.groupBox1.Controls.Add(this.textBoxRcv);
            this.groupBox1.Controls.Add(this.btnOpenSerial);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(12, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(767, 260);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GPS Port";
            // 
            // lblCurrentBaud2
            // 
            this.lblCurrentBaud2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentBaud2.Location = new System.Drawing.Point(140, 139);
            this.lblCurrentBaud2.Name = "lblCurrentBaud2";
            this.lblCurrentBaud2.Size = new System.Drawing.Size(120, 24);
            this.lblCurrentBaud2.TabIndex = 75;
            this.lblCurrentBaud2.Text = "Baud";
            this.lblCurrentBaud2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPort2
            // 
            this.lblCurrentPort2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPort2.Location = new System.Drawing.Point(11, 137);
            this.lblCurrentPort2.Name = "lblCurrentPort2";
            this.lblCurrentPort2.Size = new System.Drawing.Size(100, 24);
            this.lblCurrentPort2.TabIndex = 74;
            this.lblCurrentPort2.Text = "Port";
            this.lblCurrentPort2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCurrentPort2.UseWaitCursor = true;
            // 
            // btnCloseSerial2
            // 
            this.btnCloseSerial2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCloseSerial2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCloseSerial2.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseSerial2.Location = new System.Drawing.Point(610, 95);
            this.btnCloseSerial2.Name = "btnCloseSerial2";
            this.btnCloseSerial2.Size = new System.Drawing.Size(140, 41);
            this.btnCloseSerial2.TabIndex = 73;
            this.btnCloseSerial2.Text = "Disconnect";
            this.btnCloseSerial2.UseVisualStyleBackColor = false;
            this.btnCloseSerial2.Click += new System.EventHandler(this.btnCloseSerial_Click2);
            // 
            // btnOpenSerial2
            // 
            this.btnOpenSerial2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOpenSerial2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenSerial2.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenSerial2.Location = new System.Drawing.Point(450, 95);
            this.btnOpenSerial2.Name = "btnOpenSerial2";
            this.btnOpenSerial2.Size = new System.Drawing.Size(140, 41);
            this.btnOpenSerial2.TabIndex = 72;
            this.btnOpenSerial2.Text = "Connect";
            this.btnOpenSerial2.UseVisualStyleBackColor = false;
            this.btnOpenSerial2.Click += new System.EventHandler(this.btnOpenSerial_Click2);
            // 
            // cboxBaud2
            // 
            this.cboxBaud2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cboxBaud2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxBaud2.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold);
            this.cboxBaud2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cboxBaud2.FormattingEnabled = true;
            this.cboxBaud2.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cboxBaud2.Location = new System.Drawing.Point(140, 95);
            this.cboxBaud2.Name = "cboxBaud2";
            this.cboxBaud2.Size = new System.Drawing.Size(120, 41);
            this.cboxBaud2.TabIndex = 70;
            this.cboxBaud2.SelectedIndexChanged += new System.EventHandler(this.cboxBaud_SelectedIndexChanged_2);
            // 
            // cboxPort2
            // 
            this.cboxPort2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cboxPort2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxPort2.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold);
            this.cboxPort2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cboxPort2.FormattingEnabled = true;
            this.cboxPort2.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cboxPort2.Location = new System.Drawing.Point(10, 94);
            this.cboxPort2.Name = "cboxPort2";
            this.cboxPort2.Size = new System.Drawing.Size(101, 41);
            this.cboxPort2.TabIndex = 69;
            this.cboxPort2.SelectedIndexChanged += new System.EventHandler(this.cboxPort_SelectedIndexChanged_2);
            // 
            // cboxPort
            // 
            this.cboxPort.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cboxPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxPort.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxPort.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cboxPort.FormattingEnabled = true;
            this.cboxPort.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cboxPort.Location = new System.Drawing.Point(10, 30);
            this.cboxPort.Name = "cboxPort";
            this.cboxPort.Size = new System.Drawing.Size(100, 41);
            this.cboxPort.TabIndex = 50;
            this.cboxPort.UseWaitCursor = true;
            this.cboxPort.SelectedIndexChanged += new System.EventHandler(this.cboxPort_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(469, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(292, 19);
            this.label1.TabIndex = 41;
            this.label1.Text = "NMEA String";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(290, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 24);
            this.label7.TabIndex = 68;
            this.label7.Text = "NMEA Hz";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboxBaud
            // 
            this.cboxBaud.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cboxBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxBaud.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold);
            this.cboxBaud.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cboxBaud.FormattingEnabled = true;
            this.cboxBaud.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cboxBaud.Location = new System.Drawing.Point(140, 30);
            this.cboxBaud.Name = "cboxBaud";
            this.cboxBaud.Size = new System.Drawing.Size(120, 41);
            this.cboxBaud.TabIndex = 49;
            this.cboxBaud.SelectedIndexChanged += new System.EventHandler(this.cboxBaud_SelectedIndexChanged_1);
            // 
            // cboxNMEAHz
            // 
            this.cboxNMEAHz.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboxNMEAHz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxNMEAHz.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold);
            this.cboxNMEAHz.Items.AddRange(new object[] {
            "1",
            "2",
            "5",
            "8",
            "10"});
            this.cboxNMEAHz.Location = new System.Drawing.Point(290, 30);
            this.cboxNMEAHz.Name = "cboxNMEAHz";
            this.cboxNMEAHz.Size = new System.Drawing.Size(100, 41);
            this.cboxNMEAHz.TabIndex = 67;
            this.cboxNMEAHz.SelectedIndexChanged += new System.EventHandler(this.cboxNMEAHz_SelectedIndexChanged);
            // 
            // lblCurrentPort
            // 
            this.lblCurrentPort.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPort.Location = new System.Drawing.Point(10, 71);
            this.lblCurrentPort.Name = "lblCurrentPort";
            this.lblCurrentPort.Size = new System.Drawing.Size(100, 24);
            this.lblCurrentPort.TabIndex = 47;
            this.lblCurrentPort.Text = "Port";
            this.lblCurrentPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCurrentPort.UseWaitCursor = true;
            // 
            // lblCurrentBaud
            // 
            this.lblCurrentBaud.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentBaud.Location = new System.Drawing.Point(140, 71);
            this.lblCurrentBaud.Name = "lblCurrentBaud";
            this.lblCurrentBaud.Size = new System.Drawing.Size(120, 24);
            this.lblCurrentBaud.TabIndex = 46;
            this.lblCurrentBaud.Text = "Baud";
            this.lblCurrentBaud.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCloseSerial
            // 
            this.btnCloseSerial.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCloseSerial.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCloseSerial.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseSerial.Location = new System.Drawing.Point(610, 30);
            this.btnCloseSerial.Name = "btnCloseSerial";
            this.btnCloseSerial.Size = new System.Drawing.Size(140, 41);
            this.btnCloseSerial.TabIndex = 44;
            this.btnCloseSerial.Text = "Disconnect";
            this.btnCloseSerial.UseVisualStyleBackColor = false;
            this.btnCloseSerial.Click += new System.EventHandler(this.btnCloseSerial_Click);
            // 
            // textBoxRcv
            // 
            this.textBoxRcv.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxRcv.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxRcv.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.textBoxRcv.Location = new System.Drawing.Point(13, 166);
            this.textBoxRcv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxRcv.Multiline = true;
            this.textBoxRcv.Name = "textBoxRcv";
            this.textBoxRcv.ReadOnly = true;
            this.textBoxRcv.Size = new System.Drawing.Size(740, 60);
            this.textBoxRcv.TabIndex = 40;
            // 
            // btnOpenSerial
            // 
            this.btnOpenSerial.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOpenSerial.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenSerial.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenSerial.Location = new System.Drawing.Point(450, 30);
            this.btnOpenSerial.Name = "btnOpenSerial";
            this.btnOpenSerial.Size = new System.Drawing.Size(140, 41);
            this.btnOpenSerial.TabIndex = 45;
            this.btnOpenSerial.Text = "Connect";
            this.btnOpenSerial.UseVisualStyleBackColor = false;
            this.btnOpenSerial.Click += new System.EventHandler(this.btnOpenSerial_Click);
            // 
            // btnCloseSerialArduino
            // 
            this.btnCloseSerialArduino.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCloseSerialArduino.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCloseSerialArduino.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseSerialArduino.Location = new System.Drawing.Point(568, 24);
            this.btnCloseSerialArduino.Name = "btnCloseSerialArduino";
            this.btnCloseSerialArduino.Size = new System.Drawing.Size(138, 40);
            this.btnCloseSerialArduino.TabIndex = 52;
            this.btnCloseSerialArduino.Text = "Disconnect";
            this.btnCloseSerialArduino.UseVisualStyleBackColor = false;
            this.btnCloseSerialArduino.Click += new System.EventHandler(this.btnCloseSerialArduino_Click);
            // 
            // btnOpenSerialArduino
            // 
            this.btnOpenSerialArduino.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOpenSerialArduino.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenSerialArduino.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenSerialArduino.Location = new System.Drawing.Point(372, 24);
            this.btnOpenSerialArduino.Name = "btnOpenSerialArduino";
            this.btnOpenSerialArduino.Size = new System.Drawing.Size(138, 40);
            this.btnOpenSerialArduino.TabIndex = 53;
            this.btnOpenSerialArduino.Text = "Connect";
            this.btnOpenSerialArduino.UseVisualStyleBackColor = false;
            this.btnOpenSerialArduino.Click += new System.EventHandler(this.btnOpenSerialArduino_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(378, 97);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(142, 19);
            this.label15.TabIndex = 58;
            this.label15.Text = "From Section Port:";
            // 
            // lblCurrentArduinoPort
            // 
            this.lblCurrentArduinoPort.AutoSize = true;
            this.lblCurrentArduinoPort.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentArduinoPort.Location = new System.Drawing.Point(162, 40);
            this.lblCurrentArduinoPort.Name = "lblCurrentArduinoPort";
            this.lblCurrentArduinoPort.Size = new System.Drawing.Size(40, 18);
            this.lblCurrentArduinoPort.TabIndex = 59;
            this.lblCurrentArduinoPort.Text = "Port";
            // 
            // txtBoxRecvArduino
            // 
            this.txtBoxRecvArduino.BackColor = System.Drawing.SystemColors.Window;
            this.txtBoxRecvArduino.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.txtBoxRecvArduino.Location = new System.Drawing.Point(382, 119);
            this.txtBoxRecvArduino.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBoxRecvArduino.Name = "txtBoxRecvArduino";
            this.txtBoxRecvArduino.ReadOnly = true;
            this.txtBoxRecvArduino.Size = new System.Drawing.Size(320, 27);
            this.txtBoxRecvArduino.TabIndex = 63;
            // 
            // cboxArdPort
            // 
            this.cboxArdPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxArdPort.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.cboxArdPort.FormattingEnabled = true;
            this.cboxArdPort.Location = new System.Drawing.Point(25, 31);
            this.cboxArdPort.Name = "cboxArdPort";
            this.cboxArdPort.Size = new System.Drawing.Size(121, 37);
            this.cboxArdPort.TabIndex = 64;
            this.cboxArdPort.SelectedIndexChanged += new System.EventHandler(this.cboxArdPort_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtBoxSendArduino);
            this.groupBox2.Controls.Add(this.cboxArdPort);
            this.groupBox2.Controls.Add(this.txtBoxRecvArduino);
            this.groupBox2.Controls.Add(this.lblCurrentArduinoPort);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.btnOpenSerialArduino);
            this.groupBox2.Controls.Add(this.btnCloseSerialArduino);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(22, 480);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(724, 155);
            this.groupBox2.TabIndex = 65;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Section Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 19);
            this.label2.TabIndex = 66;
            this.label2.Text = "To Section Port:";
            // 
            // txtBoxSendArduino
            // 
            this.txtBoxSendArduino.BackColor = System.Drawing.SystemColors.Window;
            this.txtBoxSendArduino.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.txtBoxSendArduino.Location = new System.Drawing.Point(25, 119);
            this.txtBoxSendArduino.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBoxSendArduino.Name = "txtBoxSendArduino";
            this.txtBoxSendArduino.ReadOnly = true;
            this.txtBoxSendArduino.Size = new System.Drawing.Size(320, 27);
            this.txtBoxSendArduino.TabIndex = 65;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.usejrk);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtBoxSendAutoSteer);
            this.groupBox3.Controls.Add(this.cboxASPort);
            this.groupBox3.Controls.Add(this.txtBoxRecvAutoSteer);
            this.groupBox3.Controls.Add(this.lblCurrentAutoSteerPort);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btnOpenSerialAutoSteer);
            this.groupBox3.Controls.Add(this.btnCloseSerialAutoSteer);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(12, 310);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(724, 155);
            this.groupBox3.TabIndex = 66;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "AutoSteer Port";
            // 
            // usejrk
            // 
            this.usejrk.AutoSize = true;
            this.usejrk.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usejrk.Location = new System.Drawing.Point(255, 27);
            this.usejrk.Name = "usejrk";
            this.usejrk.Size = new System.Drawing.Size(72, 29);
            this.usejrk.TabIndex = 67;
            this.usejrk.Text = "JRK";
            this.usejrk.UseVisualStyleBackColor = true;
            this.usejrk.CheckedChanged += new System.EventHandler(this.usejrk_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 19);
            this.label3.TabIndex = 66;
            this.label3.Text = "To Auto Steer:";
            // 
            // txtBoxSendAutoSteer
            // 
            this.txtBoxSendAutoSteer.BackColor = System.Drawing.SystemColors.Window;
            this.txtBoxSendAutoSteer.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.txtBoxSendAutoSteer.Location = new System.Drawing.Point(25, 117);
            this.txtBoxSendAutoSteer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBoxSendAutoSteer.Name = "txtBoxSendAutoSteer";
            this.txtBoxSendAutoSteer.ReadOnly = true;
            this.txtBoxSendAutoSteer.Size = new System.Drawing.Size(320, 27);
            this.txtBoxSendAutoSteer.TabIndex = 65;
            // 
            // cboxASPort
            // 
            this.cboxASPort.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboxASPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxASPort.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.cboxASPort.FormattingEnabled = true;
            this.cboxASPort.Location = new System.Drawing.Point(25, 31);
            this.cboxASPort.Name = "cboxASPort";
            this.cboxASPort.Size = new System.Drawing.Size(121, 37);
            this.cboxASPort.TabIndex = 64;
            this.cboxASPort.SelectedIndexChanged += new System.EventHandler(this.cboxASPort_SelectedIndexChanged);
            // 
            // txtBoxRecvAutoSteer
            // 
            this.txtBoxRecvAutoSteer.BackColor = System.Drawing.SystemColors.Window;
            this.txtBoxRecvAutoSteer.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.txtBoxRecvAutoSteer.Location = new System.Drawing.Point(382, 117);
            this.txtBoxRecvAutoSteer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBoxRecvAutoSteer.Name = "txtBoxRecvAutoSteer";
            this.txtBoxRecvAutoSteer.ReadOnly = true;
            this.txtBoxRecvAutoSteer.Size = new System.Drawing.Size(320, 27);
            this.txtBoxRecvAutoSteer.TabIndex = 63;
            // 
            // lblCurrentAutoSteerPort
            // 
            this.lblCurrentAutoSteerPort.AutoSize = true;
            this.lblCurrentAutoSteerPort.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentAutoSteerPort.Location = new System.Drawing.Point(156, 42);
            this.lblCurrentAutoSteerPort.Name = "lblCurrentAutoSteerPort";
            this.lblCurrentAutoSteerPort.Size = new System.Drawing.Size(40, 18);
            this.lblCurrentAutoSteerPort.TabIndex = 59;
            this.lblCurrentAutoSteerPort.Text = "Port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(378, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 19);
            this.label6.TabIndex = 58;
            this.label6.Text = "From Auto Steer:";
            // 
            // btnOpenSerialAutoSteer
            // 
            this.btnOpenSerialAutoSteer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOpenSerialAutoSteer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenSerialAutoSteer.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenSerialAutoSteer.Location = new System.Drawing.Point(372, 24);
            this.btnOpenSerialAutoSteer.Name = "btnOpenSerialAutoSteer";
            this.btnOpenSerialAutoSteer.Size = new System.Drawing.Size(138, 40);
            this.btnOpenSerialAutoSteer.TabIndex = 53;
            this.btnOpenSerialAutoSteer.Text = "Connect";
            this.btnOpenSerialAutoSteer.UseVisualStyleBackColor = false;
            this.btnOpenSerialAutoSteer.Click += new System.EventHandler(this.btnOpenSerialAutoSteer_Click);
            // 
            // btnCloseSerialAutoSteer
            // 
            this.btnCloseSerialAutoSteer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCloseSerialAutoSteer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCloseSerialAutoSteer.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseSerialAutoSteer.Location = new System.Drawing.Point(560, 24);
            this.btnCloseSerialAutoSteer.Name = "btnCloseSerialAutoSteer";
            this.btnCloseSerialAutoSteer.Size = new System.Drawing.Size(138, 40);
            this.btnCloseSerialAutoSteer.TabIndex = 52;
            this.btnCloseSerialAutoSteer.Text = "Disconnect";
            this.btnCloseSerialAutoSteer.UseVisualStyleBackColor = false;
            this.btnCloseSerialAutoSteer.Click += new System.EventHandler(this.btnCloseSerialAutoSteer_Click);
            // 
            // rbtnGGA
            // 
            this.rbtnGGA.AutoSize = true;
            this.rbtnGGA.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.rbtnGGA.Location = new System.Drawing.Point(21, 62);
            this.rbtnGGA.Name = "rbtnGGA";
            this.rbtnGGA.Size = new System.Drawing.Size(65, 27);
            this.rbtnGGA.TabIndex = 69;
            this.rbtnGGA.TabStop = true;
            this.rbtnGGA.Text = "GGA";
            this.rbtnGGA.UseVisualStyleBackColor = true;
            this.rbtnGGA.CheckedChanged += new System.EventHandler(this.rbtnGGA_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbtnUBX);
            this.groupBox4.Controls.Add(this.rbtnOGI);
            this.groupBox4.Controls.Add(this.rbtnRMC);
            this.groupBox4.Controls.Add(this.rbtnGGA);
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(802, 31);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(142, 239);
            this.groupBox4.TabIndex = 70;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fix From";
            // 
            // rbtnUBX
            // 
            this.rbtnUBX.AutoSize = true;
            this.rbtnUBX.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.rbtnUBX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbtnUBX.Location = new System.Drawing.Point(21, 191);
            this.rbtnUBX.Name = "rbtnUBX";
            this.rbtnUBX.Size = new System.Drawing.Size(62, 27);
            this.rbtnUBX.TabIndex = 72;
            this.rbtnUBX.TabStop = true;
            this.rbtnUBX.Text = "UBX";
            this.rbtnUBX.UseVisualStyleBackColor = true;
            // 
            // rbtnOGI
            // 
            this.rbtnOGI.AutoSize = true;
            this.rbtnOGI.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.rbtnOGI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbtnOGI.Location = new System.Drawing.Point(21, 148);
            this.rbtnOGI.Name = "rbtnOGI";
            this.rbtnOGI.Size = new System.Drawing.Size(61, 27);
            this.rbtnOGI.TabIndex = 71;
            this.rbtnOGI.TabStop = true;
            this.rbtnOGI.Text = "OGI";
            this.rbtnOGI.UseVisualStyleBackColor = true;
            // 
            // rbtnRMC
            // 
            this.rbtnRMC.AutoSize = true;
            this.rbtnRMC.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.rbtnRMC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbtnRMC.Location = new System.Drawing.Point(21, 105);
            this.rbtnRMC.Name = "rbtnRMC";
            this.rbtnRMC.Size = new System.Drawing.Size(66, 27);
            this.rbtnRMC.TabIndex = 70;
            this.rbtnRMC.TabStop = true;
            this.rbtnRMC.Text = "RMC";
            this.rbtnRMC.UseVisualStyleBackColor = true;
            // 
            // FormCommSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(956, 649);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnRescan);
            this.Controls.Add(this.btnSerialOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormCommSet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Communication Settings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormCommSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnRescan;
        private System.Windows.Forms.Button btnSerialOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCurrentPort;
        private System.Windows.Forms.Label lblCurrentBaud;
        private System.Windows.Forms.Button btnCloseSerial;
        private System.Windows.Forms.TextBox textBoxRcv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenSerial;
        private System.Windows.Forms.ComboBox cboxBaud;
        private System.Windows.Forms.ComboBox cboxPort;
        private System.Windows.Forms.Button btnCloseSerialArduino;
        private System.Windows.Forms.Button btnOpenSerialArduino;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblCurrentArduinoPort;
        private System.Windows.Forms.TextBox txtBoxRecvArduino;
        private System.Windows.Forms.ComboBox cboxArdPort;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxSendArduino;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxSendAutoSteer;
        private System.Windows.Forms.ComboBox cboxASPort;
        private System.Windows.Forms.TextBox txtBoxRecvAutoSteer;
        private System.Windows.Forms.Label lblCurrentAutoSteerPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOpenSerialAutoSteer;
        private System.Windows.Forms.Button btnCloseSerialAutoSteer;
        private System.Windows.Forms.ComboBox cboxNMEAHz;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbtnGGA;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbtnOGI;
        private System.Windows.Forms.RadioButton rbtnRMC;
        private System.Windows.Forms.CheckBox usejrk;
        private System.Windows.Forms.RadioButton rbtnUBX;
        private System.Windows.Forms.ComboBox cboxBaud2;
        private System.Windows.Forms.ComboBox cboxPort2;
        private System.Windows.Forms.Button btnCloseSerial2;
        private System.Windows.Forms.Button btnOpenSerial2;
        private System.Windows.Forms.Label lblCurrentBaud2;
        private System.Windows.Forms.Label lblCurrentPort2;
    }
}