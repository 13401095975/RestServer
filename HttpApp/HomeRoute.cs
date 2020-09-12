using HttpApp.attribute;
using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp
{
    [Controller("HomeRoute")]
    public class HomeRoute
    {
       
        [RequestMapping("GET","/")]
        public string home(HttpRequest request)
        {
            return "Hello from SimpleHttpServer";
        }

        [RequestMapping("GET", "/info")]
        public string info(HttpRequest request)
        {
            return "Hello from info*";
            
        }
    }
}
