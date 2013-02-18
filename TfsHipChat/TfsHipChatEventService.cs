using System.IO;
using System.Xml.Serialization;
using System;
using System.Xml.Linq;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public class TfsHipChatEventService : IEventService
    {
        private readonly INotificationHandler _notificationHandler;

        // TODO: replace poor man's IoC with a full solution
        public TfsHipChatEventService() : this(new NotificationHandler())
        {
        }

        public TfsHipChatEventService(INotificationHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }

        public void Notify(string eventXml, string tfsIdentityXml)
        {
            var xml = XElement.Parse(eventXml);

            switch (xml.Name.LocalName)
            {
                case "CheckinEvent":
                    var checkinEvent = DeserializeXmlToType<CheckinEvent>(eventXml);
                    _notificationHandler.HandleCheckin(checkinEvent);
                    break;

                case "BuildCompletionEvent":
                    var buildEvent = DeserializeXmlToType<BuildCompletionEvent>(eventXml);
                    _notificationHandler.HandleBuildCompletion(buildEvent);
                    break;

                default:
                    throw new NotSupportedException("The event received is not supported.");
            }
        }

        private static T DeserializeXmlToType<T>(string eventXml) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(eventXml))
            {
                return serializer.Deserialize(reader) as T;
            }
        }
    }
}
