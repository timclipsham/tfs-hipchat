using HipChat;
using Microsoft.TeamFoundation.VersionControl.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsHipChat
{
    public class HipChatNotifier : INotifier
    {
        private HipChatClient _hipChatClient;

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
            var message = string.Format("{0} checked in changeset {1}\n{2}", checkinEvent.Committer, checkinEvent.Number, checkinEvent.Comment);
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.yellow);
        }
    }
}
