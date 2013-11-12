using System;
using System.Linq;
using System.Xml.Serialization;

namespace TfsHipChat.Tfs.Events
{
    [XmlRoot]
    public class WorkItemChangedEvent
    {
        public string PortfolioProject { get; set; }
        public string ProjectNodeId { get; set; }
        public string AreaPath { get; set; }
        public string Title { get; set; }
        public string WorkItemTitle { get; set; }
        public string Subscriber { get; set; }
        public string ChangerSid { get; set; }
        public string ChangerTeamFoundationId { get; set; }
        public string DisplayUrl { get; set; }
        public string TimeZone { get; set; }
        public string TimeZoneOffset { get; set; }
        public string ChangeType { get; set; }

        public FieldGroup CoreFields { get; set; }
        public FieldGroup ChangedFields { get; set; }
        public TextField[] TextFields { get; set; }

        public WorkItemChangedEvent()
        {
            CoreFields = new FieldGroup {IntegerFields = new Field[0], StringFields = new Field[0]};
            ChangedFields = new FieldGroup {IntegerFields = new Field[0], StringFields = new Field[0]};
            TextFields = new TextField[0];
        }

        public Field GetFieldOrNull(string name)
        {
            return GetCoreFieldOrNull(name) ?? GetChangedFieldOrNull(name);
        }

        public Field GetChangedFieldOrNull(string name)
        {
            return ChangedFields.StringFields
                .Union(ChangedFields.IntegerFields)
                .FirstOrDefault(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public Field GetCoreFieldOrNull(string name)
        {
            return CoreFields.StringFields
                .Union(CoreFields.IntegerFields)
                .FirstOrDefault(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public TextField GetTextFieldOrNull(string name)
        {
            if (TextFields == null)
            {
                return null;
            }
            return TextFields
                .FirstOrDefault(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}