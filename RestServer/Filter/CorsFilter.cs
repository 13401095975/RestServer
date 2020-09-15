using RestServer.Http;
using RestServer.RestAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestServer.Filter
{
    [WebFilter(int.MinValue, "/")]
    class CorsFilter : IFilter
    {
        public void Filter(HttpRequest request, ref HttpResponse response, ProcessChain chain, int curPos)
        {
            response.Headers.SetAllowCredentials(true);
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
