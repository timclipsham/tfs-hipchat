using System;

namespace TfsHipChat.Tfs.Events
{
    [Flags]
    public enum QueueStatus
    {
        None = 0,
        InProgress = 1,
        Retry = 2,
        Queued = 4,
        Postponed = 8,
        Completed = 16,
        Canceled = 32,
        All = Canceled | Completed | Postponed | Queued | Retry | InProgress,
    }
}