namespace SandboxManager
{
    public class SandboxedApplication
    {
        public string Name { get; set; }
        public string HostPath { get; set; } // Path to the executable on the host

        public SandboxConfiguration GetConfiguration()
        {
            string hostDir = System.IO.Path.GetDirectoryName(HostPath);
            string appName = System.IO.Path.GetFileNameWithoutExtension(HostPath);
            string sandboxDir = $"C:\\{appName}";
            string sandboxExePath = $"{sandboxDir}\\{System.IO.Path.GetFileName(HostPath)}";

            var config = new SandboxConfiguration();
            config.EnableNetworking = true;
            config.MappedFolders.Add(new MappedFolder
            {
                HostFolder = hostDir,
                SandboxFolder = sandboxDir,
                ReadOnly = true
            });
            config.MappedFolders.Add(new MappedFolder
            {
                HostFolder = LoggingService.GetLogDirectory(),
                SandboxFolder = "C:\\Logs",
                ReadOnly = false // Allow sandbox to write logs
            });
            config.LogonCommand = new LogonCommand
            {
                Command = $"\"{sandboxExePath}\""
            };

            return config;
        }
    }
}
