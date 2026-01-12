using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIOKI_SAKURA
{
    public partial class Form1 : Form
    {
        private HIOKI sw1001;
        private HIOKI bt3563a;
        private CancellationTokenSource scanCts;

        public Form1()
        {
            InitializeComponent();
            LoadAvailableCOMPorts();
        }

        private void LoadAvailableCOMPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            comboBoxCOM_SW.Items.Clear();
            comboBoxCOM_BT.Items.Clear();

            foreach (string port in ports)
            {
                comboBoxCOM_SW.Items.Add(port);
                comboBoxCOM_BT.Items.Add(port);
            }

            if (comboBoxCOM_SW.Items.Count > 0) comboBoxCOM_SW.SelectedIndex = 0;
            if (comboBoxCOM_BT.Items.Count > 0) comboBoxCOM_BT.SelectedIndex = 0;
        }

        // ===========================
        // SW1001 單通道測試
        // ===========================
        private void btnTest_Click(object sender, EventArgs e)
        {
            string comPort = comboBoxCOM_SW.SelectedItem?.ToString();
            string slotText = comboBoxSlot.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(comPort) || string.IsNullOrEmpty(slotText))
            {
                MessageBox.Show("請先選擇 SW1001 COM Port 和 Slot");
                return;
            }

            try
            {
                sw1001 = new HIOKI(comPort, 115200, DeviceType.SW1001);
                string id = sw1001.GetID();
                textBoxScanResult.Text = $"Slot {slotText} Response:\r\n{id}";
                sw1001.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SW1001 連線錯誤: {ex.Message}");
            }
        }

        // ===========================
        // BT3563A 控制
        // ===========================
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (bt3563a != null)
            {
                MessageBox.Show("BT3563A 已經啟動");
                return;
            }

            string comPortBT = comboBoxCOM_BT.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(comPortBT))
            {
                MessageBox.Show("請先選擇 BT3563A COM Port");
                return;
            }

            try
            {
                bt3563a = new HIOKI(comPortBT, 9600, DeviceType.BT3563A);
                string volt = bt3563a.MeasureVoltage();
                string res = bt3563a.MeasureResistance();
                AppendTextSafe(textBoxBT, $"BT3563A 啟動: Voltage={volt}, Resistance={res}\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"BT3563A 連線錯誤: {ex.Message}");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            // 停止 BT3563A
            if (bt3563a != null)
            {
                bt3563a.Close();
                bt3563a = null;
                AppendTextSafe(textBoxBT, "BT3563A 測試停止\r\n");
            }

            // 停止 SW1001 掃描
            scanCts?.Cancel();
        }

        // ===========================
        // SW1001 多通道掃描
        // ===========================
        private void btnStartScan_Click(object sender, EventArgs e)
        {
            string comPort = comboBoxCOM_SW.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(comPort))
            {
                MessageBox.Show("請先選擇 SW1001 COM Port");
                return;
            }

            int slot = int.Parse(comboBoxSlot.SelectedItem.ToString());
            // 多通道掃描範例: Slot=slot, Channels 1~3
            int[] slots = new int[] { slot, slot, slot };
            int[] channels = new int[] { 1, 2, 3 };

            scanCts = new CancellationTokenSource();
            CancellationToken token = scanCts.Token;

            Task.Run(() =>
            {
                try
                {
                    sw1001 = new HIOKI(comPort, 115200, DeviceType.SW1001);
                    sw1001.SetupScan(slots, channels);
                    AppendTextSafe(textBoxScanResult, "多通道掃描啟動...\r\n");

                    while (!token.IsCancellationRequested)
                    {
                        string volt = sw1001.ReadScanVoltage();
                        AppendTextSafe(textBoxScanResult, $"掃描電壓結果: {volt}\r\n");
                        Thread.Sleep(500); // 500ms 間隔
                    }
                    sw1001.Close();
                    AppendTextSafe(textBoxScanResult, "掃描停止\r\n");
                }
                catch (Exception ex)
                {
                    AppendTextSafe(textBoxScanResult, $"掃描錯誤: {ex.Message}\r\n");
                }
            });
        }

        // ===========================
        // UI Thread 安全更新
        // ===========================
        private void AppendTextSafe(TextBox tb, string text)
        {
            if (tb.InvokeRequired)
                tb.Invoke(new Action(() => tb.AppendText(text)));
            else
                tb.AppendText(text);
        }
    }
}
