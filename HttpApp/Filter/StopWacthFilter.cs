using HttpApp.attribute;
using SimpleHttpServer.Models;
using System;
using System.Diagnostics;

namespace HttpApp.Filter
{
    [WebFilter(1, "/.*")]
    public class StopWacthFilter : IFilter
    {
        public void Filter(HttpRequest request,ref HttpResponse response, FilterChain next)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            next.doFilter(request,ref response);
            stopwatch.Stop();
            Console.WriteLine(request.Method + " "+request.Path+ ", 耗时："+(stopwatch.ElapsedMilliseconds).ToString()+"ms");
        }
    }
}
