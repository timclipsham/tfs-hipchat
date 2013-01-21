using HipChat;
using System.Text.RegularExpressions;
using Microsoft.TeamFoundation.VersionControl.Common;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public class HipChatNotifier : INotifier
    {
        private readonly HipChatClient _hipChatClient;
        private readonly string _tfsServerUrl;

        public HipChatNotifier()
        {
            _hipChatClient = new HipChatClient
            {
                Token = Properties.Settings.Default.HipChat_Token,
                RoomId = Properties.Settings.Default.HipChat_RoomId,
                From = Properties.Settings.Default.HipChat_From
            };

            _tfsServerUrl = Regex.Replace(Properties.Settings.Default.TfsServerUrl, "[\\/]+$", "") + "/";
        }

        public void SendCheckinNotification(CheckinEvent checkinEvent)
        {
            var changesetUrl = BuildChangesetUrl(checkinEvent.Number);
            var commonPath = VersionedItemAnalyzer.GetCommonPath(checkinEvent.GetVersionedItems());
            var message = string.Format("{0} checked in changeset <a href=\"{1}\">{2}</a> ({3})<br>{4}",
                checkinEvent.CommitterDisplay, changesetUrl, checkinEvent.Number, commonPath,
                checkinEvent.Comment);
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.yellow);
        }

        public void SendBuildCompletionFailedNotification(BuildCompletionEvent buildCompletionEvent)
        {
            var message = string.Format("{0} (requested by {1})", buildCompletionEvent.Title, buildCompletionEvent.RequestedBy);
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.red);
        }

        private string BuildChangesetUrl(int changesetNumber)
        {
            return _tfsServerUrl + "_versionControl/changeset?id=" + changesetNumber;
        }
    }
}
