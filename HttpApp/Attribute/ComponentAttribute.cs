using System;

namespace RestServer.RestAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public string Name { get; set; }

        public ComponentAttribute(string name) {
            this.Name = name;
        }

    }
}
