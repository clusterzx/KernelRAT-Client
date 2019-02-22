using System;
using System.IO;
using System.Text;

namespace Kernel.Functions.Surveillance {
    class FileSystem {
        public static string ListDir(string Dir) {
            string[] Dirs = Directory.GetDirectories(Dir);
            string[] Files = Directory.GetFiles(Dir);
            string Result = null;
            foreach (string dir in Dirs) {
                Result += dir + ", ";
            }
            foreach (string file in Files) {
                Result += file + ", ";
            }
            return Result.Substring(0, Result.Length - 2) + ".";
        }
    }
}
