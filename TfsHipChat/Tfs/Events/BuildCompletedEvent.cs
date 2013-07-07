using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    /// <summary>
    /// Object model for deserializing a TFS BuildCompletedEvent.
    /// </summary>
    [XmlRoot(Namespace = "http://schemas.microsoft.com/TeamFoundation/2010/Build")]
    public class BuildCompletedEvent
    {
        public BuildDetail Build { get; set; }
        public BuildController Controller { get; set; }
        public BuildDefinition Definition { get; set; }
        public List<QueuedBuild> Requests { get; set; }
        public string Subscriber { get; set; }
        public string TeamProjectCollectionUrl { get; set; }
        public string TimeZone { get; set; }
        public string TimeZoneOffset { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; }
        public string WebAccessUri { get; set; }
        public int Duration { get; set; }
        public DateTime FinishTimeLocal { get; set; }
        public DateTime StartTimeLocal { get; set; }
    }
}
