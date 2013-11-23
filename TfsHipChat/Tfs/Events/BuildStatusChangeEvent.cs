using System;
using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    [XmlRoot]
    public class BuildStatusChangeEvent
    {
        public string BuildUri { get; set; }
        public string TeamFoundationServerUrl { get; set; }
        public string TeamProject { get; set; }
        public string Title { get; set; }
        public string Subscriber { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public string TimeZone { get; set; }
        public string ChangedTime { get; set; }
        public FieldChange StatusChange { get; set; }
        public string ChangedBy { get; set; }
    }
}