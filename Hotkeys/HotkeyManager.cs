namespace Hotkeys
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class HotkeyManager : IDisposable
    {
        private enum HookType
        {
            CallWndProc = 4,
            CallWndProCret = 12,
            Cbt = 5,
            Debug = 9,
            ForeGroundIdle = 11,
            GetMessage = 3,
            JournalPlayBkac = 1,
            JournalRecord = 0,
            Keyboard = 2,
            KeyboardLowLevel = 13,
            Mouse = 7,
            MouseLowLevel = 14,
            MsgFilter = -1,
            Shell = 10,
            SysMsgFilter = 6,
        }

        private enum KeyPressEventType
        {
            KeyDown = 0x0100,
            KeyUp = 0x0101,
            SysKeyDown = 0x0104,
            SysKeyUp = 0x0105,
        }

        private class Hotkey : IEquatable<Hotkey>
        {
            public Keys Key { get; set; }
            public ModifierKeys ModifierKeys { get; set; }

            public override bool Equals(object? obj) =>
                Equals(obj as Hotkey);
            public bool Equals(Hotkey? other) =>
                other is not null
                && Key == other.Key
                && ModifierKeys == other.ModifierKeys;
            public override int GetHashCode() =>
                HashCode.Combine(Key, ModifierKeys);

            public static bool operator ==(Hotkey left, Hotkey right) =>
                EqualityComparer<Hotkey>.Default.Equals(left, right);
            public static bool operator !=(Hotkey left, Hotkey right) =>
                !(left == right);
        }

        [StructLayout(LayoutKind.Sequential)]
        private class KeyboardLowLevelInputEvent
        {
            public Keys VirtualKeyCode { get; set; }
            public int ScanCode { get; set; }
            public int Flags { get; set; }
            public int Time { get; set; }
            public UIntPtr ExtraInfo { get; set; }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int hookCode,
            IntPtr keyPressEventType,
            KeyboardLowLevelInputEvent keyboardEvent);

        private static LowLevelKeyboardProc? lowLevelKeyboardCallbackDelegate;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(
            HookType hookType,
            LowLevelKeyboardProc callback,
            IntPtr library,
            uint threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hookHandle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(
            IntPtr hookHandle,
            int hookCode,
            IntPtr keyPressEventType,
            KeyboardLowLevelInputEvent keyboardEvent);

        public event EventHandler<KeyPressedEventArgs>? KeyPressed;
        protected virtual void OnKeyPressed(
            Keys key,
            ModifierKeys modifierKeys) =>
            KeyPressed?.Invoke(this, new KeyPressedEventArgs
            {
                PressedKey = key,
                ModifierKeys = modifierKeys,
            });

        public event EventHandler<HotkeyPressedEventArgs>? HotkeyPressed;
        protected virtual void OnHotkeyPressed(
            Keys key,
            ModifierKeys modifierKeys) =>
            HotkeyPressed?.Invoke(this, new HotkeyPressedEventArgs
            {
                PressedKey = key,
                ModifierKeys = modifierKeys,
            });

        private HashSet<Hotkey> RegisteredHotkeys { get; set; } = [];
        private IntPtr? KeyboardHookHandle { get; set; }
        private ModifierKeys CurrentModifierKeys { get; set; }

        private bool disposedValue;

        public HotkeyManager()
        {
            lowLevelKeyboardCallbackDelegate =
                new LowLevelKeyboardProc(LowLevelKeyboardCallback);
            KeyboardHookHandle = SetWindowsHookEx(
                HookType.KeyboardLowLevel,
                lowLevelKeyboardCallbackDelegate,
                IntPtr.Zero,
                0);
        }

        public bool AddHotkey(
            Keys key,
            ModifierKeys modifierKeys = ModifierKeys.None)
        {
            // Local copy to avoid locking
            var localCopy = new HashSet<Hotkey>(RegisteredHotkeys);
            var result = localCopy.Add(new Hotkey
            {
                Key = key,
                ModifierKeys = modifierKeys,
            });
            RegisteredHotkeys = localCopy;
            return result;
        }

        public bool RemoveHotkey(
            Keys key,
            ModifierKeys modifierKeys = ModifierKeys.None)
        {
            // Local copy to avoid locking
            var localCopy = new HashSet<Hotkey>(RegisteredHotkeys);
            var result = localCopy.Remove(new Hotkey
            {
                Key = key,
                ModifierKeys = modifierKeys,
            });
            RegisteredHotkeys = localCopy;
            return result;
        }

        public void RemoveAllHotkeys() =>
            RegisteredHotkeys = [];

        public static string ToString(Keys key, ModifierKeys modifier)
        {
            var text = "";
            if (modifier.HasFlag(ModifierKeys.Shift))
            {
                text += ModifierKeys.Shift + " + ";
            }
            if (modifier.HasFlag(ModifierKeys.Control))
            {
                text += ModifierKeys.Control + " + ";
            }
            if (modifier.HasFlag(ModifierKeys.Alt))
            {
                text += ModifierKeys.Alt + " + ";
            }
            if (modifier.HasFlag(ModifierKeys.Windows))
            {
                text += ModifierKeys.Windows + " + ";
            }
            text += key.ToString();

            return text;
        }

        private bool UpdateModifierKeys(
            IntPtr keyPressEventType,
            Keys pressedKey)
        {
            // We assume key down. Should be right about 50% of the time. ;)
            Action<ModifierKeys> applyModifier =
                (ModifierKeys modifierKey) => CurrentModifierKeys |= modifierKey;

#pragma warning disable IDE0078 // Use pattern matching
            if (keyPressEventType == (IntPtr)KeyPressEventType.KeyUp
                    || keyPressEventType == (IntPtr)KeyPressEventType.SysKeyUp)
#pragma warning restore IDE0078 // Use pattern matching
            {
                applyModifier = (ModifierKeys modifierKey) =>
                   CurrentModifierKeys &= ~modifierKey;
            }

#pragma warning disable IDE0010 // Add missing cases
            switch (pressedKey)
#pragma warning restore IDE0010 // Add missing cases
            {
                case Keys.ShiftKey:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    applyModifier(ModifierKeys.Shift);
                    return true;

                case Keys.ControlKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                    applyModifier(ModifierKeys.Control);
                    return true;

                case Keys.Menu:
                case Keys.LMenu:
                case Keys.RMenu:
                    applyModifier(ModifierKeys.Alt);
                    return true;

                case Keys.LWin:
                case Keys.RWin:
                    applyModifier(ModifierKeys.Windows);
                    return true;
            }
            return false;
        }

        private IntPtr LowLevelKeyboardCallback(
            int hookCode,
            IntPtr keyPressEventType,
            KeyboardLowLevelInputEvent keyboardEvent)
        {
            var hookResult = CallNextHookEx(
                (IntPtr)0,
                hookCode,
                keyPressEventType,
                keyboardEvent);

            if (hookCode < 0)
            {
                return hookResult;
            }

            try
            {
                if (UpdateModifierKeys(
                    keyPressEventType,
                    keyboardEvent.VirtualKeyCode))
                {
                    return hookResult;
                }

                // We only care about key down
#pragma warning disable IDE0078 // Use pattern matching
                if (keyPressEventType == (IntPtr)KeyPressEventType.KeyDown
                    || keyPressEventType == (IntPtr)KeyPressEventType.SysKeyDown)
#pragma warning restore IDE0078 // Use pattern matching
                {
                    OnKeyPressed(
                        keyboardEvent.VirtualKeyCode,
                        CurrentModifierKeys);

                    var localRef = RegisteredHotkeys;
                    if (localRef.Count == 0)
                    {
                        return hookResult;
                    }
                    if (localRef.Contains(new Hotkey
                    {
                        Key = keyboardEvent.VirtualKeyCode,
                        ModifierKeys = CurrentModifierKeys,
                    }))
                    {
                        OnHotkeyPressed(
                            keyboardEvent.VirtualKeyCode,
                            CurrentModifierKeys);
                    }
                }
            }
            catch (Exception)
            {
                CurrentModifierKeys = ModifierKeys.None;
            }
            return hookResult;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (KeyboardHookHandle.HasValue)
                    {
                        _ = UnhookWindowsHookEx(KeyboardHookHandle.Value);
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
