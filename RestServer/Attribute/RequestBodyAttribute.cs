using System;

namespace RestServer.RestAttribute
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class RequestBodyAttribute : Attribute
    {
        public RequestBodyAttribute() {
        }

    }
}
