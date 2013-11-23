using System.Linq;
using TfsHipChat.Configuration;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public class NotificationHandler : INotificationHandler
    {
        private readonly IHipChatNotifier _hipChatNotifier;
        private readonly IConfigurationProvider _configurationProvider;
        private readonly ILogger _logger;

        public NotificationHandler(IHipChatNotifier hipChatNotifier, IConfigurationProvider configurationProvider)
        {
            _hipChatNotifier = hipChatNotifier;
            _configurationProvider = configurationProvider;
            _logger = LoggerInstance.Current;
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

        public void HandleWorkItemChanged(WorkItemChangedEvent changedEvent)
        {
            var teamProjectMapping = FindTeamProjectMapping(changedEvent.PortfolioProject);

            if (teamProjectMapping == null)
            {
                _logger.Trace("Ignoring project {0}", changedEvent.PortfolioProject);
                return;
            }

            var workItemTypeField = changedEvent.GetFieldOrNull("Work Item Type");
            if (workItemTypeField == null)
            {
                _logger.Warn("Could not find field 'Work Item Type'");
                return;
            }

            if (changedEvent.ChangeType == "Change")
            {
                if (workItemTypeField.NewValue == "Task")
                {
                    if (!IsNotificationSubscribedTo(teamProjectMapping, Notification.TaskWorkChange))
                    {
                        return;
                    }

                    var stateChangedField = changedEvent.GetChangedFieldOrNull("State");
                    if (stateChangedField != null)
                    {
                        _hipChatNotifier.SendTaskStateChangedNotification(
                            changedEvent, teamProjectMapping.HipChatRoomId);
                    }

                    var stateField = changedEvent.GetFieldOrNull("State");
                    var remainingWorkChangedField = changedEvent.GetChangedFieldOrNull("Remaining Work");
                    if (remainingWorkChangedField != null
                        && stateField.NewValue == "In Progress")
                    {
                        _hipChatNotifier.SendTaskChangedRemainingNotification(
                            changedEvent, teamProjectMapping.HipChatRoomId);
                    }

                    var assignedToField = changedEvent.GetChangedFieldOrNull("Assigned To");
                    if (assignedToField != null 
                        && assignedToField.NewValue != assignedToField.OldValue
                        && stateField.NewValue == "In Progress")
                    {
                        _hipChatNotifier.SendTaskOwnerChangedNotification(
                            changedEvent, teamProjectMapping.HipChatRoomId);
                    }

                    var historyField = changedEvent.GetTextFieldOrNull("History");
                    if (historyField != null)
                    {
                        if (!historyField.Value.StartsWith("Associated with changeset") &&
                            !historyField.Value.Contains(
                                "The Fixed In field was updated as part of associating work items with the build."))
                        {
                            _hipChatNotifier.SendTaskHistoryCommentNotification(changedEvent,
                                                                                teamProjectMapping.HipChatRoomId);
                        }
                    }
                }
                else
                {
                    _logger.Info(string.Format("Ignoring unhandled field type: {0}", workItemTypeField.NewValue));
                    return;
                }
            }
            else if (changedEvent.ChangeType == "New")
            {
                _logger.Trace("Ignoring new work item event");
                return;
            }
        }

        public void HandleBuildStatusChange(BuildStatusChangeEvent buildStatusChangedEvent)
        {
            var teamProjectMapping = FindTeamProjectMapping(buildStatusChangedEvent.TeamProject);
            if (!string.IsNullOrEmpty(buildStatusChangedEvent.StatusChange.NewValue))
            {
                _hipChatNotifier.SendBuildStatusChangedNotification(
                    buildStatusChangedEvent, teamProjectMapping.HipChatRoomId);
            }
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
