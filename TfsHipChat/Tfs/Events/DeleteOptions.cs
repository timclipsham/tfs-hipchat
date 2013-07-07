using System;

namespace TfsHipChat.Tfs.Events
{
    [Flags]
    public enum DeleteOptions
    {
        None = 0,
        DropLocation = 1,
        TestResults = 2,
        Label = 4,
        Details = 8,
        Symbols = 16,
        All = Symbols | Details | Label | TestResults | DropLocation,
    }
}