namespace HttpApp
{
    public class ServerConfig
    {
        public static int Port { get; set; } = 8080;

        public static bool EnableCompress { get; set; } = true;
        public static int MinCompressSize { get; set; } = 1024;
    }
}
