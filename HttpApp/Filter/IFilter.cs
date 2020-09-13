using SimpleHttpServer.Models;

namespace HttpApp.Filter
{
    public interface IFilter
    {
        void Filter(HttpRequest request,ref HttpResponse response, FilterChain next);
    }
}
