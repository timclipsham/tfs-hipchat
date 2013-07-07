using System;

namespace TfsHipChat.Tfs.Events
{
    [Flags]
    public enum ScheduleDays
    {
        None = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 4,
        Thursday = 8,
        Friday = 16,
        Saturday = 32,
        Sunday = 64,
        All = Sunday | Saturday | Friday | Thursday | Wednesday | Tuesday | Monday,
    }
}