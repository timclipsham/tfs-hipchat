using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    public class NameValueField
    {
        [XmlAttribute]
        public string Name { get; set; }
        public string Value { get; set; }
    }
}