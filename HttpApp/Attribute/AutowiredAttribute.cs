using System;

namespace RestServer.RestAttribute
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AutowiredAttribute : Attribute
    {

        public AutowiredAttribute() {
        }

    }
}
