using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandboxManager;
using System.Collections.Generic;

namespace SandboxManager.Tests
{
    [TestClass]
    public class SandboxConfigurationTests
    {
        [TestMethod]
        public void ToXml_DefaultConfiguration_GeneratesCorrectXml()
        {
            // Arrange
            var config = new SandboxConfiguration();
            var expectedXml = "<Configuration><Networking>Enable</Networking><MappedFolders/></Configuration>";

            // Act
            var actualXml = config.ToXml().Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("<?xmlversion=\"1.0\"encoding=\"utf-16\"?>", "");

            // Assert
            Assert.AreEqual(expectedXml, actualXml);
        }

        [TestMethod]
        public void ToXml_WithNetworkingDisabled_GeneratesCorrectXml()
        {
            // Arrange
            var config = new SandboxConfiguration { EnableNetworking = false };
            var expectedXml = "<Configuration><Networking>Disable</Networking><MappedFolders/></Configuration>";

            // Act
            var actualXml = config.ToXml().Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("<?xmlversion=\"1.0\"encoding=\"utf-16\"?>", "");

            // Assert
            Assert.AreEqual(expectedXml, actualXml);
        }

        [TestMethod]
        public void ToXml_WithSingleMappedFolder_GeneratesCorrectXml()
        {
            // Arrange
            var config = new SandboxConfiguration();
            config.MappedFolders.Add(new MappedFolder
            {
                HostFolder = "C:\\Host",
                SandboxFolder = "C:\\Sandbox",
                ReadOnly = true
            });
            var expectedXml = "<Configuration><Networking>Enable</Networking><MappedFolders><MappedFolder><HostFolder>C:\\Host</HostFolder><SandboxFolder>C:\\Sandbox</SandboxFolder><ReadOnly>true</ReadOnly></MappedFolder></MappedFolders></Configuration>";

            // Act
            var actualXml = config.ToXml().Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("<?xmlversion=\"1.0\"encoding=\"utf-16\"?>", "");

            // Assert
            Assert.AreEqual(expectedXml, actualXml);
        }

        [TestMethod]
        public void ToXml_WithMultipleMappedFolders_GeneratesCorrectXml()
        {
            // Arrange
            var config = new SandboxConfiguration();
            config.MappedFolders.Add(new MappedFolder { HostFolder = "C:\\Host1", SandboxFolder = "C:\\Sandbox1", ReadOnly = true });
            config.MappedFolders.Add(new MappedFolder { HostFolder = "C:\\Host2", SandboxFolder = "C:\\Sandbox2", ReadOnly = false });
            var expectedXml = "<Configuration><Networking>Enable</Networking><MappedFolders><MappedFolder><HostFolder>C:\\Host1</HostFolder><SandboxFolder>C:\\Sandbox1</SandboxFolder><ReadOnly>true</ReadOnly></MappedFolder><MappedFolder><HostFolder>C:\\Host2</HostFolder><SandboxFolder>C:\\Sandbox2</SandboxFolder><ReadOnly>false</ReadOnly></MappedFolder></MappedFolders></Configuration>";

            // Act
            var actualXml = config.ToXml().Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("<?xmlversion=\"1.0\"encoding=\"utf-16\"?>", "");

            // Assert
            Assert.AreEqual(expectedXml, actualXml);
        }

        [TestMethod]
        public void ToXml_WithLogonCommand_GeneratesCorrectXml()
        {
            // Arrange
            var config = new SandboxConfiguration
            {
                LogonCommand = new LogonCommand { Command = "notepad.exe" }
            };
            var expectedXml = "<Configuration><Networking>Enable</Networking><MappedFolders/><LogonCommand><Command>notepad.exe</Command></LogonCommand></Configuration>";

            // Act
            var actualXml = config.ToXml().Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("<?xmlversion=\"1.0\"encoding=\"utf-16\"?>", "");

            // Assert
            Assert.AreEqual(expectedXml, actualXml);
        }
    }
}
