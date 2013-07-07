using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    public class ProcessTemplate
    {
        [XmlAttribute]
        public string TeamProject { get; set; }
        [XmlAttribute]
        public string ServerPath { get; set; }
        [XmlAttribute]
        public string Description { get; set; }
        [XmlAttribute]
        public BuildReason SupportedReasons { get; set; }
        [XmlAttribute]
        public ProcessTemplateType TemplateType { get; set; }
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public string Version { get; set; }

        public string Parameters { get; set; }
        public bool FileExists { get; set; }
    }
}