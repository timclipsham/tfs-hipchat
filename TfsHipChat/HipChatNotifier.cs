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
                Token = Properties.Settings.Default.Token,
                RoomId = Properties.Settings.Default.RoomId,
                From = Properties.Settings.Default.From
            };
        }

        public void SendCheckinNotification(CheckinEvent checkinEvent)
        {
            var message = string.Format("Check-in by {0} (changeset {1}): {2}", checkinEvent.Committer, checkinEvent.Number, checkinEvent.Comment);
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.yellow);
        }
    }
}
