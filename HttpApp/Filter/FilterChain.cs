using HttpApp.RouteHandlers;
using SimpleHttpServer.Models;
using System.Collections.Generic;

namespace HttpApp.Filter
{
    public class FilterChain
    {
        public SortedList<int, IFilter> FilterList = new SortedList<int, IFilter>();

        private int pos = 0;

        public List<IRouteHandler> RouteHandlerList = new List<IRouteHandler>();

        public void AddFilter(int key, IFilter filter) {
            FilterList.Add(key, filter);
        }

        public void AddRouteHandler(IRouteHandler handler) {
            RouteHandlerList.Add(handler);
        }

        public void doFilter(HttpRequest request,ref HttpResponse response) { 
            if(pos < FilterList.Count)
            {
                IFilter filter = FilterList.Values[pos++];

                filter.Filter(request,ref response, this);
                
            }
            //过滤器执行完成后，调用route
            foreach (IRouteHandler routeHandler in RouteHandlerList) {
                if (routeHandler.IsMatch(request)) {
                    routeHandler.Handler(request,ref response);
                    break;
                }
            }
            pos = 0;
            return;
        }
    }
}
