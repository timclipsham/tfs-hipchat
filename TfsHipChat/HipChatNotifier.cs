using System.DirectoryServices.AccountManagement;
using HipChat;
using TfsHipChat.Configuration;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public class HipChatNotifier : IHipChatNotifier
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly HipChatClient _hipChatClient;
        
        public HipChatNotifier(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
            _hipChatClient = new HipChatClient
            {
                Token = _configurationProvider.Config.HipChatToken,
            };
        }

        public void SendCheckinNotification(CheckinEvent checkinEvent, int roomId)
        {
            var message = string.Format("{0} - <a href=\"{1}\">{2}</a>",
                                        checkinEvent.GetCommitterName(),
                                        checkinEvent.GetChangesetUrl(),
                                        checkinEvent.Comment.Replace("\n", "<br>"));
            _hipChatClient.From = GetFromValue("Source");
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.yellow);
        }

        public void SendBuildFailedNotification(BuildCompletionEvent buildEvent, int roomId)
        {
            var message = string.Format("{0} build <a href=\"{1}\">{2}</a> {3} (requested by {4})",
                buildEvent.TeamProject, buildEvent.Url, buildEvent.Id, buildEvent.CompletionStatus,
                buildEvent.GetRequestedByName());
            _hipChatClient.From = GetFromValue("Build");
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.red);
        }

        public void SendBuildSuccessNotification(BuildCompletionEvent buildEvent, int roomId)
        {
            var message = string.Format("{0} build <a href=\"{1}\">{2}</a> {3} (requested by {4})",
                buildEvent.TeamProject, buildEvent.Url, buildEvent.Id, buildEvent.CompletionStatus,
                buildEvent.GetRequestedByName());
            _hipChatClient.From = GetFromValue("Build");
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.green);
        }

        public void SendTaskStateChangedNotification(WorkItemChangedEvent changedEvent, int roomId)
        {
            var title = changedEvent.GetFieldOrNull("Title").NewValue;
            var state = changedEvent.GetFieldOrNull("State").NewValue;
            var message = string.Format("<a href='{0}'>{1}</a> is <i>{2}</i>",
                changedEvent.DisplayUrl,
                title, 
                state);
            var userName = GetAssignedToOrNull(changedEvent) ?? GetChangedByOrNull(changedEvent);
            if (userName != null)
            {
                message = string.Format("{0} - {1}", userName, message);
            }
            _hipChatClient.From = GetFromValue("Task");
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.yellow);
        }

        public void SendTaskChangedRemainingNotification(WorkItemChangedEvent changedEvent, int roomId)
        {
            var title = changedEvent.GetFieldOrNull("Title").NewValue;
            var remaining = changedEvent.GetFieldOrNull("Remaining Work");
            var message = string.Format("<a href='{0}'>{1}</a> hours changed: {2} &#8658; {3}",
                                        changedEvent.DisplayUrl,
                                        title,
                                        remaining.OldValue,
                                        remaining.NewValue);
            var userName = GetAssignedToOrNull(changedEvent) ?? GetChangedByOrNull(changedEvent);
            if (userName != null)
            {
                message = string.Format("{0} - {1}", userName, message);
            }

            _hipChatClient.From = GetFromValue("Task");
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.yellow);
        }

        public void SendTaskHistoryCommentNotification(WorkItemChangedEvent changedEvent, int roomId)
        {
            var shortTitle = Shorten(changedEvent.GetFieldOrNull("Title").NewValue, 25);
            var userName = GetChangedByOrNull(changedEvent) ?? GetAssignedToOrNull(changedEvent);
            var comment = Shorten(changedEvent.GetTextFieldOrNull("History").Value, 250);
            var message = string.Format("<a href='{0}'>{1}</a> comment: <i>{2}</i>",
                                        changedEvent.DisplayUrl,
                                        shortTitle,
                                        comment);
            if (userName != null)
            {
                message = string.Format("{0} - {1}", userName, message);
            }
            _hipChatClient.From = GetFromValue("Task");
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.purple);
        }

        public void SendTaskOwnerChangedNotification(WorkItemChangedEvent changedEvent, int roomId)
        {
            var title = changedEvent.GetFieldOrNull("Title").NewValue;
            var assignedTo = changedEvent.GetFieldOrNull("Assigned To").NewValue;
            var message = string.Format("<a href='{0}'>{1}</a> assigned to <i>{2}</i>",
                changedEvent.DisplayUrl,
                title, 
                assignedTo);
            _hipChatClient.From = GetFromValue("Task");
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.yellow);
        }

        public void SendBuildStatusChangedNotification(BuildStatusChangeEvent buildStatusChangedEvent, int roomId)
        {
            var changedBy = GetDisplayNameFromUsername(buildStatusChangedEvent.ChangedBy) 
                ?? buildStatusChangedEvent.ChangedBy;
            var message = string.Format("{0} - Build <a href='{1}'>{2}</a> queued for deploy to <i>{3}</i>",
                changedBy,
                buildStatusChangedEvent.Url,
                buildStatusChangedEvent.Id,
                buildStatusChangedEvent.StatusChange.NewValue);
            _hipChatClient.From = GetFromValue("Deploy");
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.green);
        }

        private string GetFromValue(string fallback)
        {
            return _configurationProvider.Config == null || string.IsNullOrEmpty(_configurationProvider.Config.HipChatFrom)
                       ? fallback
                       : _configurationProvider.Config.HipChatFrom;
        }

        public static string Shorten(string text, int preferredMaxLength)
        {
            if (text == null)
            {
                return text;
            }
            if (text.Length > preferredMaxLength + 8)
            {
                return text.Substring(0, preferredMaxLength - 3) + "..";
            }
            return text;
        }

        private static string GetAssignedToOrNull(WorkItemChangedEvent workItemChangedEvent)
        {
            var assignedToField = workItemChangedEvent.GetFieldOrNull("Assigned To");
            if (assignedToField != null && !string.IsNullOrEmpty(assignedToField.NewValue))
            {
                return assignedToField.NewValue;
            }
            return null;
        }

        private static string GetChangedByOrNull(WorkItemChangedEvent workItemChangedEvent)
        {
            var changedByField = workItemChangedEvent.GetFieldOrNull("Changed By");
            if (changedByField != null && !string.IsNullOrEmpty(changedByField.NewValue))
            {
                return changedByField.NewValue;
            }
            return null;
        }

        private string GetDisplayNameFromUsername(string username)
        {
            try
            {
                var searchContext = new PrincipalContext(ContextType.Domain, "SW");
                var adUser = UserPrincipal.FindByIdentity(searchContext, username);
                return adUser.DisplayName;
            }
            catch
            {
                return null;
            }
        }
    }
}
