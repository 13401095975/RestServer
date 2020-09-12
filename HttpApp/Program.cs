using HttpApp.attribute;
using HttpApp.Logger;
using SimpleHttpServer;
using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new RestApplication().run();
        }

    }
}
