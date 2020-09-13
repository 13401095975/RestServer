using System;

namespace RestServer.RestAttribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequestMappingAttribute : Attribute
    {
        public string Method { get; set; }
        public string Path { get; set; }

        public RequestMappingAttribute(string method, string path) {
            this.Method = method;
            this.Path = path;
        }

    }
}
