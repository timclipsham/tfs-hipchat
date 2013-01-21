using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    /// <summary>
    /// Event raised when a build completes
    /// </summary>
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class BuildCompletionEvent
    {
        private string _teamFoundationServerUrl;
        private string _teamProject;
        private string _id;
        private string _url;
        private string _type;
        private string _title;
        private string _completionStatus;
        private string _subscriber;
        private string _configuration;
        private string _requestedBy;
        private string _timeZone;
        private string _timeZoneOffset;
        private string _buildStartTime;
        private string _buildCompleteTime;
        private string _buildMachine;

        #region Public Properties
        /// <remarks/>
        [XmlElement(DataType = "anyURI")]
        public string TeamFoundationServerUrl
        {
            get { return _teamFoundationServerUrl; }
            set { _teamFoundationServerUrl = value; }
        }

        /// <remarks/>
        public string TeamProject
        {
            get { return _teamProject; }
            set { _teamProject = value; }
        }

        /// <remarks/>
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <remarks/>
        [XmlElement(DataType = "anyURI")]
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <remarks/>
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <remarks/>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <remarks/>
        public string CompletionStatus
        {
            get { return _completionStatus; }
            set { _completionStatus = value; }
        }

        /// <remarks/>
        public string Subscriber
        {
            get { return _subscriber; }
            set { _subscriber = value; }
        }

        /// <remarks/>
        public string Configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }

        /// <remarks/>
        public string RequestedBy
        {
            get { return _requestedBy; }
            set { _requestedBy = value; }
        }

        /// <remarks/>
        public string TimeZone
        {
            get { return _timeZone; }
            set { _timeZone = value; }
        }

        /// <remarks/>
        public string TimeZoneOffset
        {
            get { return _timeZoneOffset; }
            set { _timeZoneOffset = value; }
        }

        /// <remarks/>
        public string BuildStartTime
        {
            get { return _buildStartTime; }
            set { _buildStartTime = value; }
        }

        /// <remarks/>
        public string BuildCompleteTime
        {
            get { return _buildCompleteTime; }
            set { _buildCompleteTime = value; }
        }

        /// <remarks/>
        public string BuildMachine
        {
            get { return _buildMachine; }
            set { _buildMachine = value; }
        }
        #endregion
    }
}