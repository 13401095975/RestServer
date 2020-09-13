using RestServer;
using RestServer.Config;
using System.Collections.Generic;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            new RestApplicationServer().run();
           //new RestApplicationServer().run(new RestConfiguration { 
           //     StaticFileConfigurations = new List<StaticFileConfiguration>() { 
           //         new StaticFileConfiguration("/e", "E:\\"),
           //         new StaticFileConfiguration("/f", "F:\\")
           //     }
           // });
        }
    }
}
