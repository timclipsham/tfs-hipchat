using System.Collections.Generic;
using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    public class BuildDefinitionSourceProvider
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public DefinitionTriggerType SupportedTriggerTypes { get; set; }

        public List<NameValueField> Fields { get; set; }
    }
}