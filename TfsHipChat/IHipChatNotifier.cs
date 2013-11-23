using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public interface IHipChatNotifier
    {
        void SendCheckinNotification(CheckinEvent checkinEvent, int roomId);
        void SendBuildFailedNotification(BuildCompletionEvent buildEvent, int roomId);
        void SendBuildSuccessNotification(BuildCompletionEvent buildEvent, int roomId);
        void SendTaskStateChangedNotification(WorkItemChangedEvent changedEvent, int roomId);
        void SendTaskChangedRemainingNotification(WorkItemChangedEvent changedEvent, int roomId);
        void SendTaskHistoryCommentNotification(WorkItemChangedEvent changedEvent, int roomId);
        void SendTaskOwnerChangedNotification(WorkItemChangedEvent changedEvent, int roomId);
        void SendBuildStatusChangedNotification(BuildStatusChangeEvent buildStatusChangedEvent, int roomId);
    }
}
