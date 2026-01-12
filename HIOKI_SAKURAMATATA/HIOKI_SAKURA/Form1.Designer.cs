using System;
using System.Drawing;
using System.Windows.Forms;

namespace HIOKI_SAKURA
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼
        private void InitializeComponent()
        {
            this.grpSW1001 = new System.Windows.Forms.GroupBox();
            this.lblCOM_SW = new System.Windows.Forms.Label();
            this.comboBoxCOM_SW = new System.Windows.Forms.ComboBox();
            this.lblSlot = new System.Windows.Forms.Label();
            this.comboBoxSlot = new System.Windows.Forms.ComboBox();
            this.lblChannel = new System.Windows.Forms.Label();
            this.comboBoxChannel = new System.Windows.Forms.ComboBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnStartScan = new System.Windows.Forms.Button();
            this.btnStopScan = new System.Windows.Forms.Button();
            this.btnExportCSV = new System.Windows.Forms.Button();
            this.textBoxScanResult = new System.Windows.Forms.TextBox();
            this.grpBT3563A = new System.Windows.Forms.GroupBox();
            this.lblCOM_BT = new System.Windows.Forms.Label();
            this.comboBoxCOM_BT = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.textBoxBT = new System.Windows.Forms.TextBox();
            this.grpSW1001.SuspendLayout();
            this.grpBT3563A.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSW1001
            // 
            this.grpSW1001.Controls.Add(this.lblCOM_SW);
            this.grpSW1001.Controls.Add(this.comboBoxCOM_SW);
            this.grpSW1001.Controls.Add(this.lblSlot);
            this.grpSW1001.Controls.Add(this.comboBoxSlot);
            this.grpSW1001.Controls.Add(this.lblChannel);
            this.grpSW1001.Controls.Add(this.comboBoxChannel);
            this.grpSW1001.Controls.Add(this.btnTest);
            this.grpSW1001.Controls.Add(this.btnStartScan);
            this.grpSW1001.Controls.Add(this.btnStopScan);
            this.grpSW1001.Controls.Add(this.btnExportCSV);
            this.grpSW1001.Controls.Add(this.textBoxScanResult);
            this.grpSW1001.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.grpSW1001.Location = new System.Drawing.Point(10, 10);
            this.grpSW1001.Name = "grpSW1001";
            this.grpSW1001.Size = new System.Drawing.Size(710, 370);
            this.grpSW1001.TabIndex = 0;
            this.grpSW1001.TabStop = false;
            this.grpSW1001.Text = "SW1001 控制區";
            // 
            // lblCOM_SW
            // 
            this.lblCOM_SW.Location = new System.Drawing.Point(10, 30);
            this.lblCOM_SW.Name = "lblCOM_SW";
            this.lblCOM_SW.Size = new System.Drawing.Size(120, 30);
            this.lblCOM_SW.TabIndex = 0;
            this.lblCOM_SW.Text = "COM Port:";
            // 
            // comboBoxCOM_SW
            // 
            this.comboBoxCOM_SW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCOM_SW.Location = new System.Drawing.Point(140, 30);
            this.comboBoxCOM_SW.Name = "comboBoxCOM_SW";
            this.comboBoxCOM_SW.Size = new System.Drawing.Size(120, 33);
            this.comboBoxCOM_SW.TabIndex = 1;
            // 
            // lblSlot
            // 
            this.lblSlot.Location = new System.Drawing.Point(266, 30);
            this.lblSlot.Name = "lblSlot";
            this.lblSlot.Size = new System.Drawing.Size(64, 30);
            this.lblSlot.TabIndex = 2;
            this.lblSlot.Text = "Slot:";
            // 
            // comboBoxSlot
            // 
            this.comboBoxSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSlot.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.comboBoxSlot.Location = new System.Drawing.Point(331, 30);
            this.comboBoxSlot.Name = "comboBoxSlot";
            this.comboBoxSlot.Size = new System.Drawing.Size(60, 33);
            this.comboBoxSlot.TabIndex = 3;
            // 
            // lblChannel
            // 
            this.lblChannel.Location = new System.Drawing.Point(397, 30);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(97, 30);
            this.lblChannel.TabIndex = 4;
            this.lblChannel.Text = "Channel:";
            // 
            // comboBoxChannel
            // 
            this.comboBoxChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChannel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.comboBoxChannel.Location = new System.Drawing.Point(500, 30);
            this.comboBoxChannel.Name = "comboBoxChannel";
            this.comboBoxChannel.Size = new System.Drawing.Size(60, 33);
            this.comboBoxChannel.TabIndex = 5;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(580, 30);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(120, 30);
            this.btnTest.TabIndex = 6;
            this.btnTest.Text = "Test Connection";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnStartScan
            // 
            this.btnStartScan.Location = new System.Drawing.Point(580, 70);
            this.btnStartScan.Name = "btnStartScan";
            this.btnStartScan.Size = new System.Drawing.Size(120, 30);
            this.btnStartScan.TabIndex = 7;
            this.btnStartScan.Text = "Start Scan";
            this.btnStartScan.Click += new System.EventHandler(this.btnStartScan_Click);
            // 
            // btnStopScan
            // 
            this.btnStopScan.Location = new System.Drawing.Point(580, 110);
            this.btnStopScan.Name = "btnStopScan";
            this.btnStopScan.Size = new System.Drawing.Size(120, 30);
            this.btnStopScan.TabIndex = 8;
            this.btnStopScan.Text = "Stop Scan";
            this.btnStopScan.Click += new System.EventHandler(this.btnStopScan_Click);
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.Location = new System.Drawing.Point(580, 150);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(120, 30);
            this.btnExportCSV.TabIndex = 9;
            this.btnExportCSV.Text = "Export CSV";
            this.btnExportCSV.Click += new System.EventHandler(this.btnExportCSV_Click);
            // 
            // textBoxScanResult
            // 
            this.textBoxScanResult.Location = new System.Drawing.Point(10, 70);
            this.textBoxScanResult.Multiline = true;
            this.textBoxScanResult.Name = "textBoxScanResult";
            this.textBoxScanResult.ReadOnly = true;
            this.textBoxScanResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxScanResult.Size = new System.Drawing.Size(550, 280);
            this.textBoxScanResult.TabIndex = 10;
            // 
            // grpBT3563A
            // 
            this.grpBT3563A.Controls.Add(this.lblCOM_BT);
            this.grpBT3563A.Controls.Add(this.comboBoxCOM_BT);
            this.grpBT3563A.Controls.Add(this.btnStart);
            this.grpBT3563A.Controls.Add(this.btnStop);
            this.grpBT3563A.Controls.Add(this.textBoxBT);
            this.grpBT3563A.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.grpBT3563A.Location = new System.Drawing.Point(10, 390);
            this.grpBT3563A.Name = "grpBT3563A";
            this.grpBT3563A.Size = new System.Drawing.Size(710, 230);
            this.grpBT3563A.TabIndex = 1;
            this.grpBT3563A.TabStop = false;
            this.grpBT3563A.Text = "BT3563A 控制區";
            // 
            // lblCOM_BT
            // 
            this.lblCOM_BT.Location = new System.Drawing.Point(10, 30);
            this.lblCOM_BT.Name = "lblCOM_BT";
            this.lblCOM_BT.Size = new System.Drawing.Size(120, 30);
            this.lblCOM_BT.TabIndex = 0;
            this.lblCOM_BT.Text = "COM Port:";
            // 
            // comboBoxCOM_BT
            // 
            this.comboBoxCOM_BT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCOM_BT.Location = new System.Drawing.Point(140, 30);
            this.comboBoxCOM_BT.Name = "comboBoxCOM_BT";
            this.comboBoxCOM_BT.Size = new System.Drawing.Size(120, 33);
            this.comboBoxCOM_BT.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(500, 30);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(90, 30);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start Test";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(600, 30);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(90, 30);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop Test";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // textBoxBT
            // 
            this.textBoxBT.Location = new System.Drawing.Point(10, 70);
            this.textBoxBT.Multiline = true;
            this.textBoxBT.Name = "textBoxBT";
            this.textBoxBT.ReadOnly = true;
            this.textBoxBT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBT.Size = new System.Drawing.Size(680, 150);
            this.textBoxBT.TabIndex = 4;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(730, 630);
            this.Controls.Add(this.grpSW1001);
            this.Controls.Add(this.grpBT3563A);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.Name = "Form1";
            this.Text = "SW1001 & BT3563A 自動量測";
            this.grpSW1001.ResumeLayout(false);
            this.grpSW1001.PerformLayout();
            this.grpBT3563A.ResumeLayout(false);
            this.grpBT3563A.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        // ======== 元件宣告 ========
        private GroupBox grpSW1001;
        private Label lblCOM_SW;
        private ComboBox comboBoxCOM_SW;
        private Label lblSlot;
        private ComboBox comboBoxSlot;
        private Label lblChannel;
        private ComboBox comboBoxChannel;
        private Button btnTest;
        private Button btnStartScan;
        private Button btnStopScan;
        private Button btnExportCSV;
        private TextBox textBoxScanResult;

        private GroupBox grpBT3563A;
        private Label lblCOM_BT;
        private ComboBox comboBoxCOM_BT;
        private Button btnStart;
        private Button btnStop;
        private TextBox textBoxBT;
    }
}
