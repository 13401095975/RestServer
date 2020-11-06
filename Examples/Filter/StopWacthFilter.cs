using RestServer.Common.Logger;
using RestServer.Filter;
using RestServer.Http;
using RestServer.RestAttribute;
using System;
using System.Diagnostics;

namespace Examples.Filter
{
    [WebFilter(1, "/")]
    public class StopWacthFilter : IFilter
    {
        private ILogger logger = LoggerFactory.GetLogger();
        public void Filter(HttpRequest request,ref HttpResponse response, ProcessChain chain, int nextIndex)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            chain.doFilter(request,ref response, nextIndex);
            stopwatch.Stop();
            logger.Info(request.Method + " "+request.Path+ ", 耗时："+(stopwatch.ElapsedMilliseconds).ToString()+"ms");
        }
    }
}
