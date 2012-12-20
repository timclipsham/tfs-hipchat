using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace TfsHipChat.Events
{
    /// <summary>
    /// Event raised when a Build Completes
    /// </summary>
    [GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class BuildCompletionEvent
    {
        #region Fields
        private string teamFoundationServerUrl;
        private string teamProject;
        private string id;
        private string url;
        private string type;
        private string title;
        private string completionStatus;
        private string subscriber;
        private string configuration;
        private string requestedBy;
        private string timeZone;
        private string timeZoneOffset;
        private string buildStartTime;
        private string buildCompleteTime;
        private string buildMachine;
        #endregion

        #region Public Properties
        /// <remarks/>
        [XmlElementAttribute(DataType = "anyURI")]
        public string TeamFoundationServerUrl
        {
            get { return this.teamFoundationServerUrl; }
            set { this.teamFoundationServerUrl = value; }
        }

        /// <remarks/>
        public string TeamProject
        {
            get { return this.teamProject; }
            set { this.teamProject = value; }
        }

        /// <remarks/>
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <remarks/>
        [XmlElementAttribute(DataType = "anyURI")]
        public string Url
        {
            get { return this.url; }
            set { this.url = value; }
        }

        /// <remarks/>
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <remarks/>
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        /// <remarks/>
        public string CompletionStatus
        {
            get { return this.completionStatus; }
            set { this.completionStatus = value; }
        }

        /// <remarks/>
        public string Subscriber
        {
            get { return this.subscriber; }
            set { this.subscriber = value; }
        }

        /// <remarks/>
        public string Configuration
        {
            get { return this.configuration; }
            set { this.configuration = value; }
        }

        /// <remarks/>
        public string RequestedBy
        {
            get { return this.requestedBy; }
            set { this.requestedBy = value; }
        }

        /// <remarks/>
        public string TimeZone
        {
            get { return this.timeZone; }
            set { this.timeZone = value; }
        }

        /// <remarks/>
        public string TimeZoneOffset
        {
            get { return this.timeZoneOffset; }
            set { this.timeZoneOffset = value; }
        }

        /// <remarks/>
        public string BuildStartTime
        {
            get { return this.buildStartTime; }
            set { this.buildStartTime = value; }
        }

        /// <remarks/>
        public string BuildCompleteTime
        {
            get { return this.buildCompleteTime; }
            set { this.buildCompleteTime = value; }
        }

        /// <remarks/>
        public string BuildMachine
        {
            get { return this.buildMachine; }
            set { this.buildMachine = value; }
        }
        #endregion
    }
}