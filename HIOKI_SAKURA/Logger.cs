using System;
using System.IO;

namespace HIOKI_SAKURA
{
    internal static class Logger
    {
        private static readonly string folderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "HIOKI_SAKURA");

        private static readonly string logFile = Path.Combine(folderPath, "operation.log");

        static Logger()
        {
            // 建立資料夾（如果不存在）
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }

        /// <summary>
        /// 寫入操作日誌
        /// </summary>
        /// <param name="message">要寫入的文字</param>
        public static void Log(string message)
        {
            try
            {
                string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
                File.AppendAllText(logFile, line + Environment.NewLine);
            }
            catch
            {
                // 避免 LOG 寫入失敗影響程式
            }
        }
    }
}
