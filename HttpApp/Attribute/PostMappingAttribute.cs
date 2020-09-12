using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.attribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PostMappingAttribute : Attribute
    {
        public string Method { get; set; } = "POST";
        public string Path { get; set; }

        public PostMappingAttribute(string path) {
            this.Path = path;
        }

    }
}
