using SimpleHttpServer;
using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HttpApp
{
    public class RouteDispatcher
    {
        private List<Route> Routes = new List<Route>();

        public RouteDispatcher(List<Route> routeList)
        {
            this.Routes.AddRange(routeList);
        }

        public void AddRoutes(List<Route> routeList)
        {
            this.Routes.AddRange(routeList);
        }

        public void dispatcher(HttpRequest request, ref HttpResponse response) {
            RouteRequest(request, ref response);
        }

        protected void RouteRequest(HttpRequest request, ref HttpResponse response)
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
                HttpBuilder.Ok(ref response, Serialize(o));
                return;
            }
            catch (Exception ex)
            {
                HttpBuilder.InternalServerError(ref response);
                return;
            }
        }

        public string Serialize(object data)
        {
            if (data is string || data is int || data is String || data is Int16)
            {
                return data.ToString();
            }
            else
            {
                return new JavaScriptSerializer().Serialize(data);
            }
        }
    }
}
