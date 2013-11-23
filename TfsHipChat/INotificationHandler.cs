using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public interface INotificationHandler
    {
        void HandleCheckin(CheckinEvent checkinEvent);
        void HandleBuildCompletion(BuildCompletionEvent buildEvent);
        void HandleWorkItemChanged(WorkItemChangedEvent changedEvent);
        void HandleBuildStatusChange(BuildStatusChangeEvent buildStatusChangedEvent);
    }
}
