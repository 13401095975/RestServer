using System.Reflection;

namespace SimpleHttpServer.Models
{
    public class Route
    {
        /**
         * 请求方法
         */
        public string Method { get; set; }

        /*
         * 请求路径
         */
        public string Path { get; set; }

        public bool isRegex { get; set; } = false;

        /**
         * 对应类的实例信息
         */
        public object instance { get; set; }
        /**
         * 对应的方法信息
         */
        public MethodInfo method { get; set; }

        /**
         * 所有路由的方法的参数必须为 HttpRequest request
         */
        public object Handler(HttpRequest request) {
            return method.Invoke(instance, new object[] { request });
        }

        
        
    }
}
