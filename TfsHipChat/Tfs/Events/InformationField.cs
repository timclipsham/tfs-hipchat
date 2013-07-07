using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    public class InformationField
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Value { get; set; }
    }
}