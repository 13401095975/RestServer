using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.Filter
{
    public class FilterChain
    {
        public SortedList<int, IFilter> filterList = new SortedList<int, IFilter>();

        private int pos = 0;

        public RouteDispatcher dispatcher { get; set; }

        public void AddFilter(int key, IFilter filter) {
            filterList.Add(key, filter);
        }



        public void doFilter(HttpRequest request,ref HttpResponse response) { 
            if(pos < filterList.Count)
            {
                IFilter filter = filterList.Values[pos++];

                filter.Filter(request,ref response, this);
                
            }
            //过滤器执行完成后，调用route
            dispatcher.dispatcher(request,ref response);
            pos = 0;
            return;
        }
    }
}
