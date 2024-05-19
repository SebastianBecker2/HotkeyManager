namespace Hotkeys
{
    using System;

    [Flags]
    public enum ModifierKeys
    {
        None = 0,
        Alt = 1 << 0,
        Shift = 1 << 1,
        Control = 1 << 2,
        Windows = 1 << 3,
        All = Alt | Shift | Control | Windows,
    }
}
