using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    public class BuildDetail
    {
        [XmlAttribute]
        string Uri { get; set; }
        [XmlAttribute]
        public string TeamProject { get; set; }
        [XmlAttribute]
        public string BuildNumber { get; set; }
        [XmlAttribute]
        public string BuildDefinitionUri { get; set; }
        [XmlAttribute]
        public DateTime StartTime { get; set; }
        [XmlAttribute]
        public DateTime FinishTime { get; set; }
        [XmlAttribute]
        public BuildReason Reason { get; set; }
        [XmlAttribute]
        public BuildStatus Status { get; set; }
        [XmlAttribute]
        public string Quality { get; set; }
        [XmlAttribute]
        public BuildPhaseStatus CompilationStatus { get; set; }
        [XmlAttribute]
        public BuildPhaseStatus TestStatus { get; set; }
        [XmlAttribute]
        public string DropLocation { get; set; }
        [XmlAttribute]
        public string DropLocationRoot { get; set; }
        [XmlAttribute]
        public string LogLocation { get; set; }
        [XmlAttribute]
        public string BuildControllerUri { get; set; }
        [XmlAttribute]
        public string SourceGetVersion { get; set; }
        [XmlAttribute]
        public DateTime LastChangedOn { get; set; }
        [XmlAttribute]
        public string LastChangedBy { get; set; }
        [XmlAttribute]
        public string LastedChangedByDisplayName { get; set; }
        [XmlAttribute]
        public bool KeepForever { get; set; }
        [XmlAttribute]
        public string LabelName { get; set; }
        [XmlAttribute]
        public bool IsDeleted { get; set; }

        public string ProcessParameters { get; set; }
        public List<int> QueueIds { get; set; }
        public List<BuildInformationNode> Information { get; set; }
    }
}