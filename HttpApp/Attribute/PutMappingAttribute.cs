using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.attribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PutMappingAttribute : Attribute
    {
        public string Method { get; set; } = "PUT";
        public string Path { get; set; }

        public PutMappingAttribute(string path) {
            this.Path = path;
        }

    }
}
