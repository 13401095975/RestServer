using RestServer.Http;

namespace RestServer.RouteHandler
{
    public interface IRouteHandler
    {
        bool IsMatch(HttpRequest request);
        void Handler(HttpRequest request,ref HttpResponse response);
    }
}
