using System.Diagnostics;
namespace Kernel.Functions.Performance {
    class Shell {
        public static void Hide(string command) {
            ProcessStartInfo psi = new ProcessStartInfo("cmd", "/c " + command);
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(psi);
        }
        public static void Show(string command) {
            ProcessStartInfo psi = new ProcessStartInfo("cmd", "/k " + command);
            Process.Start(psi);
        }
        public static string Reverse(string command) {
            string str = null;
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c " + command;
            p.StartInfo.StandardOutputEncoding = System.Text.Encoding.GetEncoding("cp866");
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            str = p.StandardOutput.ReadToEnd();
            p.Close();
            p.Dispose();
            return str;
        }
    }
}
