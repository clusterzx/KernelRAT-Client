using System;
using System.Runtime.InteropServices;

namespace Kernel.Functions.Performance {
    class Message {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);
        public static void Show(string boxname, string inbox, int type) {
            MessageBox((IntPtr)0, inbox, boxname, type);
        }
    }
}
