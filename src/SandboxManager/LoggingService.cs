using System;
using System.IO;

namespace SandboxManager
{
    public static class LoggingService
    {
        private static readonly string logDirectory;
        private static readonly string logFilePath;

        static LoggingService()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            logDirectory = Path.Combine(appDataPath, "SandboxManager", "Logs");
            Directory.CreateDirectory(logDirectory);
            logFilePath = Path.Combine(logDirectory, "events.log");
        }

        public static void Log(string message)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
            File.AppendAllText(logFilePath, logMessage);
        }

        public static string GetLogDirectory()
        {
            return logDirectory;
        }

        public static string GetLogFilePath()
        {
            return logFilePath;
        }
    }
}
