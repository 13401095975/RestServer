using System;

namespace RestServer.Config
{
    public class ServerConfig
    {
        /**
         * 监听端口，默认8080
         */
        public static int Port { get; set; } = 8080;

        public static string ServerName { get; set; } = "RestServer";

        /**
         * 是否开启压缩，默认开启
         */
        public static bool EnableCompress { get; set; } = true;

        /**
         * 压缩时的最小数据量，即低于该值不压缩
         */
        public static int MinCompressSize { get; set; } = 1024;

    }
}
