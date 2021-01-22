using RestServer;
using RestServer.Config;
using System.Collections.Generic;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            //new RestApplicationServer().run();
           new RestApplicationServer().run(new RestConfiguration { 
                StaticFileConfigurations = new List<StaticFileConfiguration>() { 
                    new StaticFileConfiguration("/", @"C:\Users\13401\Documents\ShareX\Tools\"),
           //         new StaticFileConfiguration("/f", "F:\\")
                }
            });
        }
    }
}
