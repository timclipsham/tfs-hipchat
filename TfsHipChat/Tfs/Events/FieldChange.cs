namespace TfsHipChat.Tfs.Events
{
    public class FieldChange
    {
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}