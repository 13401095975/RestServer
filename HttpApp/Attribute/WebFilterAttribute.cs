using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WebFilterAttribute : Attribute
    {
        public int Order { get; set; }
        public string UrlPatterns { get; set; }
        public string Name { get; set; }

        public WebFilterAttribute(int order, string path) {
            this.Order = order;
            this.UrlPatterns = path;
        }

    }
}
