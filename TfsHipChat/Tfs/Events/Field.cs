namespace TfsHipChat.Tfs.Events
{
    public class Field
    {
        public string Name { get; set; }
        public string ReferenceName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}