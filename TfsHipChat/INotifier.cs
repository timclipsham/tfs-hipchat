using Microsoft.TeamFoundation.VersionControl.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsHipChat
{
    public interface INotifier
    {
        void SendCheckinNotification(CheckinEvent checkinEvent);
    }
}
