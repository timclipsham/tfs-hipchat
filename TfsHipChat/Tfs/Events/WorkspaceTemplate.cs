using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    public class WorkspaceTemplate
    {
        [XmlAttribute]
        public string DefinitionUri { get; set; }
        [XmlAttribute]
        public DateTime LastModifiedDate { get; set; }
        [XmlAttribute]
        public string LastModifiedBy { get; set; }

        public List<WorkspaceMapping> Mappings { get; set; }
    }
}