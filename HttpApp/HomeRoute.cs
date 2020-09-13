using HttpApp.attribute;
using HttpApp.Serializer;
using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

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
        public Student info(HttpRequest request)
        {
            return new Student("xieyun", 30);
            
        }
        [RequestMapping("POST", "/info")]
        public string create(HttpRequest request)
        {
            Student s = JsonSerializer.FromJson<Student>(request.Content);
            Console.WriteLine(s.ToString());
            return "ok";

        }
    }
}
