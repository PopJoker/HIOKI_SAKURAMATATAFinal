using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace HIOKI_SAKURA
{
    public partial class FormChannelSetting : Form
    {
        public Dictionary<int, (bool V, bool R)> ChannelSettings { get; private set; }

        private Dictionary<int, (CheckBox V, CheckBox R)> checkBoxMap = new Dictionary<int, (CheckBox V, CheckBox R)>();

        public FormChannelSetting(Dictionary<int, (bool V, bool R)> currentSettings = null)
        {
            InitializeComponent();
            ChannelSettings = new Dictionary<int, (bool V, bool R)>();
            SetupTableLayoutColumns();
            InitializeDynamicChannels(currentSettings);
            this.btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            ChannelSettings.Clear();
            foreach (var kvp in checkBoxMap)
            {
                int ch = kvp.Key;
                var (V, R) = kvp.Value;
                ChannelSettings[ch] = (V.Checked, R.Checked);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SetupTableLayoutColumns()
        {
            this.tableLayoutPanel1.ColumnStyles.Clear();
            this.tableLayoutPanel1.ColumnCount = 5;

            // 設定欄位對齊
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F)); // Channel Label 改大
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 48F)); // V Label
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));   // V CheckBox
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 48F)); // R Label
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));   // R CheckBox
        }

        private void InitializeDynamicChannels(Dictionary<int, (bool V, bool R)> currentSettings = null)
        {
            for (int i = 0; i < 20; i++)
            {
                this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                // Channel Label
                Label lblCh = new Label()
                {
                    Text = $"Ch{i + 1}",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                this.tableLayoutPanel1.Controls.Add(lblCh, 0, i);

                // V Label & CheckBox
                Label lblV = new Label() { Text = "V", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
                CheckBox cbV = new CheckBox() { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter };
                this.tableLayoutPanel1.Controls.Add(lblV, 1, i);
                this.tableLayoutPanel1.Controls.Add(cbV, 2, i);

                // R Label & CheckBox
                Label lblR = new Label() { Text = "R", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
                CheckBox cbR = new CheckBox() { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter };
                this.tableLayoutPanel1.Controls.Add(lblR, 3, i);
                this.tableLayoutPanel1.Controls.Add(cbR, 4, i);

                // 套用傳入設定
                if (currentSettings != null && currentSettings.TryGetValue(i + 1, out var cfg))
                {
                    cbV.Checked = cfg.V;
                    cbR.Checked = cfg.R;
                }

                checkBoxMap[i + 1] = (cbV, cbR);
            }
        }
    }
}
