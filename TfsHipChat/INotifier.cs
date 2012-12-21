using Microsoft.TeamFoundation.VersionControl.Common;
using TfsHipChat.Events;

namespace TfsHipChat
{
    public interface INotifier
    {
        void SendCheckinNotification(CheckinEvent checkinEvent);
        void SendBuildCompletionFailedNotification(BuildCompletionEvent buildCompletionEvent);
    }
}
