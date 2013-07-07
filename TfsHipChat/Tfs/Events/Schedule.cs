using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    public class Schedule
    {
        [XmlAttribute]
        public string TimeZoneId { get; set; }
        [XmlAttribute]
        public int UtcStartTime { get; set; }
        [XmlAttribute]
        public ScheduleDays UtcDaysToBuild { get; set; }
    }
}