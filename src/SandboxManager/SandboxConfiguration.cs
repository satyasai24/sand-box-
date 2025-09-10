using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace SandboxManager
{
    [XmlRoot("Configuration")]
    public class SandboxConfiguration
    {
        [XmlIgnore]
        public bool EnableNetworking
        {
            get => Networking == "Enable";
            set => Networking = value ? "Enable" : "Disable";
        }

        [XmlElement("Networking")]
        public string Networking { get; set; } = "Enable";

        [XmlArray("MappedFolders")]
        [XmlArrayItem("MappedFolder")]
        public List<MappedFolder> MappedFolders { get; set; } = new List<MappedFolder>();

        [XmlElement("LogonCommand")]
        public LogonCommand LogonCommand { get; set; }

        public string ToXml()
        {
            var serializer = new XmlSerializer(typeof(SandboxConfiguration));
            using (var stringWriter = new StringWriter())
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add("", ""); // To remove xmlns:xsi and xmlns:xsd attributes
                serializer.Serialize(stringWriter, this, ns);
                return stringWriter.ToString();
            }
        }
    }

    public class MappedFolder
    {
        [XmlElement("HostFolder")]
        public string HostFolder { get; set; }

        [XmlElement("SandboxFolder")]
        public string SandboxFolder { get; set; }

        [XmlElement("ReadOnly")]
        public bool ReadOnly { get; set; }
    }

    public class LogonCommand
    {
        [XmlElement("Command")]
        public string Command { get; set; }
    }
}
