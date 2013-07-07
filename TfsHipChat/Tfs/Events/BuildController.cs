using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    public class BuildController
    {
        [XmlAttribute]
        public string Uri { get; set; }
        [XmlAttribute]
        public string ServiceHostUri { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string CustomAssemblyPath { get; set; }
        [XmlAttribute]
        public int MaxConcurrentBuilds { get; set; }
        [XmlAttribute]
        public int QueueCount { get; set; }
        [XmlAttribute]
        public ControllerStatus Status { get; set; }
        [XmlAttribute]
        public string StatusMessage { get; set; }
        [XmlAttribute]
        public bool Enabled { get; set; }
        [XmlAttribute]
        public string Url { get; set; }
        [XmlAttribute]
        public string MessageQueueUrl { get; set; }
        [XmlAttribute]
        public DateTime DateCreated { get; set; }
        [XmlAttribute]
        public DateTime DateUpdated { get; set; }

        public List<string> Tags { get; set; }
        public string Description { get; set; }
        // public List<PropertyValue> Properties { get; set; }
    }
}