using RestServer.Http;
using RestServer.RestAttribute;

namespace RestServer.Filter
{
    [WebFilter(int.MinValue, "/")]
    class CorsFilter : IFilter
    {
        public void Filter(HttpRequest request, ref HttpResponse response, ProcessChain chain, int curPos)
        {
            response.Headers.SetAllowCredentials(true);
            response.Headers.SetAllowOrigin("*");
            if (request.Method.Equals(HttpMethod.OPTIONS.ToString()))
            {
                return;
            }
            else {
                chain.doFilter(request,ref response, curPos);
            }
        }
    }
}
