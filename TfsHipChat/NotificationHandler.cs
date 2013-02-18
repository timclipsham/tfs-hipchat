using System.Linq;
using TfsHipChat.Configuration;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public class NotificationHandler : INotificationHandler
    {
        private readonly INotifier _notifier;
        private readonly IConfigurationProvider _configurationProvider;

        // TODO: replace poor man's IoC with a full solution
        public NotificationHandler()
            : this(new HipChatNotifier(), new ConfigurationProvider())
        {
        }

        public NotificationHandler(INotifier notifier, IConfigurationProvider configurationProvider)
        {
            _notifier = notifier;
            _configurationProvider = configurationProvider;
        }

        public void HandleCheckinEvent(CheckinEvent checkinEvent)
        {
            var teamProjectMapping = FindTeamProjectMapping(checkinEvent.TeamProject);

            if (teamProjectMapping != null)
            {
                _notifier.SendCheckinNotification(checkinEvent, teamProjectMapping.HipChatRoomId);
            }
        }

        public void HandleBuildCompletionEvent(BuildCompletionEvent buildEvent)
        {
            var teamProjectMapping = FindTeamProjectMapping(buildEvent.TeamProject);

            if (teamProjectMapping == null)
            {
                return;
            }

            if (buildEvent.CompletionStatus == "Successfully Completed")
            {
                _notifier.SendBuildCompletionSuccessNotification(buildEvent, teamProjectMapping.HipChatRoomId);
            }
            else
            {
                _notifier.SendBuildCompletionFailedNotification(buildEvent, teamProjectMapping.HipChatRoomId);
            }
        }

        private TeamProjectMapping FindTeamProjectMapping(string teamProjectName)
        {
            return
                _configurationProvider.Config.TeamProjectMappings.SingleOrDefault(
                    t => t.TeamProjectName.ToLower() == teamProjectName.ToLower());
        }
    }
}
