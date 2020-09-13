using RestServer.Http;

namespace RestServer.Filter
{
    public interface IFilter
    {
        void Filter(HttpRequest request,ref HttpResponse response, FilterChain chain, int curPos);
    }
}
