﻿using RestServer.Common.Logger;
using RestServer.Common.Serializer;
using RestServer.RestAttribute;
using System;
using System.Reflection;
using System.Text;

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

                object[] attrs = parameter.GetCustomAttributes(false);
                int count = 0;
                foreach (object attr in attrs)
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
                    else
                    {
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
            if (isIntegerType(t))
            {
                return request.Query.GetIntValue(parameter.Name);
            }
            else if (isStringType(t))
            {
                return request.Query.GetStringValue(parameter.Name);
            }
            return null;
        }

        private bool isIntegerType(Type t) {
            return t == typeof(int)
                || t == typeof(int?)
                || t == typeof(Int32)
                || t == typeof(UInt32)
                || t == typeof(Int64)
                || t == typeof(UInt64)
                || t == typeof(long);
        }

        private bool isStringType(Type t)
        {
            return t == typeof(string)
                || t == typeof(String)
                || t == typeof(string);
        }

        private object getValueFromBody(ParameterInfo parameter, HttpRequest request)
        {
            
            if (request.BodyBytes == null)
            {
                return null;
            }
            string content = Encoding.UTF8.GetString(request.BodyBytes);
            MethodInfo methodInfo = typeof(JsonSerializer).GetMethod("FromJson");
            return methodInfo.MakeGenericMethod(new Type[] { parameter.ParameterType }).Invoke(null, new object[] { content });
        }


    }
}
