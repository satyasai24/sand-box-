using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SandboxManager
{
    public static class ConfigurationService
    {
        private static readonly string configDirectory;
        private static readonly string configFilePath;

        static ConfigurationService()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            configDirectory = Path.Combine(appDataPath, "SandboxManager");
            Directory.CreateDirectory(configDirectory);
            configFilePath = Path.Combine(configDirectory, "applications.json");
        }

        public static List<SandboxedApplication> LoadApplications()
        {
            if (!File.Exists(configFilePath))
            {
                // Return a default list if the config file doesn't exist yet
                return new List<SandboxedApplication>
                {
                    new SandboxedApplication { Name = "Google Chrome", HostPath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe" }
                };
            }

            try
            {
                string json = File.ReadAllText(configFilePath);
                return JsonSerializer.Deserialize<List<SandboxedApplication>>(json);
            }
            catch (Exception ex)
            {
                // Log the error and return a default list
                LoggingService.Log($"Error loading configuration: {ex.Message}");
                return new List<SandboxedApplication>();
            }
        }

        public static void SaveApplications(List<SandboxedApplication> applications)
        {
            try
            {
                string json = JsonSerializer.Serialize(applications, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configFilePath, json);
            }
            catch (Exception ex)
            {
                // Log the error
                LoggingService.Log($"Error saving configuration: {ex.Message}");
            }
        }
    }
}
