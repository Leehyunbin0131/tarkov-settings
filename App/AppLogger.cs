using System;
using System.IO;

namespace tarkov_settings
{
    internal static class AppLogger
    {
        private static readonly object sync = new object();

        public static string AppDataDirectory
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "tarkov-settings");
            }
        }

        public static string LogFilePath => Path.Combine(AppDataDirectory, "tarkov-settings.log");

        public static void Info(string message)
        {
            Write("INFO", message);
        }

        public static void Error(string message, Exception ex = null)
        {
            Write("ERROR", ex == null ? message : $"{message}: {ex}");
        }

        private static void Write(string level, string message)
        {
            try
            {
                Directory.CreateDirectory(AppDataDirectory);
                lock (sync)
                {
                    File.AppendAllText(
                        LogFilePath,
                        $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}{Environment.NewLine}");
                }
            }
            catch
            {
                // Logging must never break color reset paths.
            }
        }
    }
}
