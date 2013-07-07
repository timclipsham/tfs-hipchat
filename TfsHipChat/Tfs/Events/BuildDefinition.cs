using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    public class BuildDefinition
    {
        [XmlAttribute]
        public int BatchSize { get; set; }
        [XmlAttribute]
        public string BuildControllerUri { get; set; }
        [XmlAttribute]
        public DefinitionTriggerType TriggerType { get; set; }
        [XmlAttribute]
        public int ContinuousIntegrationQuietPeriod { get; set; }
        [XmlAttribute]
        public string DefaultDropLocation { get; set; }
        [XmlAttribute]
        public DefinitionQueueStatus QueueStatus { get; set; }
        [XmlAttribute]
        public string FullPath { get; set; }
        [XmlAttribute]
        public string LastBuildUri { get; set; }
        [XmlAttribute]
        public string LastGoodBuildUri { get; set; }
        [XmlAttribute]
        public string LastGoodBuildLabel { get; set; }
        [XmlAttribute]
        public string Uri { get; set; }
        [XmlAttribute]
        public DateTime DateCreated { get; set; }

        public string Description { get; set; }
        public List<RetensionPolicy> RetensionPolicies { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<BuildDefinitionSourceProvider> SourceProviders { get; set; }
        public WorkspaceTemplate WorkspaceTemplate { get; set; }
        public ProcessTemplate Process { get; set; }
        public string ProcessParameters { get; set; }
        //public List<PropertyValue> Properties { get; set; }
    }
}