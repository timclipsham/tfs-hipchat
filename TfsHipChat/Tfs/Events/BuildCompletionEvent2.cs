using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    /// <summary>
    /// Object model for deserializing a TFS BuildCompletionEvent2.
    /// </summary>
    [XmlRoot]
    public class BuildCompletionEvent2
    {
        public string BuildUri { get; set; }
        public string TeamFoundationServerUrl { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string TeamProject { get; set; }
        public string AgentPath { get; set; }
        public string DefinitionPath { get; set; }
        public string BuildNumber { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public string ConfigurationFolderUri { get; set; }
        public string SourceGetVersion { get; set; }
        public string Quality { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string CompilationStatus { get; set; }
        public string TestStatus { get; set; }
        public string LogLocation { get; set; }
        public string DropLocation { get; set; }
        public string RequestedFor { get; set; }
        public string RequestedBy { get; set; }
        public string LastChangedOn { get; set; }
        public string LastChangedBy { get; set; }
        public bool KeepForever { get; set; }
        public string TimeZone { get; set; }
        public string TimeZoneOffset { get; set; }
        public string Subscriber { get; set; }
    }
}