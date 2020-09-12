using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp
{
    public class ServerConfig
    {
        public static int Port { get; set; } = 8080;

        public static bool Compress { get; set; } = true;
        public static int minCompressSize { get; set; } = 1024;
    }
}
