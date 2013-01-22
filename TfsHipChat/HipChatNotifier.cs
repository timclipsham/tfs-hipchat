using HipChat;
using Microsoft.TeamFoundation.VersionControl.Common;
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
                RoomId = Properties.Settings.Default.HipChat_RoomId,
                From = Properties.Settings.Default.HipChat_From
            };
        }

        public void SendCheckinNotification(CheckinEvent checkinEvent)
        {
            var message = string.Format("{0} checked in changeset <a href=\"{1}\">{2}</a><br>{3}",
                checkinEvent.CommitterDisplay, checkinEvent.GetChangesetUrl(), checkinEvent.Number,
                checkinEvent.Comment);

            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.yellow);
        }

        public void SendBuildCompletionFailedNotification(BuildCompletionEvent buildEvent)
        {
            var message = string.Format("{0} build <a href=\"{1}\">{2}</a> {3} (requested by {4})",
                buildEvent.TeamProject, buildEvent.Url, buildEvent.Id, buildEvent.CompletionStatus,
                buildEvent.RequestedBy);

            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.red);
        }
    }
}
