using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.attribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GetMappingAttribute : Attribute
    {
        public string Method { get; set; } = "GET";
        public string Path { get; set; }
        public GetMappingAttribute(string path) {
            this.Path = path;
        }

    }
}
