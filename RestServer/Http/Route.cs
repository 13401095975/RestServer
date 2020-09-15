using RestServer.Common.Logger;
using RestServer.Common.Serializer;
using RestServer.RestAttribute;
using System;
using System.Reflection;

namespace RestServer.Http
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

        public ParameterInfo[] parameterInfo { get; set; }

        private ILogger logger = LoggerFactory.GetLogger();
        /**
         * 
         */
        public object Handler(HttpRequest request) {
            if (parameterInfo == null || parameterInfo.Length == 0) { 
                return method.Invoke(instance, new object[] {});
            }
            object[] param = new object[parameterInfo.Length];

            for (int i=0; i< parameterInfo.Length; i++) {
                ParameterInfo parameter = parameterInfo[i];


                System.Collections.Generic.IEnumerable<Attribute> enumerable = parameter.GetCustomAttributes();
                int count = 0;
                foreach (Attribute attr in enumerable)
                {
                    count += 1;
                    if (attr is RequestParamAttribute)
                    {
                        param[i] = getValueFromUrl(parameter, request);
                    }
                    else if (attr is RequestBodyAttribute)
                    {
                        param[i] = getValueFromBody(parameter, request);
                    }
                    else {
                        logger.Warn("Unknown attribute:" + attr.ToString());
                        param[i] = null;
                    }
                }
                /**
                 * 没有注解
                 */
                if (count == 0) {
                    Type t = parameter.ParameterType;
                    if (t == typeof(HttpRequest))
                    {
                        param[i] = request;
                    }
                    else {
                        param[i] = getValueFromUrl(parameter, request);
                    }
                }

            }

            return method.Invoke(instance, param);
        }
        private object getValueFromUrl(ParameterInfo parameter, HttpRequest request) {
            Type t = parameter.ParameterType;
            if (t == typeof(int) || t == typeof(long))
            {
                return request.Query.GetIntValue(parameter.Name);
            }
            else if (t == typeof(string))
            {
                return request.Query.GetStringValue(parameter.Name);
            }
            return null;
        }

        private object getValueFromBody(ParameterInfo parameter, HttpRequest request)
        {
            MethodInfo methodInfo = typeof(JsonSerializer).GetMethod("FromJson");
            return methodInfo.MakeGenericMethod(new Type[] { parameter.ParameterType }).Invoke(null, new object[] { request.Content });
            //return JsonSerializer.FromJson<parameter.ParameterType>(request.Content);
        }


    }
}
