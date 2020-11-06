using RestServer.Common.Logger;
using RestServer.Filter;
using RestServer.Http;
using RestServer.RestAttribute;
using System;

namespace Examples.Filter
{
    [WebFilter(2, "/api")]
    public class AuthFilter : IFilter
    {
        private ILogger logger = LoggerFactory.GetLogger();
        public void Filter(HttpRequest request,ref HttpResponse response, ProcessChain chain, int nextIndex)
        {
            logger.Info("auth filter start");
            chain.doFilter(request,ref response, nextIndex);
            logger.Info("auth filter return");
        }
    }
}
