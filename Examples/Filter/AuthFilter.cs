using RestServer.Filter;
using RestServer.Http;
using RestServer.RestAttribute;
using System;

namespace Examples.Filter
{
    [WebFilter(2, "/api")]
    public class AuthFilter : IFilter
    {
        public void Filter(HttpRequest request,ref HttpResponse response, FilterChain chain, int nextIndex)
        {
            Console.WriteLine("auth filter start");
            chain.doFilter(request,ref response, nextIndex);
            Console.WriteLine("auth filter return");
        }
    }
}
