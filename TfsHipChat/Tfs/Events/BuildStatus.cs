using System;

namespace TfsHipChat.Tfs.Events
{
    [Flags]
    public enum BuildStatus
    {
        None = 0,
        InProgress = 1,
        Succeeded = 2,
        PartiallySucceeded = 4,
        Failed = 8,
        Stopped = 16,
        NotStarted = 32,
        All = NotStarted | Stopped | Failed | PartiallySucceeded | Succeeded | InProgress,
    }
}