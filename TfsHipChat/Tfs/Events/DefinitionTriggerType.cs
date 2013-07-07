using System;

namespace TfsHipChat.Tfs.Events
{
    [Flags]
    public enum DefinitionTriggerType
    {
        None = 1,
        ContinuousIntegration = 2,
        BatchedContinuousIntegration = 4,
        Schedule = 8,
        ScheduleForced = 16,
        GatedCheckIn = 32,
        BatchedGatedCheckIn = 64,
        All = BatchedGatedCheckIn | GatedCheckIn | ScheduleForced | Schedule | BatchedContinuousIntegration | ContinuousIntegration | None,
    }
}