using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandboxManager;

namespace SandboxManager.Tests
{
    [TestClass]
    public class SandboxedApplicationTests
    {
        [TestMethod]
        public void GetConfiguration_ReturnsCorrectConfiguration()
        {
            // Arrange
            var app = new SandboxedApplication
            {
                Name = "TestApp",
                HostPath = "C:\\Program Files\\TestApp\\test.exe"
            };

            // Act
            var config = app.GetConfiguration();

            // Assert
            Assert.IsTrue(config.EnableNetworking);
            Assert.IsNotNull(config.LogonCommand);
            Assert.AreEqual("powershell.exe -ExecutionPolicy Bypass -File \"C:\\Scripts\\boot.ps1\" -AppToLaunch \"C:\\test\\test.exe\"", config.LogonCommand.Command);
            Assert.AreEqual(3, config.MappedFolders.Count); // App, Logs, and Scripts folders

            var appFolder = config.MappedFolders[0];
            Assert.AreEqual("C:\\Program Files\\TestApp", appFolder.HostFolder);
            Assert.AreEqual("C:\\test", appFolder.SandboxFolder);
            Assert.IsTrue(appFolder.ReadOnly);

            var logsFolder = config.MappedFolders[1];
            Assert.AreEqual(LoggingService.GetLogDirectory(), logsFolder.HostFolder);
            Assert.AreEqual("C:\\Logs", logsFolder.SandboxFolder);
            Assert.IsFalse(logsFolder.ReadOnly);

            var scriptsFolder = config.MappedFolders[2];
            Assert.AreEqual(System.AppDomain.CurrentDomain.BaseDirectory + "scripts", scriptsFolder.HostFolder);
            Assert.AreEqual("C:\\Scripts", scriptsFolder.SandboxFolder);
            Assert.IsTrue(scriptsFolder.ReadOnly);
        }
    }
}
