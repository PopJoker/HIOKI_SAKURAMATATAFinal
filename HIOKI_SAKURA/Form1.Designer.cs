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
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblHeader = new System.Windows.Forms.Label();
            this.grpEngineer = new System.Windows.Forms.GroupBox();
            this.btnExportSetting = new System.Windows.Forms.Button();
            this.btnRefreshSystem = new System.Windows.Forms.Button();
            this.lblBatchSize = new System.Windows.Forms.Label();
            this.numBatchSize = new System.Windows.Forms.NumericUpDown();
            this.lblBT_Com = new System.Windows.Forms.Label();
            this.comboBT_COM = new System.Windows.Forms.ComboBox();
            this.lblSW_Com = new System.Windows.Forms.Label();
            this.comboSW_COM = new System.Windows.Forms.ComboBox();
            this.dataGridConfigs = new System.Windows.Forms.DataGridView();
            this.colCfgName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCfgSlot = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.grpOperator = new System.Windows.Forms.GroupBox();
            this.btnImportSetting = new System.Windows.Forms.Button();
            this.lblScanCount = new System.Windows.Forms.Label();
            this.textBarcode = new System.Windows.Forms.TextBox();
            this.listScanned = new System.Windows.Forms.ListBox();
            this.btnClearBatch = new System.Windows.Forms.Button();
            this.textBoxScanResult = new System.Windows.Forms.TextBox();
            this.grpSetting = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.btnBrowe = new System.Windows.Forms.Button();
            this.tbFileLocation = new System.Windows.Forms.TextBox();
            this.picSakura = new System.Windows.Forms.PictureBox();
            this.btnUploadSet = new System.Windows.Forms.Button();
            this.grpEngineer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBatchSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridConfigs)).BeginInit();
            this.grpOperator.SuspendLayout();
            this.grpSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSakura)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.AliceBlue;
            this.lblHeader.Location = new System.Drawing.Point(86, 27);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(297, 43);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Gus Hioki Sakura";
            // 
            // grpEngineer
            // 
            this.grpEngineer.BackColor = System.Drawing.Color.White;
            this.grpEngineer.Controls.Add(this.btnExportSetting);
            this.grpEngineer.Controls.Add(this.btnRefreshSystem);
            this.grpEngineer.Controls.Add(this.lblBatchSize);
            this.grpEngineer.Controls.Add(this.numBatchSize);
            this.grpEngineer.Controls.Add(this.lblBT_Com);
            this.grpEngineer.Controls.Add(this.comboBT_COM);
            this.grpEngineer.Controls.Add(this.lblSW_Com);
            this.grpEngineer.Controls.Add(this.comboSW_COM);
            this.grpEngineer.Controls.Add(this.dataGridConfigs);
            this.grpEngineer.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.grpEngineer.ForeColor = System.Drawing.Color.Navy;
            this.grpEngineer.Location = new System.Drawing.Point(20, 80);
            this.grpEngineer.Name = "grpEngineer";
            this.grpEngineer.Size = new System.Drawing.Size(1050, 260);
            this.grpEngineer.TabIndex = 2;
            this.grpEngineer.TabStop = false;
            this.grpEngineer.Text = "工程師設定 (批次量測配置)";
            // 
            // btnExportSetting
            // 
            this.btnExportSetting.Location = new System.Drawing.Point(900, 70);
            this.btnExportSetting.Name = "btnExportSetting";
            this.btnExportSetting.Size = new System.Drawing.Size(120, 64);
            this.btnExportSetting.TabIndex = 7;
            this.btnExportSetting.Text = "Export\r\nSetting";
            this.btnExportSetting.Click += new System.EventHandler(this.btnExportSetting_Click);
            // 
            // btnRefreshSystem
            // 
            this.btnRefreshSystem.Location = new System.Drawing.Point(900, 24);
            this.btnRefreshSystem.Name = "btnRefreshSystem";
            this.btnRefreshSystem.Size = new System.Drawing.Size(120, 40);
            this.btnRefreshSystem.TabIndex = 5;
            this.btnRefreshSystem.Text = "ReFresh";
            this.btnRefreshSystem.Click += new System.EventHandler(this.btnRefreshSystem_Click);
            // 
            // lblBatchSize
            // 
            this.lblBatchSize.Location = new System.Drawing.Point(20, 35);
            this.lblBatchSize.Name = "lblBatchSize";
            this.lblBatchSize.Size = new System.Drawing.Size(130, 25);
            this.lblBatchSize.TabIndex = 0;
            this.lblBatchSize.Text = "每批需掃描數：";
            // 
            // numBatchSize
            // 
            this.numBatchSize.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.numBatchSize.Location = new System.Drawing.Point(160, 32);
            this.numBatchSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBatchSize.Name = "numBatchSize";
            this.numBatchSize.Size = new System.Drawing.Size(120, 34);
            this.numBatchSize.TabIndex = 1;
            this.numBatchSize.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numBatchSize.ValueChanged += new System.EventHandler(this.numBatchSize_ValueChanged);
            // 
            // lblBT_Com
            // 
            this.lblBT_Com.Location = new System.Drawing.Point(300, 35);
            this.lblBT_Com.Name = "lblBT_Com";
            this.lblBT_Com.Size = new System.Drawing.Size(140, 25);
            this.lblBT_Com.TabIndex = 2;
            this.lblBT_Com.Text = "BT3563A COM：";
            // 
            // comboBT_COM
            // 
            this.comboBT_COM.Location = new System.Drawing.Point(440, 32);
            this.comboBT_COM.Name = "comboBT_COM";
            this.comboBT_COM.Size = new System.Drawing.Size(120, 33);
            this.comboBT_COM.TabIndex = 3;
            // 
            // lblSW_Com
            // 
            this.lblSW_Com.Location = new System.Drawing.Point(600, 35);
            this.lblSW_Com.Name = "lblSW_Com";
            this.lblSW_Com.Size = new System.Drawing.Size(140, 25);
            this.lblSW_Com.TabIndex = 4;
            this.lblSW_Com.Text = "SW1001 COM：";
            // 
            // comboSW_COM
            // 
            this.comboSW_COM.Location = new System.Drawing.Point(740, 32);
            this.comboSW_COM.Name = "comboSW_COM";
            this.comboSW_COM.Size = new System.Drawing.Size(120, 33);
            this.comboSW_COM.TabIndex = 5;
            // 
            // dataGridConfigs
            // 
            this.dataGridConfigs.BackgroundColor = System.Drawing.Color.White;
            this.dataGridConfigs.ColumnHeadersHeight = 29;
            this.dataGridConfigs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCfgName,
            this.colCfgSlot});
            this.dataGridConfigs.Location = new System.Drawing.Point(20, 70);
            this.dataGridConfigs.Name = "dataGridConfigs";
            this.dataGridConfigs.RowHeadersWidth = 51;
            this.dataGridConfigs.Size = new System.Drawing.Size(840, 170);
            this.dataGridConfigs.TabIndex = 6;
            // 
            // colCfgName
            // 
            this.colCfgName.HeaderText = "配置名稱";
            this.colCfgName.MinimumWidth = 6;
            this.colCfgName.Name = "colCfgName";
            this.colCfgName.Width = 150;
            // 
            // colCfgSlot
            // 
            this.colCfgSlot.HeaderText = "Slot";
            this.colCfgSlot.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.colCfgSlot.MinimumWidth = 6;
            this.colCfgSlot.Name = "colCfgSlot";
            this.colCfgSlot.Width = 60;
            // 
            // grpOperator
            // 
            this.grpOperator.BackColor = System.Drawing.Color.White;
            this.grpOperator.Controls.Add(this.btnImportSetting);
            this.grpOperator.Controls.Add(this.lblScanCount);
            this.grpOperator.Controls.Add(this.textBarcode);
            this.grpOperator.Controls.Add(this.listScanned);
            this.grpOperator.Controls.Add(this.btnClearBatch);
            this.grpOperator.Controls.Add(this.textBoxScanResult);
            this.grpOperator.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.grpOperator.ForeColor = System.Drawing.Color.Navy;
            this.grpOperator.Location = new System.Drawing.Point(20, 360);
            this.grpOperator.Name = "grpOperator";
            this.grpOperator.Size = new System.Drawing.Size(1050, 260);
            this.grpOperator.TabIndex = 3;
            this.grpOperator.TabStop = false;
            this.grpOperator.Text = "作業員操作 (批次掃描模式)";
            // 
            // btnImportSetting
            // 
            this.btnImportSetting.Location = new System.Drawing.Point(900, 45);
            this.btnImportSetting.Name = "btnImportSetting";
            this.btnImportSetting.Size = new System.Drawing.Size(120, 64);
            this.btnImportSetting.TabIndex = 8;
            this.btnImportSetting.Text = "Import\r\nSetting";
            this.btnImportSetting.Click += new System.EventHandler(this.btnImportSetting_Click);
            // 
            // lblScanCount
            // 
            this.lblScanCount.Location = new System.Drawing.Point(20, 35);
            this.lblScanCount.Name = "lblScanCount";
            this.lblScanCount.Size = new System.Drawing.Size(300, 25);
            this.lblScanCount.TabIndex = 0;
            this.lblScanCount.Text = "已掃描：0 / X";
            // 
            // textBarcode
            // 
            this.textBarcode.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.textBarcode.Location = new System.Drawing.Point(20, 70);
            this.textBarcode.Name = "textBarcode";
            this.textBarcode.Size = new System.Drawing.Size(600, 39);
            this.textBarcode.TabIndex = 1;
            this.textBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBarcode_KeyDown);
            // 
            // listScanned
            // 
            this.listScanned.ItemHeight = 25;
            this.listScanned.Location = new System.Drawing.Point(20, 120);
            this.listScanned.Name = "listScanned";
            this.listScanned.Size = new System.Drawing.Size(600, 104);
            this.listScanned.TabIndex = 2;
            // 
            // btnClearBatch
            // 
            this.btnClearBatch.Location = new System.Drawing.Point(650, 70);
            this.btnClearBatch.Name = "btnClearBatch";
            this.btnClearBatch.Size = new System.Drawing.Size(120, 40);
            this.btnClearBatch.TabIndex = 3;
            this.btnClearBatch.Text = "Clear";
            this.btnClearBatch.Click += new System.EventHandler(this.btnClearBatch_Click);
            // 
            // textBoxScanResult
            // 
            this.textBoxScanResult.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.textBoxScanResult.Location = new System.Drawing.Point(650, 120);
            this.textBoxScanResult.Multiline = true;
            this.textBoxScanResult.Name = "textBoxScanResult";
            this.textBoxScanResult.ReadOnly = true;
            this.textBoxScanResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxScanResult.Size = new System.Drawing.Size(380, 104);
            this.textBoxScanResult.TabIndex = 4;
            // 
            // grpSetting
            // 
            this.grpSetting.BackColor = System.Drawing.Color.White;
            this.grpSetting.Controls.Add(this.label3);
            this.grpSetting.Controls.Add(this.label2);
            this.grpSetting.Controls.Add(this.label1);
            this.grpSetting.Controls.Add(this.tbFileName);
            this.grpSetting.Controls.Add(this.btnBrowe);
            this.grpSetting.Controls.Add(this.tbFileLocation);
            this.grpSetting.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.grpSetting.ForeColor = System.Drawing.Color.Navy;
            this.grpSetting.Location = new System.Drawing.Point(20, 640);
            this.grpSetting.Name = "grpSetting";
            this.grpSetting.Size = new System.Drawing.Size(1050, 107);
            this.grpSetting.TabIndex = 5;
            this.grpSetting.TabStop = false;
            this.grpSetting.Text = "設定區";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(279, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "→";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(308, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "檔案位置";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "檔名";
            // 
            // tbFileName
            // 
            this.tbFileName.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.tbFileName.Location = new System.Drawing.Point(20, 58);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(260, 39);
            this.tbFileName.TabIndex = 6;
            // 
            // btnBrowe
            // 
            this.btnBrowe.Location = new System.Drawing.Point(900, 57);
            this.btnBrowe.Name = "btnBrowe";
            this.btnBrowe.Size = new System.Drawing.Size(120, 40);
            this.btnBrowe.TabIndex = 5;
            this.btnBrowe.Text = "Browe";
            this.btnBrowe.Click += new System.EventHandler(this.btnBrowe_Click);
            // 
            // tbFileLocation
            // 
            this.tbFileLocation.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.tbFileLocation.Location = new System.Drawing.Point(305, 58);
            this.tbFileLocation.Name = "tbFileLocation";
            this.tbFileLocation.Size = new System.Drawing.Size(589, 39);
            this.tbFileLocation.TabIndex = 5;
            // 
            // picSakura
            // 
            this.picSakura.Image = global::HIOKI_SAKURA.Properties.Resources.ChatGPT_Image_2025年11月19日_下午02_51_12;
            this.picSakura.Location = new System.Drawing.Point(20, 10);
            this.picSakura.Name = "picSakura";
            this.picSakura.Size = new System.Drawing.Size(60, 60);
            this.picSakura.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSakura.TabIndex = 1;
            this.picSakura.TabStop = false;
            // 
            // btnUploadSet
            // 
            this.btnUploadSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            this.btnUploadSet.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.btnUploadSet.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnUploadSet.Location = new System.Drawing.Point(962, 10);
            this.btnUploadSet.Name = "btnUploadSet";
            this.btnUploadSet.Size = new System.Drawing.Size(108, 60);
            this.btnUploadSet.TabIndex = 8;
            this.btnUploadSet.Text = "Upload\r\nSet";
            this.btnUploadSet.UseVisualStyleBackColor = false;
            this.btnUploadSet.Click += new System.EventHandler(this.btnMqttSetting_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1089, 756);
            this.Controls.Add(this.btnUploadSet);
            this.Controls.Add(this.grpSetting);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.picSakura);
            this.Controls.Add(this.grpEngineer);
            this.Controls.Add(this.grpOperator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Gus Hioki Sakura";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpEngineer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numBatchSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridConfigs)).EndInit();
            this.grpOperator.ResumeLayout(false);
            this.grpOperator.PerformLayout();
            this.grpSetting.ResumeLayout(false);
            this.grpSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSakura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #region Fields
        private Label lblHeader;
        private PictureBox picSakura;

        private GroupBox grpEngineer;
        private Label lblBatchSize;
        private NumericUpDown numBatchSize;
        private Label lblBT_Com;
        private ComboBox comboBT_COM;
        private Label lblSW_Com;
        private ComboBox comboSW_COM;
        private DataGridView dataGridConfigs;
        private DataGridViewTextBoxColumn colCfgName;
        private DataGridViewComboBoxColumn colCfgSlot;

        private GroupBox grpOperator;
        private Label lblScanCount;
        private ListBox listScanned;
        private TextBox textBarcode;
        private Button btnClearBatch;
        private TextBox textBoxScanResult;
        #endregion

        private Button btnRefreshSystem;
        private GroupBox grpSetting;
        private Button btnBrowe;
        private TextBox tbFileLocation;
        private Button btnExportSetting;
        private Button btnImportSetting;
        private Label label1;
        private TextBox tbFileName;
        private Label label3;
        private Label label2;
        private Button btnUploadSet;
    }
}
