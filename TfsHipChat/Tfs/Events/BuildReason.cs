using System;

namespace TfsHipChat.Tfs.Events
{
    [Flags]
    public enum BuildReason
    {
        None = 0,
        Manual = 1,
// ReSharper disable InconsistentNaming
        IndividualCI = 2,
        BatchedCI = 4,
// ReSharper restore InconsistentNaming
        Schedule = 8,
        ScheduleForced = 16,
        UserCreated = 32,
        ValidateShelveset = 64,
        CheckInShelveset = 128,
        Triggered = CheckInShelveset | UserCreated | ScheduleForced | Schedule | BatchedCI | IndividualCI | Manual,
        All = Triggered | ValidateShelveset,
    }
}