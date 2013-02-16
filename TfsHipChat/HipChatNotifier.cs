using HipChat;
using Microsoft.TeamFoundation.VersionControl.Common;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public class HipChatNotifier : INotifier
    {
        private readonly HipChatClient _hipChatClient;

        public HipChatNotifier(int roomId)
        {
            _hipChatClient = new HipChatClient
            {
                Token = Properties.Settings.Default.HipChat_Token,
                RoomId = roomId,
                From = Properties.Settings.Default.HipChat_From
            };
        }

        public void SendCheckinNotification(CheckinEvent checkinEvent)
        {
            var message = string.Format("{0} checked in changeset <a href=\"{1}\">{2}</a> to {4}<br>{3}",
                checkinEvent.Committer, checkinEvent.GetChangesetUrl(), checkinEvent.Number, checkinEvent.Comment, checkinEvent.TeamProject);
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.yellow);
        }

        public void SendBuildCompletionFailedNotification(BuildCompletionEvent buildEvent)
        {
            var message = string.Format("{0} (requested by {1}) Failed", buildCompletionEvent.Title, buildCompletionEvent.RequestedBy);
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.red);
        }

        public void SendBuildCompletionSuccessNotification(BuildCompletionEvent buildCompletionEvent)
        {
            var message = string.Format("{0} (requested by {1})", buildCompletionEvent.Title, buildCompletionEvent.RequestedBy);
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.green);
        }
    }
}
