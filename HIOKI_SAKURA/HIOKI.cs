// HIOKI.cs
using System;
using System.IO;
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
            try
            {
                sp = new SerialPort(comPort, baudRate, Parity.None, 8, StopBits.One);
                sp.NewLine = "\r\n";
                sp.ReadTimeout = 2000;
                sp.WriteTimeout = 2000;
                sp.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"無法開啟 {comPort}: {ex.Message}");
            }
        }

        // ============================================================
        // 基本指令（有回覆）
        // ============================================================
        public string GetID()
        {
            if (!IsOpen()) return "";
            Send("*IDN?");
            return Read();
        }

        // ============================================================
        // SW1001 通道切換（正確手冊用法）
        // ============================================================
        public string SelectChannel(int slot, int ch)
        {
            if (device != DeviceType.SW1001)
                return "";

            // 官方格式：slot*100 + ch
            string channelCode = (slot * 100 + ch).ToString();

            try
            {
                // 1️ OPEN
                //SendNoResponse($":ROUT:OPEN {channelCode}");
                //Thread.Sleep(50);

                // 2️ CLOSE
                SendNoResponse($":ROUT:CLOSE {channelCode}");
                Thread.Sleep(10);

                // 3️ 查錯誤
                Send("SYST:ERR?");
                string err = Read();
                return err;
            }
            catch (IOException ex)
            {
                return $"-999,\"Exception: {ex.Message}\"";
            }
        }




        // ============================================================
        // BT3563A 測量功能
        // ============================================================
        public string MeasureVoltage()
        {
            if (device != DeviceType.BT3563A) return "";
            Send(":MEAS:VOLT?");
            Thread.Sleep(10);
            return Read();
        }

        public string MeasureCurrent()
        {
            if (device != DeviceType.BT3563A) return "";
            Send(":MEAS:CURR?");
            Thread.Sleep(10);
            return Read();
        }

        public string MeasureResistance()
        {
            if (device != DeviceType.BT3563A) return "";
            Send(":MEAS:RES?");
            Thread.Sleep(10);
            return Read();
        }

        // ============================================================
        // 共用：Send (不用回覆)
        // ============================================================
        private void SendNoResponse(string cmd)
        {
            if (!IsOpen())
                throw new InvalidOperationException("Serial port 未開啟");

            try
            {
                sp.DiscardInBuffer();
                sp.WriteLine(cmd);
            }
            catch (Exception ex)
            {
                throw new IOException($"Send 指令失敗: {cmd}, {ex.Message}", ex);
            }
        }

        // ============================================================
        // 共用：Send + 期待回覆
        // ============================================================
        private void Send(string cmd)
        {
            if (!IsOpen())
                throw new InvalidOperationException("Serial port 未開啟");

            try
            {
                sp.DiscardInBuffer();
                sp.WriteLine(cmd);
            }
            catch (Exception ex)
            {
                throw new IOException($"Send 指令失敗: {cmd}, {ex.Message}", ex);
            }
        }

        // ============================================================
        // 共用：Read
        // ============================================================
        private string Read()
        {
            if (!IsOpen())
                throw new InvalidOperationException("Serial port 未開啟");

            try
            {
                string response = sp.ReadLine().Trim();
                if (string.IsNullOrEmpty(response))
                    throw new IOException("儀器無回應");
                return response;
            }
            catch (TimeoutException)
            {
                throw new IOException("讀取儀器回應超時");
            }
            catch (Exception ex)
            {
                throw new IOException($"讀取儀器回應失敗: {ex.Message}", ex);
            }
        }

        private bool IsOpen()
        {
            return sp != null && sp.IsOpen;
        }

        public void Close()
        {
            try
            {
                if (sp != null)
                {
                    if (sp.IsOpen) sp.Close();
                    sp.Dispose();
                    sp = null;
                }
            }
            catch { }
        }
    }
}
