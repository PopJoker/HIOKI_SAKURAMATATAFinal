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
            sp.NewLine = "\n";
            sp.Open();
        }

        // -----------------------
        // 取得儀器 ID
        // -----------------------
        public string GetID()
        {
            if (!IsOpen()) return "";
            Send("*IDN?");
            return Read();
        }

        // -----------------------
        // SW1001：清空全部通道
        // -----------------------
        public void OpenAll()
        {
            if (device != DeviceType.SW1001) return;

            Send(":ROUT:OPEN ALL");
            CheckError();
        }

        // -----------------------
        // SW1001：切換指定 slot + ch
        // -----------------------
        public void SelectChannel(int slot, int ch)
        {
            if (device != DeviceType.SW1001) return;

            // SW1001 正確寫法：slot!ch
            Send($":ROUT:OPEN ALL");
            Thread.Sleep(50);

            Send($":ROUT:CLOS (@{slot}!{ch})");
            Thread.Sleep(100);

            CheckError();
        }

        // -----------------------
        // SW1001：設定掃描清單
        // -----------------------
        public void SetupScan(int[] slots, int[] channels)
        {
            if (device != DeviceType.SW1001) return;
            if (slots.Length != channels.Length)
                throw new Exception("slots 與 channels 長度必須一致");

            string list = "";
            for (int i = 0; i < slots.Length; i++)
                list += $"{slots[i]}!{channels[i]},";

            list = list.TrimEnd(',');

            Send($":ROUT:SCAN (@{list})");
            Thread.Sleep(100);

            CheckError();
        }

        // -----------------------
        // SW1001：開始掃描
        // -----------------------
        public void StartScan()
        {
            if (device != DeviceType.SW1001) return;

            Send(":ROUT:SCAN:START");
            Thread.Sleep(100);
            CheckError();
        }
        // -----------------------
        // SW1001：讀取掃描電壓
        // -----------------------
        public string ReadScanVoltage()
        {
            if (device != DeviceType.SW1001) return "";

            Send(":ROUT:SCAN:VAL?"); // 查詢掃描電壓
            Thread.Sleep(150);
            string val = Read();
            return val.Trim(); // 去掉換行
        }

        // -----------------------
        // BT3563A：量測功能
        // -----------------------
        public string MeasureVoltage()
        {
            if (device != DeviceType.BT3563A) return "";
            Send(":MEAS:VOLT?");
            Thread.Sleep(150);
            return Read();
        }

        public string MeasureCurrent()
        {
            if (device != DeviceType.BT3563A) return "";
            Send(":MEAS:CURR?");
            Thread.Sleep(150);
            return Read();
        }

        public string MeasureResistance()
        {
            if (device != DeviceType.BT3563A) return "";
            Send(":MEAS:RES?");
            Thread.Sleep(150);
            return Read();
        }

        // -----------------------
        // 共用功能
        // -----------------------
        private void Send(string cmd)
        {
            if (!IsOpen()) return;
            sp.DiscardInBuffer();
            sp.Write(cmd + "\n");
        }

        private string Read()
        {
            Thread.Sleep(80);
            return sp.ReadExisting();
        }

        // 查詢錯誤
        private void CheckError()
        {
            Send(":SYST:ERR?");
            Thread.Sleep(30);
            string err = Read();

            if (!err.StartsWith("0"))
                throw new Exception($"儀器錯誤：{err}");
        }

        private bool IsOpen()
        {
            return sp != null && sp.IsOpen;
        }

        public void Close()
        {
            if (sp != null && sp.IsOpen)
                sp.Close();
        }
    }
}
