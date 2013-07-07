using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    public class QueuedBuild
    {
        [XmlAttribute]
        public Guid BatchId { get; set; }
        [XmlAttribute]
        public string BuildControllerUri { get; set; }
        [XmlAttribute]
        public string BuildDefinitionUri { get; set; }
        [XmlAttribute]
        public string CustomGetVersion { get; set; }
        [XmlAttribute]
        public string DropLocation { get; set; }
        [XmlAttribute]
        public GetOption GetOption { get; set; }
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public QueuePriority Priority { get; set; }
        [XmlAttribute]
        public int QueuePosition { get; set; }
        [XmlAttribute]
        public DateTime QueueTime { get; set; }
        [XmlAttribute]
        public BuildReason Reason { get; set; }
        [XmlAttribute]
        public string RequestedBy { get; set; }
        [XmlAttribute]
        public string RequestedByDisplayName { get; set; }
        [XmlAttribute]
        public string RequestedFor { get; set; }
        [XmlAttribute]
        public string RequestedForDisplayName { get; set; }
        [XmlAttribute]
        public string ShelvesetName { get; set; }
        [XmlAttribute]
        public QueueStatus Status { get; set; }
        [XmlAttribute]
        public string TeamProject { get; set; }

        public List<string> BuildUris { get; set; }
        public string ProcessParameters { get; set; }

        public QueuedBuild()
        {
            Priority = QueuePriority.Normal;
        }
    }
}