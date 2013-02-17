using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public interface INotifier
    {
        void SendCheckinNotification(CheckinEvent checkinEvent, int roomId);
        void SendBuildCompletionFailedNotification(BuildCompletionEvent buildEvent, int roomId);
        void SendBuildCompletionSuccessNotification(BuildCompletionEvent buildEvent, int roomId);
    }
}
