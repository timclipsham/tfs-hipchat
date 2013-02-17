using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    /// <summary>
    /// Object model for deserializing a TFS build completion event.
    /// </summary>
    [XmlRoot]
    public class BuildCompletionEvent
    {
        public string TeamFoundationServerUrl { get; set; }
        public string TeamProject { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string CompletionStatus { get; set; }
        public string Subscriber { get; set; }
        public string Configuration { get; set; }
        public string RequestedBy { get; set; }
        public string TimeZone { get; set; }
        public string TimeZoneOffset { get; set; }
        public string BuildStartTime { get; set; }
        public string BuildCompleteTime { get; set; }
        public string BuildMachine { get; set; }
    }
}
