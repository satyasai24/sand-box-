using System;

namespace SandboxManager
{
    public class SandboxedApplication
    {
        public string Name { get; set; }
        public string HostPath { get; set; } // Path to the executable on the host

        public SandboxConfiguration GetConfiguration()
        {
            string hostDir = System.IO.Path.GetDirectoryName(HostPath);
            string appExeName = System.IO.Path.GetFileName(HostPath);
            string appName = System.IO.Path.GetFileNameWithoutExtension(HostPath);
            string sandboxAppDir = $"C:\\{appName}";
            string sandboxAppExePath = $"{sandboxAppDir}\\{appExeName}";

            string scriptsHostDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts");
            string sandboxScriptsDir = "C:\\Scripts";
            string sandboxBootScriptPath = $"{sandboxScriptsDir}\\boot.ps1";

            var config = new SandboxConfiguration();
            config.EnableNetworking = true;

            // Map the application folder
            config.MappedFolders.Add(new MappedFolder
            {
                HostFolder = hostDir,
                SandboxFolder = sandboxAppDir,
                ReadOnly = true
            });

            // Map the logs folder
            config.MappedFolders.Add(new MappedFolder
            {
                HostFolder = LoggingService.GetLogDirectory(),
                SandboxFolder = "C:\\Logs",
                ReadOnly = false
            });

            // Map the scripts folder
            config.MappedFolders.Add(new MappedFolder
            {
                HostFolder = scriptsHostDir,
                SandboxFolder = sandboxScriptsDir,
                ReadOnly = true
            });

            // Set the logon command to run the boot script
            config.LogonCommand = new LogonCommand
            {
                Command = $"powershell.exe -ExecutionPolicy Bypass -File \"{sandboxBootScriptPath}\" -AppToLaunch \"{sandboxAppExePath}\""
            };

            return config;
        }
    }
}
