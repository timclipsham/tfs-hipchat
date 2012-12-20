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
            var serializer = new XmlSerializer(typeof(CheckinEventService));
            CheckinEvent checkinEvent;

            using (var reader = new StringReader(eventXml))
            {
                checkinEvent = serializer.Deserialize(reader) as CheckinEvent;  // double check "TryCast"...
            }

            if (checkinEvent == null)
            {
                return;
            }

            // check whether the changeset has policy failures
            if (checkinEvent.PolicyFailures.Count > 0)
            {

            }
        }
    }
}
