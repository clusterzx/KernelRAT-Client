using System;
using System.Runtime.InteropServices;
using System.Threading;
namespace Kernel.Functions.Devices {
    class Keyboard {
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,
            int dwExtraInfo);

        public static void Press(char symv) {
            byte[] KeysBytes = new byte[26] { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B,
                    0x4C, 0x4D, 0x4E, 0x4F, 0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A };

            char[] KeysName = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                    'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

            for (int i = 0; i < 26; i++) {
                if (symv == KeysName[i]) {
                    keybd_event(KeysBytes[i], 0x45, 1 | 0, 0);
                }
            }

        }
    }
}
