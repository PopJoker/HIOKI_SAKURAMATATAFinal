using System;
using System.IO.Ports;
using System.Threading;

namespace HIOKI_SAKURA
{
    public enum DeviceType
    {
        SW1001,
        BT3563A
    }

    public class HIOKI
    {
        private SerialPort sp;
        private DeviceType device;

        public HIOKI(string comPort, int baudRate, DeviceType deviceType)
        {
            device = deviceType;
            sp = new SerialPort(comPort, baudRate, Parity.None, 8, StopBits.One);
            sp.Open();
        }

        // 取得儀器 ID
        public string GetID()
        {
            if (sp == null || !sp.IsOpen) return "";
            sp.DiscardInBuffer();
            sp.Write("*IDN?\r");
            Thread.Sleep(200);
            return sp.ReadExisting();
        }

        // -----------------------
        // SW1001 專用：切換單一通道
        // -----------------------
        public void SelectChannel(int slot, int ch)
        {
            if (sp == null || !sp.IsOpen || device != DeviceType.SW1001) return;

            // 實際通道代碼，需依手冊或硬體對應表確認
            int code = slot * 100 + ch;
            sp.Write($":ROUT:CLOSE {code}\r");
            Thread.Sleep(100);

            // 建議查錯
            sp.Write(":SYST:ERR?\r");
            Thread.Sleep(50);
            string err = sp.ReadExisting();
            if (!string.IsNullOrEmpty(err) && !err.StartsWith("0"))
                throw new Exception($"SW1001 選通道錯誤: {err}");
        }

        // -----------------------
        // SW1001 專用：多通道掃描設定
        // -----------------------
        public void SetupScan(int[] slots, int[] channels)
        {
            if (sp == null || !sp.IsOpen || device != DeviceType.SW1001) return;
            if (slots.Length != channels.Length) throw new ArgumentException("Slots 與 Channels 長度必須一致");

            // 停止掃描（如果之前有啟動）
            sp.Write(":ROUT:SCAN:STAT OFF\r");
            Thread.Sleep(50);

            // 清空掃描清單
            sp.Write(":ROUT:SCAN:DEL:ALL\r");
            Thread.Sleep(50);

            // 建立掃描清單
            for (int i = 0; i < slots.Length; i++)
            {
                int code = slots[i] * 100 + channels[i]; // 依實際手冊確認
                sp.Write($":ROUT:SCAN:ADD {code}\r");
                Thread.Sleep(50);
            }

            // 啟動掃描
            sp.Write(":ROUT:SCAN:STAT ON\r");
            Thread.Sleep(100);

            // 可選：檢查錯誤
            sp.Write(":SYST:ERR?\r");
            Thread.Sleep(50);
            string err = sp.ReadExisting();
            if (!string.IsNullOrEmpty(err) && !err.StartsWith("0"))
                throw new Exception($"SW1001 多通道掃描設定錯誤: {err}");
        }

        // -----------------------
        // 讀取目前掃描通道電壓（單次輪詢返回）
        public string ReadScanVoltage()
        {
            if (sp == null || !sp.IsOpen || device != DeviceType.SW1001) return "";

            sp.Write(":MEAS:VOLT?\r");
            Thread.Sleep(200);
            return sp.ReadExisting();
        }



        // -----------------------
        // BT3563A 專用：控制功能
        // -----------------------
        public void SetVoltage(double v)
        {
            if (sp == null || !sp.IsOpen || device != DeviceType.BT3563A) return;
            sp.Write($"VOLT {v}\r");
            Thread.Sleep(100);
        }

        public void SetCurrent(double c)
        {
            if (sp == null || !sp.IsOpen || device != DeviceType.BT3563A) return;
            sp.Write($"CURR {c}\r");
            Thread.Sleep(100);
        }

        public void OutputOn()
        {
            if (sp == null || !sp.IsOpen || device != DeviceType.BT3563A) return;
            sp.Write("OUTP ON\r");
            Thread.Sleep(100);
        }

        public void OutputOff()
        {
            if (sp == null || !sp.IsOpen || device != DeviceType.BT3563A) return;
            sp.Write("OUTP OFF\r");
            Thread.Sleep(100);
        }

        public string MeasureVoltage()
        {
            if (sp == null || !sp.IsOpen || device != DeviceType.BT3563A) return "";
            sp.Write("MEAS:VOLT?\r");
            Thread.Sleep(200);
            return sp.ReadExisting();
        }

        public string MeasureCurrent()
        {
            if (sp == null || !sp.IsOpen || device != DeviceType.BT3563A) return "";
            sp.Write("MEAS:CURR?\r");
            Thread.Sleep(200);
            return sp.ReadExisting();
        }

        public string MeasureResistance()
        {
            if (sp == null || !sp.IsOpen || device != DeviceType.BT3563A) return "";
            sp.Write("MEAS:RES?\r");
            Thread.Sleep(200);
            return sp.ReadExisting();
        }

        // -----------------------
        // 關閉串口
        // -----------------------
        public void Close()
        {
            if (sp != null && sp.IsOpen)
                sp.Close();
        }
    }
}
