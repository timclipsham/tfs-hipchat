using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public interface INotificationHandler
    {
        void HandleCheckinEvent(CheckinEvent checkinEvent);
        void HandleBuildCompletionEvent(BuildCompletionEvent buildEvent);
    }
}
