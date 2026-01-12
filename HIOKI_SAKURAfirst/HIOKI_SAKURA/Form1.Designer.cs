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
            // --- SW1001 ---
            this.lblCOM_SW = new Label();
            this.comboBoxCOM_SW = new ComboBox();
            this.lblSlot = new Label();
            this.comboBoxSlot = new ComboBox();
            this.lblChannel = new Label();
            this.comboBoxChannel = new ComboBox();
            this.btnTest = new Button();
            this.btnStartScan = new Button();
            this.textBoxScanResult = new TextBox();

            // --- BT3563A ---
            this.lblCOM_BT = new Label();
            this.comboBoxCOM_BT = new ComboBox();
            this.btnStart = new Button();
            this.btnStop = new Button();
            this.textBoxBT = new TextBox();

            this.SuspendLayout();

            // --- lblCOM_SW ---
            this.lblCOM_SW.Location = new Point(20, 20);
            this.lblCOM_SW.Size = new Size(120, 30);
            this.lblCOM_SW.Text = "SW1001 COM:";

            // --- comboBoxCOM_SW ---
            this.comboBoxCOM_SW.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxCOM_SW.Location = new Point(150, 20);
            this.comboBoxCOM_SW.Size = new Size(120, 30);

            // --- lblSlot ---
            this.lblSlot.Location = new Point(290, 20);
            this.lblSlot.Size = new Size(50, 30);
            this.lblSlot.Text = "Slot:";

            // --- comboBoxSlot ---
            this.comboBoxSlot.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxSlot.Items.AddRange(new object[] { "1", "2", "3" });
            this.comboBoxSlot.Location = new Point(350, 20);
            this.comboBoxSlot.Size = new Size(60, 30);

            // --- lblChannel ---
            this.lblChannel.Location = new Point(420, 20);
            this.lblChannel.Size = new Size(70, 30);
            this.lblChannel.Text = "Channel:";

            // --- comboBoxChannel ---
            this.comboBoxChannel.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxChannel.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8" });
            this.comboBoxChannel.Location = new Point(500, 20);
            this.comboBoxChannel.Size = new Size(60, 30);

            // --- btnTest ---
            this.btnTest.Location = new Point(580, 20);
            this.btnTest.Size = new Size(120, 30);
            this.btnTest.Text = "Test Connection";
            this.btnTest.Click += new EventHandler(this.btnTest_Click);

            // --- btnStartScan ---
            this.btnStartScan.Location = new Point(580, 60);
            this.btnStartScan.Size = new Size(120, 30);
            this.btnStartScan.Text = "Start Scan";
            this.btnStartScan.Click += new EventHandler(this.btnStartScan_Click);

            // --- textBoxScanResult ---
            this.textBoxScanResult.Location = new Point(20, 100);
            this.textBoxScanResult.Size = new Size(680, 250);
            this.textBoxScanResult.Multiline = true;
            this.textBoxScanResult.ScrollBars = ScrollBars.Vertical;
            this.textBoxScanResult.ReadOnly = true;

            // --- lblCOM_BT ---
            this.lblCOM_BT.Location = new Point(20, 370);
            this.lblCOM_BT.Size = new Size(120, 30);
            this.lblCOM_BT.Text = "BT3563A COM:";

            // --- comboBoxCOM_BT ---
            this.comboBoxCOM_BT.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxCOM_BT.Location = new Point(150, 370);
            this.comboBoxCOM_BT.Size = new Size(120, 30);

            // --- btnStart ---
            this.btnStart.Location = new Point(500, 370);
            this.btnStart.Size = new Size(90, 30);
            this.btnStart.Text = "Start Test";
            this.btnStart.Click += new EventHandler(this.btnStart_Click);

            // --- btnStop ---
            this.btnStop.Location = new Point(600, 370);
            this.btnStop.Size = new Size(90, 30);
            this.btnStop.Text = "Stop Test";
            this.btnStop.Click += new EventHandler(this.btnStop_Click);

            // --- textBoxBT ---
            this.textBoxBT.Location = new Point(20, 410);
            this.textBoxBT.Size = new Size(680, 200);
            this.textBoxBT.Multiline = true;
            this.textBoxBT.ScrollBars = ScrollBars.Vertical;
            this.textBoxBT.ReadOnly = true;

            // --- Form1 ---
            this.ClientSize = new Size(730, 630);
            this.Controls.Add(this.lblCOM_SW);
            this.Controls.Add(this.comboBoxCOM_SW);
            this.Controls.Add(this.lblSlot);
            this.Controls.Add(this.comboBoxSlot);
            this.Controls.Add(this.lblChannel);
            this.Controls.Add(this.comboBoxChannel);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnStartScan);
            this.Controls.Add(this.textBoxScanResult);
            this.Controls.Add(this.lblCOM_BT);
            this.Controls.Add(this.comboBoxCOM_BT);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.textBoxBT);

            this.Font = new Font("微軟正黑體", 12F);
            this.Name = "Form1";
            this.Text = "SW1001 & BT3563A Test Form";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // --- 元件宣告 ---
        private Label lblCOM_SW;
        private ComboBox comboBoxCOM_SW;
        private Label lblSlot;
        private ComboBox comboBoxSlot;
        private Label lblChannel;
        private ComboBox comboBoxChannel;
        private Button btnTest;
        private Button btnStartScan;
        private TextBox textBoxScanResult;

        private Label lblCOM_BT;
        private ComboBox comboBoxCOM_BT;
        private Button btnStart;
        private Button btnStop;
        private TextBox textBoxBT;
    }
}
