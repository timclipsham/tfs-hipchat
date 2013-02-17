using System;
using System.Xml.Serialization;

namespace TfsHipChat.Tfs
{
    /// <summary>
    /// Holds information about the server which raised an event
    /// </summary>
    [Serializable]
    [XmlRoot(Namespace = "", ElementName = "TeamFoundationServer")]
    public class TfsIdentity
    {
        [XmlAttribute("url")]
        public string Url { get; set; }
    }
}
