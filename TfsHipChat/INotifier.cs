using Microsoft.TeamFoundation.VersionControl.Common;
using TfsHipChat.Tfs.Events;

namespace TfsHipChat
{
    public interface INotifier
    {
        void SendCheckinNotification(CheckinEvent checkinEvent);
        void SendBuildCompletionFailedNotification(BuildCompletionEvent buildCompletionEvent);
    }
}
