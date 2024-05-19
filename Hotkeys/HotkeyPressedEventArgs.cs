namespace Hotkeys
{
    using System;
    using System.Windows.Forms;

    public class HotkeyPressedEventArgs : EventArgs
    {
        public Keys PressedKey { get; set; }
        public ModifierKeys ModifierKeys { get; set; }
    }
}
