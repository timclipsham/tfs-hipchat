using System.IO;
using System.Xml.Serialization;
using Microsoft.TeamFoundation.VersionControl.Common;

namespace TfsHipChat
{
    public class CheckinEventService : IEventService
    {
        private INotifier _notifier;

        public CheckinEventService()
        {
        }

        public CheckinEventService(INotifier notifier)
        {
            this._notifier = notifier;
        }

        public void Notify(string eventXml, string tfsIdentityXml)
        {
            var serializer = new XmlSerializer(typeof(CheckinEvent));
            CheckinEvent checkinEvent;

            using (var reader = new StringReader(eventXml))
            {
                checkinEvent = serializer.Deserialize(reader) as CheckinEvent;
            }

            if (checkinEvent == null)
            {
                return;
            }

            _notifier.SendCheckinNotification(checkinEvent);
        }
    }
}
