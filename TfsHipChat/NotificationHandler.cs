using System.Linq;
using TfsHipChat.Configuration;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public class NotificationHandler : INotificationHandler
    {
        private readonly IHipChatNotifier _hipChatNotifier;
        private readonly IConfigurationProvider _configurationProvider;

        public NotificationHandler(IHipChatNotifier hipChatNotifier, IConfigurationProvider configurationProvider)
        {
            _hipChatNotifier = hipChatNotifier;
            _configurationProvider = configurationProvider;
        }

        public void HandleCheckin(CheckinEvent checkinEvent)
        {
            var teamProjectMapping = FindTeamProjectMapping(checkinEvent.TeamProject);

            if (teamProjectMapping == null)
            {
                return;
            }

            if (IsNotificationSubscribedTo(teamProjectMapping, Notification.Checkin))
            {
                _hipChatNotifier.SendCheckinNotification(checkinEvent, teamProjectMapping.HipChatRoomId);
            }
        }

        public void HandleBuildCompletion(BuildCompletionEvent buildEvent)
        {
            var teamProjectMapping = FindTeamProjectMapping(buildEvent.TeamProject);

            if (teamProjectMapping == null)
            {
                return;
            }

            var notification = (buildEvent.CompletionStatus == "Successfully Completed")
                                   ? Notification.BuildCompletionSuccess
                                   : Notification.BuildCompletionFailure;

            if (!IsNotificationSubscribedTo(teamProjectMapping, notification))
            {
                return;
            }

            if (notification == Notification.BuildCompletionSuccess)
            {
                _hipChatNotifier.SendBuildSuccessNotification(buildEvent, teamProjectMapping.HipChatRoomId);
            }
            else
            {
                _hipChatNotifier.SendBuildFailedNotification(buildEvent, teamProjectMapping.HipChatRoomId);
            }
        }

        public void HandleBuildCompleted(BuildCompletedEvent buildEvent)
        {
            throw new System.NotImplementedException();
        }

        private TeamProjectMapping FindTeamProjectMapping(string teamProject)
        {
            return
                _configurationProvider.Config.TeamProjectMappings.SingleOrDefault(
                    t => t.TeamProject.ToLower() == teamProject.ToLower());
        }

        private static bool IsNotificationSubscribedTo(TeamProjectMapping teamProjectMapping, Notification notification)
        {
            return teamProjectMapping.Notifications == null ||
                teamProjectMapping.Notifications.Contains(notification);
        }
    }
}
