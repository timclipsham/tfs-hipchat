using HipChat;
using TfsHipChat.Configuration;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public class HipChatNotifier : IHipChatNotifier
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly HipChatClient _hipChatClient;

        public HipChatNotifier() : this(new ConfigurationProvider())
        {
        }
        
        public HipChatNotifier(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
            _hipChatClient = new HipChatClient
            {
                Token = _configurationProvider.Config.HipChatToken,
                From = _configurationProvider.Config.HipChatFrom
            };
        }

        public void SendCheckinNotification(CheckinEvent checkinEvent, int roomId)
        {
            var message = string.Format("{0} checked in changeset <a href=\"{1}\">{2}</a> ({4})<br>{3}",
                checkinEvent.CommitterDisplay, checkinEvent.GetChangesetUrl(), checkinEvent.Number,
                checkinEvent.Comment, checkinEvent.TeamProject);
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.yellow);
        }

        public void SendBuildFailedNotification(BuildCompletionEvent buildEvent, int roomId)
        {
            var message = string.Format("{0} build <a href=\"{1}\">{2}</a> {3} (requested by {4})",
                buildEvent.TeamProject, buildEvent.Url, buildEvent.Id, buildEvent.CompletionStatus,
                buildEvent.RequestedBy);
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.red);
        }

        public void SendBuildSuccessNotification(BuildCompletionEvent buildEvent, int roomId)
        {
            var message = string.Format("{0} build <a href=\"{1}\">{2}</a> {3} (requested by {4})",
                buildEvent.TeamProject, buildEvent.Url, buildEvent.Id, buildEvent.CompletionStatus,
                buildEvent.RequestedBy);
            _hipChatClient.RoomId = roomId;
            _hipChatClient.SendMessage(message, HipChatClient.BackgroundColor.green);
        }
    }
}
