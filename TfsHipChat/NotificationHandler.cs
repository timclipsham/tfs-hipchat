using System.Collections.Generic;
using System.Linq;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public class NotificationHandler : INotificationHandler
    {
        private readonly INotifier _notifier;
        private readonly IDictionary<string, int> _teamProjectMap;

        // TODO: replace poor man's IoC with a full solution
        public NotificationHandler()
            : this(new HipChatNotifier(),
                   Properties.Settings.Default.ProjectRoomMap.OfType<string>()
                             .Select(s => s.Split('|'))
                             .ToDictionary(arr => arr[0].ToLower(), arr => int.Parse(arr[1])))
        {
        }
        
        public NotificationHandler(INotifier notifier, IDictionary<string, int> teamProjectMap)
        {
            _notifier = notifier;
            _teamProjectMap = teamProjectMap;
        }

        public void HandleCheckinEvent(CheckinEvent checkinEvent)
        {
            var teamProject = checkinEvent.TeamProject.ToLower();
            int roomId;

            if (_teamProjectMap.TryGetValue(teamProject, out roomId))
            {
                _notifier.SendCheckinNotification(checkinEvent, roomId);
            }
        }

        public void HandleBuildCompletionEvent(BuildCompletionEvent buildEvent)
        {
            var teamProject = buildEvent.TeamProject.ToLower();
            int roomId;

            if (!_teamProjectMap.TryGetValue(teamProject, out roomId))
            {
                return;
            }

            if (buildEvent.CompletionStatus == "Successfully Completed")
            {
                _notifier.SendBuildCompletionSuccessNotification(buildEvent, roomId);
            }
            else
            {
                _notifier.SendBuildCompletionFailedNotification(buildEvent, roomId);
            }
        }
    }
}
