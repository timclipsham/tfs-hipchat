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
        private readonly IDictionary<string, INotifier> _projectMap;

        // TODO: replace poor man's IoC with a full solution
        public TfsHipChatEventService()
        {
            _projectMap = Properties.Settings.Default.ProjectRoomMap.OfType<string>().Select(x => x.Split('|')).ToDictionary(x => x[0].ToLower(), x => (INotifier)new HipChatNotifier(Convert.ToInt32(x[1])));
        }

        public TfsHipChatEventService(IDictionary<string, INotifier> projectMap)
        {
            _projectMap = projectMap;
        }
        
        public void Notify(string eventXml, string tfsIdentityXml)
        {
            var xml = XElement.Parse(eventXml);

            switch (xml.Name.LocalName)
            {
                case "CheckinEvent":
                    {
                        var checkinEvent = DeserializeXmlToType<CheckinEvent>(eventXml);
                        var teamproject = checkinEvent.TeamProject.ToLower();
                        INotifier notifier;

                        if (_projectMap.TryGetValue(teamproject, out notifier))
                        {
                            notifier.SendCheckinNotification(checkinEvent);
                        }

                        break;
                    }
                case "BuildCompletionEvent":
                    {
                        var buildCompletionEvent = DeserializeXmlToType<BuildCompletionEvent>(eventXml);
                        var teamproject = buildCompletionEvent.TeamProject.ToLower();
                        INotifier notifier;

                        if (!_projectMap.TryGetValue(teamproject, out notifier))
                        {
                            return;
                        }

                        if (buildCompletionEvent.CompletionStatus == "Successfully Completed")
                        {
                            notifier.SendBuildCompletionSuccessNotification(buildCompletionEvent);
                        }
                        else
                        {
                            notifier.SendBuildCompletionFailedNotification(buildCompletionEvent);
                        }
                        break;
                    }
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
