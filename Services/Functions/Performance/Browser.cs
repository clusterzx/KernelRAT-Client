using System;
using System.Diagnostics;
namespace Kernel.Functions.Performance {
    class Browser {
        public static void OpenURL(string url) {
            Uri URI = new Uri(url);
            ProcessStartInfo psi;
            psi = new ProcessStartInfo("cmd", @"/k explorer " + URI);
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(psi);
        }
    }
}
