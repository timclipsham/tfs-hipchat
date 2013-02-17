using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System;
using System.Xml.Linq;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public class TfsHipChatEventService : IEventService
    {
        private readonly INotifier _notifier;
        private readonly IDictionary<string, int> _teamProjectMap;

        // TODO: replace poor man's IoC with a full solution
        public TfsHipChatEventService()
            : this(new HipChatNotifier(),
                   Properties.Settings.Default.ProjectRoomMap.OfType<string>()
                             .Select(s => s.Split('|'))
                             .ToDictionary(arr => arr[0].ToLower(), arr => int.Parse(arr[1])))
        {
        }

        public TfsHipChatEventService(INotifier notifier, IDictionary<string, int> teamProjectMap)
        {
            _notifier = notifier;
            _teamProjectMap = teamProjectMap;
        }

        public void Notify(string eventXml, string tfsIdentityXml)
        {
            var xml = XElement.Parse(eventXml);

            switch (xml.Name.LocalName)
            {
                case "CheckinEvent":
                    {
                        var checkinEvent = DeserializeXmlToType<CheckinEvent>(eventXml);
                        var teamProject = checkinEvent.TeamProject.ToLower();
                        int roomId;

                        if (_teamProjectMap.TryGetValue(teamProject, out roomId))
                        {
                            _notifier.SendCheckinNotification(checkinEvent, roomId);
                        }

                        break;
                    }

                case "BuildCompletionEvent":
                    {
                        var buildCompletionEvent = DeserializeXmlToType<BuildCompletionEvent>(eventXml);
                        var teamProject = buildCompletionEvent.TeamProject.ToLower();
                        int roomId;

                        if (!_teamProjectMap.TryGetValue(teamProject, out roomId))
                        {
                            return;
                        }

                        if (buildCompletionEvent.CompletionStatus == "Successfully Completed")
                        {
                            _notifier.SendBuildCompletionSuccessNotification(buildCompletionEvent, roomId);
                        }
                        else
                        {
                            _notifier.SendBuildCompletionFailedNotification(buildCompletionEvent, roomId);
                        }
                        break;
                    }
                default:
                    throw new NotSupportedException("The event received is not supported.");
            }
        }

        private T DeserializeXmlToType<T>(string eventXml) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(eventXml))
            {
                return serializer.Deserialize(reader) as T;
            }
        }
    }
}
