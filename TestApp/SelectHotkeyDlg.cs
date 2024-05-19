namespace TestApp
{
    using System;
    using System.Windows.Forms;
    using Hotkeys;

    public partial class SelectHotkeyDlg : Form
    {
        public Keys Key { get; set; } = Keys.None;
        public ModifierKeys Modifier { get; set; } = Hotkeys.ModifierKeys.None;

        public SelectHotkeyDlg() => InitializeComponent();

        protected override void OnLoad(EventArgs e)
        {
            Form1.HotkeyManager.KeyPressed += HotkeyManager_KeyPressed;
            base.OnLoad(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            Form1.HotkeyManager.KeyPressed -= HotkeyManager_KeyPressed;
            base.OnClosed(e);
        }

        private void HotkeyManager_KeyPressed(
            object? sender,
            KeyPressedEventArgs e)
        {
            Key = e.PressedKey;
            Modifier = e.ModifierKeys;

            Invoke(new Action(() =>
                LblHotkey.Text = HotkeyManager.ToString(Key, Modifier)));
        }
    }
}
