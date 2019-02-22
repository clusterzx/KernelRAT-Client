using System;
using System.Text;
using System.Runtime.InteropServices;
namespace Kernel.Functions.Devices {
    class Drive {
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi)]
        protected static extern int mciSendString
           (string mciCommand,
           StringBuilder returnValue,
           int returnLength,
           IntPtr callback);

        public static void Use(bool pointer) {
            if (pointer) { mciSendString("set cdaudio door open", null, 0, IntPtr.Zero); } else { mciSendString("set cdaudio door closed", null, 0, IntPtr.Zero); }
        }
    }
}
