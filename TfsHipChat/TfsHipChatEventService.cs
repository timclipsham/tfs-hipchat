using System.IO;
using System.Xml.Serialization;
using System;
using System.Xml.Linq;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public class TfsHipChatEventService : IEventService
    {
        private readonly INotifier _notifier;

        // TODO: replace poor man's IoC with a full solution
        public TfsHipChatEventService() : this(new HipChatNotifier())
        {
        }

        public TfsHipChatEventService(INotifier notifier)
        {
            _notifier = notifier;
        }

        public void Notify(string eventXml, string tfsIdentityXml)
        {
            var xml = XElement.Parse(eventXml);

            switch (xml.Name.LocalName)
            {
                case "CheckinEvent":
                    var checkinEvent = DeserializeXmlToType<CheckinEvent>(eventXml);
                    _notifier.SendCheckinNotification(checkinEvent);
                    break;

                case "BuildCompletionEvent":
                    var buildCompletionEvent = DeserializeXmlToType<BuildCompletionEvent>(eventXml);
                    if (buildCompletionEvent.CompletionStatus != "Successfully Completed")
                    {
                        _notifier.SendBuildCompletionFailedNotification(buildCompletionEvent);
                    }
                    break;

                default:
                    throw new NotSupportedException("The event received is not supported.");
            }
        }

        private T DeserializeXmlToType<T>(string eventXml) where T : class {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(eventXml))
            {
                return serializer.Deserialize(reader) as T;
            }
        }
    }
}
