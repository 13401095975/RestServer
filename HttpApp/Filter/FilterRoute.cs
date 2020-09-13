using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestServer.Filter
{
    public class FilterRoute
    {
        public string Prefix { get; set; }
        public IFilter Filter { get; set; }

        public FilterRoute() { }
        public FilterRoute(string prefix, IFilter filter) {
            this.Prefix = prefix;
            this.Filter = filter;
        }
    }
}
