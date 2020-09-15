using System;

namespace RestServer.RestAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WebFilterAttribute : Attribute
    {
        public int Order { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }

        public WebFilterAttribute(int order, string path) {
            this.Order = order;
            this.Prefix = path;
        }

    }
}
