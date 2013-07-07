using System.IO;
using System.ServiceModel;
using System.Xml.Serialization;
using System;
using System.Xml.Linq;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TfsHipChatEventService : IEventService
    {
        private readonly INotificationHandler _notificationHandler;

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
                    _notificationHandler.HandleCheckin(
                        DeserializeXmlToType<CheckinEvent>(eventXml));
                    break;

                case "BuildCompletionEvent":
                    _notificationHandler.HandleBuildCompletion(
                        DeserializeXmlToType<BuildCompletionEvent>(eventXml));
                    break;

                case "BuildCompletionEvent2":
                    _notificationHandler.HandleBuildCompletion(
                        ConvertToBuildCompletionEvent(
                            DeserializeXmlToType<BuildCompletionEvent2>(eventXml)));
                    break;

                case "BuildCompletedEvent":
                    _notificationHandler.HandleBuildCompletion(
                        ConvertToBuildCompletionEvent(
                            DeserializeXmlToType<BuildCompletedEvent>(eventXml)));
                    break;

                default:
                    throw new NotSupportedException("The event received is not supported.");
            }
        }

        private static BuildCompletionEvent ConvertToBuildCompletionEvent(BuildCompletedEvent buildEvent)
        {
            return new BuildCompletionEvent
                       {
                           TeamFoundationServerUrl = buildEvent.TeamProjectCollectionUrl,
                           TeamProject = buildEvent.Build.TeamProject,
                           Id = buildEvent.Build.BuildNumber,
                           Url = buildEvent.WebAccessUri,
                           Title = buildEvent.Title,
                           CompletionStatus =
                               (buildEvent.Build.Status == BuildStatus.Succeeded)
                               ? "Successfully Completed"
                               : buildEvent.Build.Status.ToString(),
                           Subscriber = buildEvent.Subscriber,
                           //Configuration = "" // No conversion available from BuildCompletedEvent
                           RequestedBy = buildEvent.Requests[0].RequestedBy,
                           TimeZone = buildEvent.TimeZone,
                           TimeZoneOffset = buildEvent.TimeZoneOffset,
                           BuildStartTime = buildEvent.Build.StartTime.ToString("dd/MM/yyyy hh:mm:ss tt"),
                           BuildCompleteTime = buildEvent.Build.FinishTime.ToString("dd/MM/yyyy hh:mm:ss tt"),
                           BuildMachine = buildEvent.Controller.Name
                       };
        }

        private static BuildCompletionEvent ConvertToBuildCompletionEvent(BuildCompletionEvent2 buildEvent)
        {
            return new BuildCompletionEvent
            {
                TeamFoundationServerUrl = buildEvent.TeamFoundationServerUrl,
                TeamProject = buildEvent.TeamProject,
                Id = buildEvent.BuildNumber,
                Url = buildEvent.Url,
                Title = buildEvent.Title,
                CompletionStatus = buildEvent.Status,
                Subscriber = buildEvent.Subscriber,
                //Configuration = "" // No conversion available from BuildCompletionEvent2
                RequestedBy = buildEvent.RequestedBy,
                TimeZone = buildEvent.TimeZone,
                TimeZoneOffset = buildEvent.TimeZoneOffset,
                BuildStartTime = buildEvent.StartTime,
                BuildCompleteTime = buildEvent.FinishTime,
                BuildMachine = buildEvent.AgentPath
            };
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
