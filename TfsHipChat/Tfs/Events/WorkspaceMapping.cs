using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    public class WorkspaceMapping
    {
        [XmlAttribute]
        public string ServerItem { get; set; }
        [XmlAttribute]
        public string LocalItem { get; set; }
        [XmlAttribute]
        public WorkspaceMappingType MappingType { get; set; }
        [XmlAttribute]
        public int Depth { get; set; }
    }
}