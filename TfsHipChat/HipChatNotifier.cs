using HipChat;
using Microsoft.TeamFoundation.VersionControl.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TfsHipChat
{
    public class HipChatNotifier : INotifier
    {
        private HipChatClient _hipChatClient;
        private string _tfsServerUrl;

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
            var message = string.Format("{0} checked in changeset <a href=\"{1}\">{2}</a><br>{3}",
                checkinEvent.CommitterDisplay, changesetUrl, checkinEvent.Number, checkinEvent.Comment);
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.yellow);
        }

        private string BuildChangesetUrl(int changesetNumber)
        {
            return _tfsServerUrl + "_versionControl/changeset?id=" + changesetNumber;
        }
    }
}
