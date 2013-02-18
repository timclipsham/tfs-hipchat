using System.Linq;
using TfsHipChat.Configuration;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public class NotificationHandler : INotificationHandler
    {
        private readonly IHipChatNotifier _hipChatNotifier;
        private readonly IConfigurationProvider _configurationProvider;

        // TODO: replace poor man's IoC with a full solution
        public NotificationHandler()
            : this(new HipChatNotifier(), new ConfigurationProvider())
        {
        }

        public NotificationHandler(IHipChatNotifier hipChatNotifier, IConfigurationProvider configurationProvider)
        {
            _hipChatNotifier = hipChatNotifier;
            _configurationProvider = configurationProvider;
        }

        public void HandleCheckin(CheckinEvent checkinEvent)
        {
            var teamProjectMapping = FindTeamProjectMapping(checkinEvent.TeamProject);

            if (teamProjectMapping != null)
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

            if (buildEvent.CompletionStatus == "Successfully Completed")
            {
                _hipChatNotifier.SendBuildSuccessNotification(buildEvent, teamProjectMapping.HipChatRoomId);
            }
            else
            {
                _hipChatNotifier.SendBuildFailedNotification(buildEvent, teamProjectMapping.HipChatRoomId);
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
