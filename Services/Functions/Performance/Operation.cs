using System.Diagnostics;
namespace Kernel.Functions.Performance {
    class Operation {
        public static void Kill(string proc) {
            foreach (Process p in Process.GetProcessesByName(proc)) {
                p.Kill();
                p.WaitForExit();
            }
        }
    }
}

