using System;

namespace RestServer.Config
{
    public class HttpConfig
    {
        
        /**
         * 默认contentType
         */
        public static string DefaultContentType { get; set; } = "application/json";

        /**
         * 压缩编码类型，默认gzip
         */
        public static string DefaultContentEncoding { get; set; } = "gzip";

        public static string DefaultServerRoot { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

        public static string DefaultStaticFilePrefix { get; set; } = "/static";

        public static string DefaultCharsetEncoding { get; set; } = "charset=utf-8";

    }
}
