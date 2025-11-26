using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HIOKI_SAKURA
{
    public partial class Form1 : Form
    {
        private bool engineerMode = false;
        private HIOKI sw1001;
        private HIOKI bt3563a;
        private CancellationTokenSource scanCts;
        private Dictionary<string, string> barcodeConfigMap = new Dictionary<string, string>();

        private StringBuilder scanData = new StringBuilder();
        private string sqliteFile;

        private MqttPublisher mqtt;
        private string currentBroker;
        private int currentPort;
        private string currentUsername;
        private string currentPassword;

        // 存放每個 Config 的 Channel 設定
        private Dictionary<string, Dictionary<int, (bool V, bool R)>> configChannelMap= new Dictionary<string, Dictionary<int, (bool V, bool R)>>();

        public Form1()
        {
            InitializeComponent();
            LoadAvailableCOMPorts();
            lblScanCount.Text = $"已掃描：0 / {numBatchSize.Value}";
            // 設定 DB 路徑到 AppData
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folder = Path.Combine(appData, "HIOKI_SAKURA");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            sqliteFile = Path.Combine(folder, "ScanResult.db");
            InitSQLite();
            lblScanCount.Text = $"已掃描：0 / {numBatchSize.Value}";
            tbFileLocation.Text= AppDomain.CurrentDomain.BaseDirectory;
            InitializeEngineerMode();
            // 註冊 KeyDown 事件
            this.KeyPreview = true; // 讓 Form 先接收按鍵事件
            this.KeyDown += Form1_KeyDown;
            LoadMqttSettings();
        }

        private void LoadMqttSettings()
        {
            currentBroker = Properties.Settings.Default.MqttBroker;
            currentPort = Properties.Settings.Default.MqttPort;
            currentUsername = Properties.Settings.Default.MqttUsername;
            currentPassword = Properties.Settings.Default.MqttPassword;
        }

        private void InitializeEngineerMode()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl != btnBrowe && ctrl != textBarcode && ctrl != btnClearBatch && ctrl != lblHeader && ctrl != grpOperator && ctrl != grpSetting)
                    ctrl.Enabled = false;
            }

        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl + Shift + E
            if (e.Control && e.Shift && e.KeyCode == Keys.E)
            {
                if (engineerMode == false)
                {
                    PromptEngineerPassword();
                }
                else {
                    InitializeEngineerMode();
                    engineerMode = false;
                }
            }
        }

        private void PromptEngineerPassword()
        {
            using (var inputForm = new FormPassword())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    if (inputForm.Password == "0314")
                    {
                        UnlockEngineerMode();
                        engineerMode = true;
                    }
                    else
                    {
                        MessageBox.Show("密碼錯誤！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void UnlockEngineerMode()
        {
            engineerMode = true;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = true;
            }

            MessageBox.Show("工程模式已解鎖！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadAvailableCOMPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            comboSW_COM.Items.Clear();
            comboBT_COM.Items.Clear();

            foreach (string port in ports)
            {
                comboSW_COM.Items.Add(port);
                comboBT_COM.Items.Add(port);
            }

            if (comboSW_COM.Items.Count > 0) comboSW_COM.SelectedIndex = 0;
            if (comboBT_COM.Items.Count > 0) comboBT_COM.SelectedIndex = 0;
        }

        private void InitSQLite()
        {
            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={sqliteFile}"))
            {
                conn.Open();
                string sql = @"
        CREATE TABLE IF NOT EXISTS ScanResult (
            Barcode TEXT,
            Slot TEXT,
            Channel TEXT,
            Voltage REAL,
            Current REAL,
            Resistance REAL,
            Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP
        )";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ===========================
        // Barcode 批次掃描事件
        // ===========================
        private void textBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barcode = textBarcode.Text.Trim();
                if (!string.IsNullOrEmpty(barcode))
                {
                    textBarcode.Clear();

                    AddItemSafe(listScanned, barcode);
                    UpdateLabelSafe(lblScanCount, $"已掃描：{listScanned.Items.Count} / {numBatchSize.Value}");
                }

                e.Handled = true;
                e.SuppressKeyPress = true;

                if (listScanned.Items.Count >= numBatchSize.Value)
                {
                    StartBatchScan();
                }
            }
        }

        private bool isScanning = false; // 掃描鎖定旗標

        private void StartBatchScan()
        {
            if (isScanning)
            {
                MessageBox.Show("批次掃描正在進行中！");
                return;
            }

            int configCount = dataGridConfigs.Rows.Count - 1; // 扣掉最後空白列
            int barcodeCount = listScanned.Items.Count;

            if (barcodeCount != configCount)
            {
                MessageBox.Show($"批次掃描數量不正確！\r\n目前有 {barcodeCount} 個 barcode，現在有 {configCount} 個配置。");
                return;
            }

            // 建立 barcode -> config 對應表
            barcodeConfigMap.Clear();
            for (int i = 0; i < barcodeCount; i++)
            {
                string barcode = listScanned.Items[i].ToString();
                string cfgName = dataGridConfigs.Rows[i].Cells["colCfgName"].Value?.ToString();
                if (!string.IsNullOrEmpty(cfgName))
                    barcodeConfigMap[barcode] = cfgName;
            }

            List<string> barcodes = new List<string>(barcodeConfigMap.Keys);
            listScanned.Items.Clear();
            lblScanCount.Text = $"已掃描：0 / {numBatchSize.Value}";
            scanData.Clear();

            isScanning = true;
            scanCts = new CancellationTokenSource();
            CancellationToken token = scanCts.Token;

            Task.Run(() =>
            {
                try
                {
                    AppendTextSafe(textBoxScanResult, "批次掃描 Task 已啟動...\r\n");
                    Logger.Log("批次掃描 Task 已啟動");
                    AppendTextSafe(textBoxScanResult, $"總共 {barcodes.Count} 個 barcode\r\n");

                    string comSW = GetSelectedCOM(comboSW_COM);
                    string comBT = GetSelectedCOM(comboBT_COM);

                    try
                    {
                        sw1001 = new HIOKI(comSW, 115200, DeviceType.SW1001);
                    }
                    catch (Exception ex)
                    {
                        AppendTextSafe(textBoxScanResult, $"無法開啟 SW1001 {comSW}: {ex.Message}\r\n");
                        return; // 停止批次
                    }

                    if (!string.IsNullOrEmpty(comBT))
                    {
                        try
                        {
                            bt3563a = new HIOKI(comBT, 9600, DeviceType.BT3563A);
                        }
                        catch (Exception ex)
                        {
                            AppendTextSafe(textBoxScanResult, $"無法開啟 BT3563A {comBT}: {ex.Message}\r\n");
                            bt3563a = null; // 沒有就只能跳過量測
                        }
                    }

                    foreach (string code in barcodes)
                    {
                        if (token.IsCancellationRequested) break;

                        try
                        {
                            ScanSingleBarcodeAsync(code, token);
                        }
                        catch (Exception ex)
                        {
                            AppendTextSafe(textBoxScanResult, $"掃描 {code} 發生錯誤: {ex.Message}\r\n");
                            break; // 一旦出錯，立即停止整個批次
                        }

                        //Task.Delay(500, token).Wait();
                    }

                    AppendTextSafe(textBoxScanResult, "所有批次掃描完成\r\n");

                    // 只有 scanData 有內容才匯出 CSV
                    if (scanData.Length > 0)
                        SaveCSV($"{tbFileName.Text}_{DateTime.Now:yyyyMMdd}.csv");
                    else
                        AppendTextSafe(textBoxScanResult, "無有效掃描資料，未匯出 CSV\r\n");
                }
                catch (Exception ex)
                {
                    AppendTextSafe(textBoxScanResult, $"批次掃描錯誤: {ex.Message}\r\n");
                }
                finally
                {
                    try { sw1001?.Close(); } catch { }
                    try { bt3563a?.Close(); } catch { }
                    isScanning = false;
                    scanCts.Dispose();
                    scanCts = null;
                }
            });
        }


        private async Task ScanSingleBarcodeAsync(string barcode, CancellationToken token)
        {
            if (token.IsCancellationRequested) return;

            if (!barcodeConfigMap.TryGetValue(barcode, out string cfgName))
                throw new Exception($"找不到 Barcode 對應的配置: {barcode}");

            AppendTextSafe(textBoxScanResult, $"掃描 Barcode {barcode} 對應配置: {cfgName}\r\n");

            var scanList = new List<(int slot, int channel, bool v,  bool r)>();

            dataGridConfigs.Invoke(new Action(() =>
            {
                foreach (DataGridViewRow row in dataGridConfigs.Rows)
                {
                    if (row.IsNewRow) continue;
                    string rowCfgName = row.Cells["colCfgName"].Value?.ToString();
                    if (rowCfgName != cfgName) continue;
                    if (!int.TryParse(row.Cells["colCfgSlot"].Value?.ToString(), out int slot)) continue;
                    if (!configChannelMap.TryGetValue(cfgName, out var settings)) continue;

                    foreach (var kvp in settings)
                    {
                        var (V, R) = kvp.Value;
                        if (!V  && !R) continue; // 沒勾選任何量測就跳過
                        scanList.Add((slot, kvp.Key, V,  R));
                    }
                }
            }));

            foreach (var item in scanList)
            {
                if (token.IsCancellationRequested) return;

                string channelResp = sw1001.SelectChannel(item.slot, item.channel);
                if (!channelResp.StartsWith("0"))
                {
                    AppendTextSafe(textBoxScanResult, $"錯誤: 切換通道失敗 {channelResp}\r\n");
                    break; // 停止測試
                }
                AppendTextSafe(textBoxScanResult, $"切換 Slot {item.slot} - Ch{item.channel:D2} 回應: {channelResp}\r\n");

                string volt = "N/A", res = "N/A";

                if (bt3563a != null)
                {
                    try { volt = item.v ? bt3563a.MeasureVoltage()?.Trim() : "N/A"; } catch { volt = "ERR"; }
                    try { res = item.r ? bt3563a.MeasureResistance()?.Trim() : "N/A"; } catch { res = "ERR"; }
                }

                // 移除末尾 ,ON 或 ,OFF
                if (!string.IsNullOrEmpty(volt))
                {
                    volt = volt.Replace(",ON", "").Replace(",OFF", "");
                }
                if (!string.IsNullOrEmpty(res))
                {
                    res = res.Replace(",ON", "").Replace(",OFF", "");
                }

                string line = $"Barcode {barcode} | Slot {item.slot} - Ch{item.channel:D2}: V={volt} V, R={res} Ω";
                AppendTextSafe(textBoxScanResult, line + "\r\n");
                Logger.Log(line);

                // 存 CSV 與 SQLite
                scanData.AppendLine($"{barcode},{volt},{res},{item.slot},Ch{item.channel:D2}");
                SaveToSQLite(barcode, item.slot.ToString(), $"Ch{item.channel:D2}", volt, res);

                // --- MQTT 發佈 ---
                if (mqtt != null && mqtt.IsConnected)
                {
                    var payload = new
                    {
                        Device = "HIOKI",
                        Barcode = barcode,
                        Slot = item.slot,
                        Channel = item.channel,
                        Voltage = volt,
                        Resistance = res,
                        Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await mqtt.PublishAsync("HIOKI", payload);
                        }
                        catch (Exception ex)
                        {
                            AppendTextSafe(textBoxScanResult, $"上傳失敗: {ex.Message}\r\n");
                        }
                    });
                }
                else
                {
                    AppendTextSafe(textBoxScanResult, "尚未連線，略過上傳\r\n");
                }

            }
        }


        private void SaveToSQLite(string barcode, string slot, string channel, string volt, string res)
        {
            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={sqliteFile}"))
            {
                conn.Open();
                string sql = "INSERT INTO ScanResult (Barcode, Slot, Channel, Voltage, Resistance) VALUES (@barcode, @slot, @ch, @v, @r)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@barcode", barcode);
                    cmd.Parameters.AddWithValue("@slot", slot);
                    cmd.Parameters.AddWithValue("@ch", channel);
                    cmd.Parameters.AddWithValue("@v", double.TryParse(volt, out double v) ? v : (double?)null);
                    cmd.Parameters.AddWithValue("@r", double.TryParse(res, out double r) ? r : (double?)null);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void SaveCSV(string Name)
        {
            string folderPath = tbFileLocation.Text.Trim();
            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                folderPath = AppDomain.CurrentDomain.BaseDirectory;
            }
            string fileName = Path.Combine(folderPath, Name);

            bool fileExists = File.Exists(fileName);

            var sb = new StringBuilder();

            if (!fileExists)
                sb.AppendLine("Barcode,Slot,Channel,Voltage,Resistance");

            foreach (string line in scanData.ToString().Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var parts = line.Split(',');
                if (parts.Length >= 5)
                {
                    sb.AppendLine($"{parts[0]},{parts[3]},{parts[4]},{parts[1]},{parts[2]}");
                }
            }

            File.AppendAllText(fileName, sb.ToString()); // 用 AppendAllText 避免覆蓋

            AppendTextSafe(textBoxScanResult, $"資料已匯出 CSV: {fileName}\r\n");
            Logger.Log($"資料已匯出 CSV: {fileName}，檔案位置: {folderPath}");
        }


        private void AppendTextSafe(TextBox tb, string text)
        {
            if (tb.InvokeRequired)
                tb.Invoke(new Action(() => tb.AppendText(text)));
            else
                tb.AppendText(text);
        }

        private void btnClearBatch_Click(object sender, EventArgs e)
        {
            listScanned.Items.Clear();
            lblScanCount.Text = $"已掃描：0 / {numBatchSize.Value}";
            scanData.Clear();
            textBoxScanResult.Clear();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var colCfgBtn = new DataGridViewButtonColumn
            {
                HeaderText = "Channel設定",
                Text = "設定...",
                UseColumnTextForButtonValue = true,
                Width = 100
            };
            this.dataGridConfigs.Columns.Add(colCfgBtn);
            dataGridConfigs.CellContentClick += dataGridConfigs_CellContentClick;

            var colCfgDesc = new DataGridViewTextBoxColumn
            {
                HeaderText = "掃描內容",
                Name = "colCfgDesc",
                ReadOnly = true,
                Width = 500
            };
            this.dataGridConfigs.Columns.Add(colCfgDesc);

            tbFileName.Text = "HIOKI_VRtEST";

            mqtt = new MqttPublisher(
                broker: currentBroker,
                port: currentPort,
                username: currentUsername,
                password: currentPassword
            );

            try
            {
                await mqtt.ConnectAsync();

                if (mqtt.IsConnected)
                {
                    ShowToast("MQTT 連線成功");
                }
                else
                {
                    ShowToast("MQTT 連線失敗！請檢查帳號、密碼或 Broker 是否可用。", isError: true);
                }
            }
            catch (Exception ex)
            {
                ShowToast($"MQTT 連線發生例外: {ex.Message}", isError: true);
            }
        }

        private void ShowToast(string message, bool isError = false)
        {
            Form toast = new Form();
            toast.FormBorderStyle = FormBorderStyle.None;
            toast.StartPosition = FormStartPosition.Manual;
            toast.BackColor = isError ? Color.Red : Color.Green;
            toast.ForeColor = Color.White;
            toast.Size = new Size(300, 50);
            toast.TopMost = true;
            toast.ShowInTaskbar = false;

            Label lbl = new Label();
            lbl.Text = message;
            lbl.Font = new Font("微軟正黑體", 12, FontStyle.Bold);
            lbl.ForeColor = Color.White;
            lbl.BackColor = Color.Transparent;
            lbl.AutoSize = false;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Dock = DockStyle.Fill;
            toast.Controls.Add(lbl);

            // 位置：Form 中上
            Point parentPos = this.PointToScreen(Point.Empty);
            int x = parentPos.X + (this.Width - toast.Width) / 2;
            int y = parentPos.Y + 30; // 中上位置
            toast.Location = new Point(x, y - 30); // 從上方稍微飄入
            toast.Opacity = 0;

            toast.Show();

            int duration = 1000; // 停留時間 ms
            int timerInterval = 20; // Timer 間隔 ms
            double opacityStep = 0.05; // 透明度每步增加量
            int moveStep = 1; // 每步向下移動像素
            int holdElapsed = 0;

            // 用 int 管理階段：0=FadeIn, 1=Hold, 2=FadeOut
            int phase = 0;

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = timerInterval;

            t.Tick += (s, e) =>
            {
                if (phase == 0) // FadeIn
                {
                    toast.Opacity += opacityStep;
                    toast.Top += moveStep;
                    if (toast.Opacity >= 1)
                    {
                        toast.Opacity = 1;
                        phase = 1; // 進入停留
                    }
                }
                else if (phase == 1) // Hold
                {
                    holdElapsed += timerInterval;
                    if (holdElapsed >= duration)
                        phase = 2; // 進入 FadeOut
                }
                else if (phase == 2) // FadeOut
                {
                    toast.Opacity -= opacityStep;
                    toast.Top -= moveStep;
                    if (toast.Opacity <= 0)
                    {
                        t.Stop();
                        t.Dispose();
                        toast.Close();
                        toast.Dispose();
                    }
                }
            };

            t.Start();
        }


        private void dataGridConfigs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // 判斷是不是按鈕欄
            if (dataGridConfigs.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                string cfgName = dataGridConfigs.Rows[e.RowIndex].Cells["colCfgName"].Value?.ToString();
                if (string.IsNullOrEmpty(cfgName)) return;

                configChannelMap.TryGetValue(cfgName, out var currentSettings);

                // 打開 Channel 設定
                using (var form = new FormChannelSetting(currentSettings))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        // 儲存回 configChannelMap
                        configChannelMap[cfgName] = form.ChannelSettings;

                        // 更新描述文字
                        UpdateDataGridDesc(e.RowIndex, form.ChannelSettings);
                    }
                }
            }
        }

        private void UpdateDataGridDesc(int rowIndex, Dictionary<int, (bool V,  bool R)> settings)
        {
            string desc = "";
            foreach (var kvp in settings)
            {
                int ch = kvp.Key;
                var (V, R) = kvp.Value;
                string s = "";
                if (V) s += "V";
                if (R) s += "R";
                if (!string.IsNullOrEmpty(s))
                    desc += $"Ch{ch} {s}, ";
            }
            desc = desc.TrimEnd(',', ' ');
            dataGridConfigs.Rows[rowIndex].Cells["colCfgDesc"].Value = desc;
        }

        private void btnRefreshSystem_Click(object sender, EventArgs e)
        {
            LoadAvailableCOMPorts();
        }

        private void numBatchSize_ValueChanged(object sender, EventArgs e)
        {
            lblScanCount.Text = $"已掃描：0 / {numBatchSize.Value}";
        }

        // 取得 ComboBox 選項（跨執行緒安全）
        private string GetSelectedCOM(ComboBox combo)
        {
            if (combo.InvokeRequired)
                return (string)combo.Invoke(new Func<string>(() => combo.SelectedItem?.ToString()));
            else
                return combo.SelectedItem?.ToString();
        }

        // 安全加入 ListBox
        private void AddItemSafe(ListBox list, string item)
        {
            if (list.InvokeRequired)
                list.Invoke(new Action(() => list.Items.Add(item)));
            else
                list.Items.Add(item);
        }

        // 安全更新 Label
        private void UpdateLabelSafe(Label lbl, string text)
        {
            if (lbl.InvokeRequired)
                lbl.Invoke(new Action(() => lbl.Text = text));
            else
                lbl.Text = text;
        }

        private void btnBrowe_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "選擇 CSV 存檔資料夾";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    tbFileLocation.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnExportSetting_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                sfd.Title = "匯出工程師設定";
                sfd.FileName = "EngineerSettings.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Type,Name,Value1,Value2,Value3,Value4,Value5");

                    // 匯出 grpEngineer 控制項
                    foreach (Control ctrl in grpEngineer.Controls)
                    {
                        if (ctrl is TextBox tb)
                            sb.AppendLine($"Control,{tb.Name},{tb.Text}");
                        else if (ctrl is ComboBox cb)
                            sb.AppendLine($"Control,{cb.Name},{cb.SelectedItem}");
                        else if (ctrl is NumericUpDown num)
                            sb.AppendLine($"Control,{num.Name},{num.Value}");
                    }

                    // 匯出 DataGridView + Channel 設定
                    foreach (DataGridViewRow row in dataGridConfigs.Rows)
                    {
                        if (row.IsNewRow) continue;
                        string cfgName = row.Cells["colCfgName"].Value?.ToString();
                        string slot = row.Cells["colCfgSlot"].Value?.ToString();

                        if (!string.IsNullOrEmpty(cfgName) && configChannelMap.TryGetValue(cfgName, out var chMap))
                        {
                            foreach (var kvp in chMap)
                            {
                                int ch = kvp.Key;
                                var (V, R) = kvp.Value;
                                sb.AppendLine($"Channel,{cfgName},{slot},{ch},{V},{R}");
                            }
                        }
                    }
                    // 匯出 tbFileName
                    sb.AppendLine($"Control,tbFileName,{tbFileName.Text}");

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show($"工程師設定已匯出至 {sfd.FileName}", "提示");
                }
            }
        }
        private void btnImportSetting_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV Files (*.csv)|*.csv";
                ofd.Title = "匯入工程師設定";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var lines = File.ReadAllLines(ofd.FileName, Encoding.UTF8);

                    configChannelMap.Clear();
                    dataGridConfigs.Rows.Clear();

                    foreach (var line in lines.Skip(1)) // 跳過標題列
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        var parts = line.Split(',');

                        if (parts[0] == "Control" && parts.Length >= 3)
                        {
                            string name = parts[1];
                            string value = parts[2];

                            if (name == "tbFileName")
                            {
                                tbFileName.Text = value;
                            }

                            Control ctrl = grpEngineer.Controls.Find(name, true).FirstOrDefault();
                            if (ctrl != null)
                            {
                                if (ctrl is TextBox tb) tb.Text = value;
                                else if (ctrl is ComboBox cb) cb.SelectedItem = value;
                                else if (ctrl is NumericUpDown num && decimal.TryParse(value, out decimal v)) num.Value = v;
                            }
                        }
                        else if (parts[0] == "Channel" && parts.Length >= 6)
                        {
                            string cfgName = parts[1];
                            string slot = parts[2];
                            if (!int.TryParse(parts[3], out int ch)) continue;
                            if (!bool.TryParse(parts[4], out bool V)) V = false;
                            if (!bool.TryParse(parts[5], out bool R)) R = false;

                            if (!configChannelMap.ContainsKey(cfgName))
                                configChannelMap[cfgName] = new Dictionary<int, (bool V, bool R)>();

                            configChannelMap[cfgName][ch] = (V, R);

                            // 加入 DataGridView，如果不存在這列
                            bool rowExists = false;
                            foreach (DataGridViewRow row in dataGridConfigs.Rows)
                            {
                                if (row.IsNewRow) continue;
                                if (row.Cells["colCfgName"].Value?.ToString() == cfgName &&
                                    row.Cells["colCfgSlot"].Value?.ToString() == slot)
                                {
                                    rowExists = true;
                                    break;
                                }
                            }
                            if (!rowExists)
                            {
                                int rowIndex = dataGridConfigs.Rows.Add();
                                dataGridConfigs.Rows[rowIndex].Cells["colCfgName"].Value = cfgName;
                                dataGridConfigs.Rows[rowIndex].Cells["colCfgSlot"].Value = slot;
                            }
                        }
                    }

                    // 更新描述文字
                    for (int i = 0; i < dataGridConfigs.Rows.Count; i++)
                    {
                        var row = dataGridConfigs.Rows[i];
                        if (row.IsNewRow) continue;
                        string cfgName = row.Cells["colCfgName"].Value?.ToString();
                        if (!string.IsNullOrEmpty(cfgName) && configChannelMap.TryGetValue(cfgName, out var chMap))
                        {
                            UpdateDataGridDesc(i, chMap);
                        }
                    }

                    MessageBox.Show("工程師設定已匯入完成！", "提示");
                    Logger.Log($"工程師設定已匯入: {ofd.FileName}");
                }
            }
        }

        private async void ConnectToMqtt(string broker, int port, string username, string password)
        {
            if (mqtt != null && mqtt.IsConnected)
            {
                await mqtt.DisconnectAsync();
            }

            mqtt = new MqttPublisher(broker, port, username, password);

            try
            {
                await mqtt.ConnectAsync();
                if (mqtt.IsConnected)
                {
                    ShowToast("MQTT 連線成功");
                }
                else
                {
                    ShowToast("MQTT 連線失敗！請檢查帳號、密碼或 Broker 是否可用。", isError: true);
                }
            }
            catch (Exception ex)
            {
                ShowToast($"MQTT 連線發生例外: {ex.Message}", isError: true);
            }
        }

        private void btnMqttSetting_Click(object sender, EventArgs e)
        {
            using (var frm = new FormMqttSetting(currentBroker, currentPort, currentUsername, currentPassword))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // 讀取使用者設定
                    currentBroker = frm.Broker;
                    currentPort = frm.Port;
                    currentUsername = frm.Username;
                    currentPassword = frm.Password;

                    Properties.Settings.Default.MqttBroker = currentBroker;
                    Properties.Settings.Default.MqttPort = currentPort;
                    Properties.Settings.Default.MqttUsername = currentUsername;
                    Properties.Settings.Default.MqttPassword = currentPassword;
                    Properties.Settings.Default.Save();

                    // 可以這裡嘗試連線 MQTT
                    ConnectToMqtt(currentBroker, currentPort, currentUsername, currentPassword);
                }
            }
        }
    }
}
