using RestServer.Common;
using RestServer.Common.Serializer;
using RestServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestServer.RouteHandler
{
    public class ApiRouteHandler : IRouteHandler
    {
        private List<Route> Routes = new List<Route>();

        public ApiRouteHandler(List<Route> routeList)
        {
            this.Routes.AddRange(routeList);
        }

        public void AddRoutes(List<Route> routeList)
        {
            this.Routes.AddRange(routeList);
        }

        public void dispatcher(HttpRequest request, ref HttpResponse response) {
            Handler(request, ref response);
        }

        public void Handler(HttpRequest request, ref HttpResponse response)
        {
            List<Route> routes = Routes.Where(x => x.Path == request.Path).ToList();

            if (!routes.Any())
            {
                HttpBuilder.NotFound(ref response);
                return;
            }

            Route route = routes.SingleOrDefault(x => x.Method == request.Method);

            if (route == null)
            {
                HttpBuilder.NotAllowed(ref response);
                return;
            }
            try
            {
                object o = route.Handler(request);
                HttpBuilder.Ok(ref response, JsonSerializer.ToJson(o));
                return;
            }
            catch (Exception ex)
            {
                HttpBuilder.InternalServerError(ref response);
                return;
            }
        }

        public bool IsMatch(HttpRequest request)
        {
            return true;
        }
    }
}
