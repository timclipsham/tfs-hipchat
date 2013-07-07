using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    public class BuildInformationNode
    {
        [XmlAttribute]
        public int NodeId { get; set; }
        [XmlAttribute]
        public int ParentId { get; set; }
        [XmlAttribute]
        public string Type { get; set; }
        [XmlAttribute]
        public DateTime LastModifiedDate { get; set; }
        [XmlAttribute]
        public string LastModifiedBy { get; set; }

        public List<InformationField> Fields { get; set; }
    }
}