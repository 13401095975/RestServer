using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ControllerAttribute : Attribute
    {
        public string Name { get; set; }

        public ControllerAttribute(string name) {
            this.Name = name;
        }

    }
}
