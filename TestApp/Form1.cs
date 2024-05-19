namespace TestApp
{
    using Hotkeys;

    public partial class Form1 : Form
    {
        public class Hotkey : IEquatable<Hotkey>
        {
            public Keys Key { get; set; }
            public ModifierKeys Modifier { get; set; }

            public override bool Equals(object? obj) =>
                Equals(obj as Hotkey);
            public bool Equals(Hotkey? other) =>
                other is not null
                && Key == other.Key
                && Modifier == other.Modifier;

            public override int GetHashCode() =>
                HashCode.Combine(Key, Modifier);
        }

        public static readonly HotkeyManager HotkeyManager = new();

        public Form1() => InitializeComponent();

        protected override void OnLoad(EventArgs e)
        {
            HotkeyManager.HotkeyPressed += HotkeyManager_HotkeyPressed;

            base.OnLoad(e);
        }

        private void HotkeyManager_HotkeyPressed(
            object? sender,
            HotkeyPressedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(HotkeyManager_HotkeyPressed);
                return;
            }

            DgvHotkeys.ClearSelection();

            TmrRowHighlight.Stop();
            foreach (var r in DgvHotkeys.Rows.Cast<DataGridViewRow>())
            {
                r.DefaultCellStyle.BackColor = Color.White;
            }

            var hotkey = new Hotkey
            {
                Key = e.PressedKey,
                Modifier = e.ModifierKeys,
            };

            var row = DgvHotkeys.Rows
                .Cast<DataGridViewRow>()
                .FirstOrDefault(r => hotkey.Equals(r.Tag));
            if (row is null)
            {
                return;
            }

            row.DefaultCellStyle.BackColor = Color.Green;
            TmrRowHighlight.Start();
        }

        private void BtnRegisterHotkey_Click(object sender, EventArgs e)
        {
            HotkeyManager.RemoveAllHotkeys();

            try
            {
                using var dlg = new SelectHotkeyDlg();
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                var hotkey = new Hotkey
                {
                    Key = dlg.Key,
                    Modifier = dlg.Modifier,
                };

                if (DgvHotkeys.Rows
                    .Cast<DataGridViewRow>()
                    .Any(r => hotkey.Equals(r.Tag)))
                {
                    _ = MessageBox.Show("Hotkey already registered.");
                    return;
                }

                var row = new DataGridViewRow
                {
                    Tag = hotkey,
                };

                _ = row.Cells.Add(new DataGridViewTextBoxCell
                {
                    Value = HotkeyManager.ToString(hotkey.Key, hotkey.Modifier),
                });

                _ = DgvHotkeys.Rows.Add(row);
            }
            finally
            {
                foreach (var hotkey in DgvHotkeys.Rows
                    .Cast<DataGridViewRow>()
                    .Select(r => r.Tag as Hotkey))
                {
                    _ = HotkeyManager.AddHotkey(hotkey!.Key, hotkey!.Modifier);
                }
            }

        }

        private void BtnRemoveHotkey_Click(object sender, EventArgs e)
        {
            if (DgvHotkeys.SelectedRows.Count != 1)
            {
                return;
            }

            var hotkey = DgvHotkeys.SelectedRows[0].Tag as Hotkey;

            HotkeyManager.RemoveHotkey(hotkey!.Key, hotkey!.Modifier);
        }

        private void BtnRemoveAllHotkeys_Click(object sender, EventArgs e) =>
            HotkeyManager.RemoveAllHotkeys();

        private void TmrRowHighlight_Tick(object sender, EventArgs e)
        {
            TmrRowHighlight.Stop();
            foreach (var row in DgvHotkeys.Rows.Cast<DataGridViewRow>())
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }
    }
}
