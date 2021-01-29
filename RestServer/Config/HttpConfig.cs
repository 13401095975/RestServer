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

        /**
	     * Prefix of the charset clause in a content type String: ";charset="
	     */
        public static string CONTENT_TYPE_CHARSET_PREFIX = ";charset=";

        public static string DefaultCharsetEncoding { get; set; } = "utf-8";

    }
}
