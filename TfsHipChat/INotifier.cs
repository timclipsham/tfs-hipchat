using Microsoft.TeamFoundation.VersionControl.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsHipChat.Events;

namespace TfsHipChat
{
    public interface INotifier
    {
        void SendCheckinNotification(CheckinEvent checkinEvent);
        void SendBuildCompletionFailedNotification(BuildCompletionEvent buildCompletionEvent);
    }
}
