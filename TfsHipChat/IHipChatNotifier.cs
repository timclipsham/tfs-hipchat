using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public interface IHipChatNotifier
    {
        void SendCheckinNotification(CheckinEvent checkinEvent, int roomId);
        void SendBuildFailedNotification(BuildCompletionEvent buildEvent, int roomId);
        void SendBuildSuccessNotification(BuildCompletionEvent buildEvent, int roomId);
    }
}
