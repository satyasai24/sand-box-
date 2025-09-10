using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SandboxManager
{
    public class SandboxLauncher
    {
        public void Launch(SandboxConfiguration config)
        {
            // Get a temporary file path with a .wsb extension.
            string wsbFilePath = Path.ChangeExtension(Path.GetTempFileName(), ".wsb");

            // Serialize the config object to XML and write it to the file.
            string xmlConfig = config.ToXml();
            File.WriteAllText(wsbFilePath, xmlConfig);

            // Launch the sandbox using Process.Start().
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(wsbFilePath)
                {
                    UseShellExecute = true
                }
            };
            process.Start();
        }
    }
}
