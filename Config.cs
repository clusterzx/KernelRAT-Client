namespace Kernel {
    public class Config {
        public const string MalwareFileName = "svnjoi.exe";
        public const string MalwareCacheDir = "D:\\";

        public const string MalwareServerAddr = "http://127.0.0.1/malware/";
        public const bool FreeHostAdaptive = false;
        public const string MalwareServerKey = "root";

        public const string MalwareRegistryName = "Malware";
        public const string MalwareMigratePoint = "sys.sfg";
        public const bool MalwareBasicImplementation = false;

        public const int MalwareInitializationDelay = 1000;
        public const int MalwareRequestDelay = 1000;
    }
}
