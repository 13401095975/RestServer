using HttpApp.attribute;
using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.Filter
{
    [WebFilter(2, "/.*")]
    public class AuthFilter : IFilter
    {
        public void Filter(HttpRequest request,ref HttpResponse response, FilterChain next)
        {
            Console.WriteLine("auth filter start");
            next.doFilter(request,ref response);
            Console.WriteLine("auth filter return");
        }
    }
}
