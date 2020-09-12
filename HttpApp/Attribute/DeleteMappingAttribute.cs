using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.attribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DeleteMappingAttribute : Attribute
    {
        public string Method { get; set; } = "DELETE";
        public string Path { get; set; }

        public DeleteMappingAttribute(string path) {
            this.Path = path;
        }

    }
}
