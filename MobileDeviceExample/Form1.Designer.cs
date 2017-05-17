namespace MobileDeviceExample
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnReload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DrviceName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DeviceSerial = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DeviceVersion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DeviceModelNumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ActivationState = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DeviceBuildVersion = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DeviceBasebandBootloaderVersion = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.DeviceBasebandVersion = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.DeviceFirmwareVersion = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.DeviceId = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.DeviceName = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.DevicePhoneNumber = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.DeviceProductType = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.DeviceSIMStatus = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.DeviceWiFiAddress = new System.Windows.Forms.Label();
            this.StateLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button4 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.DeviceColor = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(176, 382);
            this.btnReload.Margin = new System.Windows.Forms.Padding(4);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(112, 34);
            this.btnReload.TabIndex = 1;
            this.btnReload.Text = "刷新状态";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "设备名";
            // 
            // DrviceName
            // 
            this.DrviceName.AutoSize = true;
            this.DrviceName.Location = new System.Drawing.Point(172, 64);
            this.DrviceName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DrviceName.Name = "DrviceName";
            this.DrviceName.Size = new System.Drawing.Size(62, 18);
            this.DrviceName.TabIndex = 3;
            this.DrviceName.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 87);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "序列号";
            // 
            // DeviceSerial
            // 
            this.DeviceSerial.AutoSize = true;
            this.DeviceSerial.Location = new System.Drawing.Point(172, 87);
            this.DeviceSerial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DeviceSerial.Name = "DeviceSerial";
            this.DeviceSerial.Size = new System.Drawing.Size(116, 18);
            this.DeviceSerial.TabIndex = 5;
            this.DeviceSerial.Text = "DeviceSerial";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 110);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "iOS版本";
            // 
            // DeviceVersion
            // 
            this.DeviceVersion.AutoSize = true;
            this.DeviceVersion.Location = new System.Drawing.Point(172, 110);
            this.DeviceVersion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.DeviceVersion.Name = "DeviceVersion";
            this.DeviceVersion.Size = new System.Drawing.Size(62, 18);
            this.DeviceVersion.TabIndex = 7;
            this.DeviceVersion.Text = "label4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 132);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "型号";
            // 
            // DeviceModelNumber
            // 
            this.DeviceModelNumber.AutoSize = true;
            this.DeviceModelNumber.Location = new System.Drawing.Point(172, 132);
            this.DeviceModelNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.DeviceModelNumber.Name = "DeviceModelNumber";
            this.DeviceModelNumber.Size = new System.Drawing.Size(62, 18);
            this.DeviceModelNumber.TabIndex = 9;
            this.DeviceModelNumber.Text = "label5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 154);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "激活状态";
            // 
            // ActivationState
            // 
            this.ActivationState.AutoSize = true;
            this.ActivationState.Location = new System.Drawing.Point(172, 154);
            this.ActivationState.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.ActivationState.Name = "ActivationState";
            this.ActivationState.Size = new System.Drawing.Size(62, 18);
            this.ActivationState.TabIndex = 11;
            this.ActivationState.Text = "label6";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 177);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 12;
            this.label6.Text = "内部版本";
            // 
            // DeviceBuildVersion
            // 
            this.DeviceBuildVersion.AutoSize = true;
            this.DeviceBuildVersion.Location = new System.Drawing.Point(172, 177);
            this.DeviceBuildVersion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.DeviceBuildVersion.Name = "DeviceBuildVersion";
            this.DeviceBuildVersion.Size = new System.Drawing.Size(62, 18);
            this.DeviceBuildVersion.TabIndex = 13;
            this.DeviceBuildVersion.Text = "label7";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 200);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 18);
            this.label7.TabIndex = 14;
            this.label7.Text = "基带引导版本";
            // 
            // DeviceBasebandBootloaderVersion
            // 
            this.DeviceBasebandBootloaderVersion.AutoSize = true;
            this.DeviceBasebandBootloaderVersion.Location = new System.Drawing.Point(172, 200);
            this.DeviceBasebandBootloaderVersion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.DeviceBasebandBootloaderVersion.Name = "DeviceBasebandBootloaderVersion";
            this.DeviceBasebandBootloaderVersion.Size = new System.Drawing.Size(62, 18);
            this.DeviceBasebandBootloaderVersion.TabIndex = 15;
            this.DeviceBasebandBootloaderVersion.Text = "label8";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 224);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 16;
            this.label8.Text = "基带版本";
            // 
            // DeviceBasebandVersion
            // 
            this.DeviceBasebandVersion.AutoSize = true;
            this.DeviceBasebandVersion.Location = new System.Drawing.Point(172, 224);
            this.DeviceBasebandVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DeviceBasebandVersion.Name = "DeviceBasebandVersion";
            this.DeviceBasebandVersion.Size = new System.Drawing.Size(62, 18);
            this.DeviceBasebandVersion.TabIndex = 17;
            this.DeviceBasebandVersion.Text = "label9";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(42, 246);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 18);
            this.label9.TabIndex = 18;
            this.label9.Text = "iBoot版本";
            // 
            // DeviceFirmwareVersion
            // 
            this.DeviceFirmwareVersion.AutoSize = true;
            this.DeviceFirmwareVersion.Location = new System.Drawing.Point(172, 246);
            this.DeviceFirmwareVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DeviceFirmwareVersion.Name = "DeviceFirmwareVersion";
            this.DeviceFirmwareVersion.Size = new System.Drawing.Size(71, 18);
            this.DeviceFirmwareVersion.TabIndex = 19;
            this.DeviceFirmwareVersion.Text = "label10";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(45, 270);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 18);
            this.label10.TabIndex = 20;
            this.label10.Text = "UDID";
            // 
            // DeviceId
            // 
            this.DeviceId.AutoSize = true;
            this.DeviceId.Location = new System.Drawing.Point(172, 270);
            this.DeviceId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DeviceId.Name = "DeviceId";
            this.DeviceId.Size = new System.Drawing.Size(71, 18);
            this.DeviceId.TabIndex = 21;
            this.DeviceId.Text = "label11";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(45, 294);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 18);
            this.label11.TabIndex = 22;
            this.label11.Text = "设备名";
            // 
            // DeviceName
            // 
            this.DeviceName.AutoSize = true;
            this.DeviceName.Location = new System.Drawing.Point(172, 294);
            this.DeviceName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DeviceName.Name = "DeviceName";
            this.DeviceName.Size = new System.Drawing.Size(71, 18);
            this.DeviceName.TabIndex = 23;
            this.DeviceName.Text = "label12";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(48, 318);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 18);
            this.label12.TabIndex = 24;
            this.label12.Text = "电话号码";
            // 
            // DevicePhoneNumber
            // 
            this.DevicePhoneNumber.AutoSize = true;
            this.DevicePhoneNumber.Location = new System.Drawing.Point(172, 318);
            this.DevicePhoneNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DevicePhoneNumber.Name = "DevicePhoneNumber";
            this.DevicePhoneNumber.Size = new System.Drawing.Size(71, 18);
            this.DevicePhoneNumber.TabIndex = 25;
            this.DevicePhoneNumber.Text = "label13";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(45, 342);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 18);
            this.label13.TabIndex = 26;
            this.label13.Text = "产品版本";
            // 
            // DeviceProductType
            // 
            this.DeviceProductType.AutoSize = true;
            this.DeviceProductType.Location = new System.Drawing.Point(172, 342);
            this.DeviceProductType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DeviceProductType.Name = "DeviceProductType";
            this.DeviceProductType.Size = new System.Drawing.Size(71, 18);
            this.DeviceProductType.TabIndex = 27;
            this.DeviceProductType.Text = "label14";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(428, 64);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 18);
            this.label14.TabIndex = 28;
            this.label14.Text = "SIM卡状态";
            // 
            // DeviceSIMStatus
            // 
            this.DeviceSIMStatus.AutoSize = true;
            this.DeviceSIMStatus.Location = new System.Drawing.Point(586, 64);
            this.DeviceSIMStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DeviceSIMStatus.Name = "DeviceSIMStatus";
            this.DeviceSIMStatus.Size = new System.Drawing.Size(71, 18);
            this.DeviceSIMStatus.TabIndex = 29;
            this.DeviceSIMStatus.Text = "label15";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(430, 88);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 18);
            this.label15.TabIndex = 30;
            this.label15.Text = "WIFI地址";
            // 
            // DeviceWiFiAddress
            // 
            this.DeviceWiFiAddress.AutoSize = true;
            this.DeviceWiFiAddress.Location = new System.Drawing.Point(586, 88);
            this.DeviceWiFiAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DeviceWiFiAddress.Name = "DeviceWiFiAddress";
            this.DeviceWiFiAddress.Size = new System.Drawing.Size(71, 18);
            this.DeviceWiFiAddress.TabIndex = 31;
            this.DeviceWiFiAddress.Text = "label16";
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.Location = new System.Drawing.Point(48, 390);
            this.StateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(53, 18);
            this.StateLabel.TabIndex = 32;
            this.StateLabel.Text = "State";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(51, 428);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(134, 34);
            this.button4.TabIndex = 37;
            this.button4.Text = "进入恢复模式";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(195, 428);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(142, 34);
            this.button7.TabIndex = 41;
            this.button7.Text = "离开恢复模式";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // DeviceColor
            // 
            this.DeviceColor.AutoSize = true;
            this.DeviceColor.Location = new System.Drawing.Point(584, 110);
            this.DeviceColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DeviceColor.Name = "DeviceColor";
            this.DeviceColor.Size = new System.Drawing.Size(71, 18);
            this.DeviceColor.TabIndex = 44;
            this.DeviceColor.Text = "label16";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(428, 110);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 18);
            this.label17.TabIndex = 43;
            this.label17.Text = "颜色";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 480);
            this.Controls.Add(this.DeviceColor);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.StateLabel);
            this.Controls.Add(this.DeviceWiFiAddress);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.DeviceSIMStatus);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.DeviceProductType);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.DevicePhoneNumber);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.DeviceName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.DeviceId);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.DeviceFirmwareVersion);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.DeviceBasebandVersion);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.DeviceBasebandBootloaderVersion);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DeviceBuildVersion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ActivationState);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DeviceModelNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DeviceVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DeviceSerial);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DrviceName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReload);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DrviceName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label DeviceSerial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label DeviceVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label DeviceModelNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ActivationState;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label DeviceBuildVersion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label DeviceBasebandBootloaderVersion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label DeviceBasebandVersion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label DeviceFirmwareVersion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label DeviceId;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label DeviceName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label DevicePhoneNumber;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label DeviceProductType;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label DeviceSIMStatus;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label DeviceWiFiAddress;
        private System.Windows.Forms.Label StateLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label DeviceColor;
        private System.Windows.Forms.Label label17;
    }
}

