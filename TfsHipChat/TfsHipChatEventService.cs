using System.IO;
using System.Xml.Serialization;
using Microsoft.TeamFoundation.VersionControl.Common;
using TfsHipChat.Events;
using System;

namespace TfsHipChat
{
    public class TfsHipChatEventService : IEventService
    {
        private INotifier _notifier;

        // TODO: replace poor man's IoC with a full solution
        public TfsHipChatEventService() : this(new HipChatNotifier())
        {
        }

        public TfsHipChatEventService(INotifier notifier)
        {
            this._notifier = notifier;
        }

        public void Notify(string eventXml, string tfsIdentityXml)
        {
            var serializer = new XmlSerializer(typeof(CheckinEvent));

            using (var reader = new StringReader(eventXml))
            {
                CheckinEvent checkinEvent = null;

                try
                {
                    checkinEvent = serializer.Deserialize(reader) as CheckinEvent;
                }
                catch (InvalidOperationException)
                {
                }

                if (checkinEvent != null)
                {
                    _notifier.SendCheckinNotification(checkinEvent);
                    return;
                }
            }

            serializer = new XmlSerializer(typeof(BuildCompletionEvent));

            using (var reader = new StringReader(eventXml))
            {

                BuildCompletionEvent buildCompletionEvent = null;

                try
                {
                    buildCompletionEvent = serializer.Deserialize(reader) as BuildCompletionEvent;
                }
                catch (InvalidOperationException)
                {
                }

                if (buildCompletionEvent != null)
                {
                    if (buildCompletionEvent.CompletionStatus != "Successfully Completed")
                    {
                        _notifier.SendBuildCompletionFailedNotification(buildCompletionEvent);
                    }
                    
                    return;
                }
            }

            throw new NotSupportedException("The event received is not supported.");
        }
    }
}
