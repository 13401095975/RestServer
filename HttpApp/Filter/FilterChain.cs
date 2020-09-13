using RestServer.Http;
using RestServer.RouteHandler;
using System.Collections.Generic;

namespace RestServer.Filter
{
    public class FilterChain
    {
        public SortedList<int, FilterRoute> FilterList = new SortedList<int, FilterRoute>();

        public List<IRouteHandler> RouteHandlerList = new List<IRouteHandler>();

        public void AddFilter(int key, string prefix,IFilter filter) {
            FilterList.Add(key, new FilterRoute(prefix, filter));
        }

        public void AddRouteHandler(IRouteHandler handler) {
            RouteHandlerList.Add(handler);
        }

        public void Handle(HttpRequest request, ref HttpResponse response) {
            doFilter(request, ref response, 0);
        }

        public void doFilter(HttpRequest request,ref HttpResponse response, int curPos) {
            if (curPos >= FilterList.Count) {
                //过滤器执行完成后，调用route
                foreach (IRouteHandler routeHandler in RouteHandlerList)
                {
                    if (routeHandler.IsMatch(request))
                    {
                        routeHandler.Handler(request, ref response);
                        break;
                    }
                }
                return;
            }
            FilterRoute filterRoute = FilterList.Values[curPos];
            if (request.Path.StartsWith(filterRoute.Prefix)) {
                IFilter filter = filterRoute.Filter;
                filter.Filter(request, ref response, this, curPos + 1);
            }
            else
            {
                doFilter(request, ref response, curPos + 1);
            }
            
            return;
        }
    }
}
