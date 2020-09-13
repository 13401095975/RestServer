using HttpApp.attribute;
using HttpApp.Filter;
using HttpApp.Logger;
using SimpleHttpServer;
using SimpleHttpServer.Models;
using SimpleHttpServer.RouteHandlers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace HttpApp
{
    public class RestApplication
    {
        private ILogger logger;

        public RestApplication() {
            logger = new ConsoleLogger();
        }

        public void run() {
            FilterChain filterChain = AutoFilterScan(AppDomain.CurrentDomain.GetAssemblies());

            List<Route> list = AutoComponentScan(AppDomain.CurrentDomain.GetAssemblies());

            filterChain.AddRouteHandler(new FileSystemRouteHandler());
            filterChain.AddRouteHandler(new ApiRouteHandler(list));

            HttpServer httpServer = new HttpServer(ServerConfig.Port, filterChain);

            Thread thread = new Thread(new ThreadStart(httpServer.Listen));
            thread.Start();
        }

        private FilterChain AutoFilterScan(IEnumerable<Assembly> assemblies)
        {
            logger.Info("auto filter scan start");
            FilterChain filterChain = new FilterChain();
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type t in assembly.GetTypes())
                {

                    object[] controllerAttributeArray = t.GetCustomAttributes(typeof(WebFilterAttribute), false);
                    if (controllerAttributeArray == null || controllerAttributeArray.Length == 0)
                    {
                        continue;
                    }

                    foreach (var item in controllerAttributeArray)
                    {

                        WebFilterAttribute attribute = item as WebFilterAttribute;
                        if (attribute != null)
                        {
                            IFilter filter = (IFilter)ClassInstanceContext.GetInstance(t);

                            filterChain.AddFilter(attribute.Order, filter);
                        }
                    }

                }
            }
            logger.Info("auto filter scan complete");
            return filterChain;

        }

        private List<Route> AutoComponentScan(IEnumerable<Assembly> assemblies)
        {
            logger.Info("auto component scan start");
            List<Route> list = new List<Route>();
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type t in assembly.GetTypes())
                {

                    object[] controllerAttributeArray = t.GetCustomAttributes(typeof(ControllerAttribute), false);
                    if (controllerAttributeArray == null || controllerAttributeArray.Length == 0)
                    {
                        continue;
                    }

                    MethodInfo[] methodInfos = t.GetMethods();
                    foreach (var methodInfo in methodInfos)
                    {
                        foreach (var attribute in methodInfo.GetCustomAttributes(typeof(RequestMappingAttribute), false))
                        {
                            RequestMappingAttribute mappingAttribute = attribute as RequestMappingAttribute;
                            if (mappingAttribute != null)
                            {
                                Route route = new Route();
                                route.Path = mappingAttribute.Path;
                                route.Method = mappingAttribute.Method;

                                object instance = ClassInstanceContext.GetInstance(t);
                                route.instance = instance;
                                route.method = methodInfo;

                                logger.Info("mapping " + route.Path);

                                list.Add(route);
                            }
                        }

                    }

                }
            }
            logger.Info("auto component complete");
            return list;

        }



    }
}
