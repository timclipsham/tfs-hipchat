using HipChat;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public class HipChatNotifier : INotifier
    {
        private readonly HipChatClient _hipChatClient;

        public HipChatNotifier()
        {
            _hipChatClient = new HipChatClient
            {
                Token = Properties.Settings.Default.HipChat_Token,
                From = Properties.Settings.Default.HipChat_From
            };
        }

        public void SendCheckinNotification(CheckinEvent checkinEvent, int roomId)
        {
            var message = string.Format("{0} checked in changeset <a href=\"{1}\">{2}</a> ({4})<br>{3}",
                checkinEvent.Committer, checkinEvent.GetChangesetUrl(), checkinEvent.Number,
                checkinEvent.Comment, checkinEvent.TeamProject);
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.yellow);
        }

        public void SendBuildCompletionFailedNotification(BuildCompletionEvent buildEvent, int roomId)
        {
            var message = string.Format("{0} build <a href=\"{1}\">{2}</a> {3} (requested by {4})",
                buildEvent.TeamProject, buildEvent.Url, buildEvent.Id, buildEvent.CompletionStatus,
                buildEvent.RequestedBy);
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.red);
        }

        public void SendBuildCompletionSuccessNotification(BuildCompletionEvent buildEvent, int roomId)
        {
            var message = string.Format("{0} build <a href=\"{1}\">{2}</a> {3} (requested by {4})",
                buildEvent.TeamProject, buildEvent.Url, buildEvent.Id, buildEvent.CompletionStatus,
                buildEvent.RequestedBy);
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.green);
        }
    }
}
