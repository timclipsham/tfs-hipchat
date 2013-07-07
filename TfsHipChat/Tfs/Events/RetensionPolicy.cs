using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    public class RetensionPolicy
    {
        [XmlAttribute]
        public BuildReason BuildReason { get; set; }
        [XmlAttribute]
        public BuildStatus BuildStatus { get; set; }
        [XmlAttribute]
        public int NumberToKeep { get; set; }
        [XmlAttribute]
        public DeleteOptions DeleteOptions { get; set; }
    }
}