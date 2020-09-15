using System;

namespace RestServer.RestAttribute
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class RequestParamAttribute : Attribute
    {
        public string Name { get; set; }

        public RequestParamAttribute(string name) {
            this.Name = name;
        }

    }
}
