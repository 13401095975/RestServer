using RestServer.Common.Logger;
using RestServer.Config;
using RestServer.Filter;
using RestServer.Http;
using RestServer.RestAttribute;
using RestServer.RouteHandler;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace RestServer
{
    public class RestApplicationServer
    {
        private ILogger logger;

        public RestApplicationServer() {
            logger = new ConsoleLogger();
        }


        public void run() {
            run(null);
        }

        public void run(RestConfiguration configuration) {
            logger.Info("RestApplicationServer starting...");

            Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
            FilterChain filterChain = AutoFilterScan(assemblyArray);

            List<Route> list = AutoComponentScan(assemblyArray);

            AutowiredScan(assemblyArray);

            if (configuration == null || configuration.StaticFileConfigurations == null || configuration.StaticFileConfigurations.Count == 0)
            {
                filterChain.AddRouteHandler(new FileSystemRouteHandler());
                logger.Info("add default static file mapping " + ServerConfig.DefaultStaticFilePrefix + " to " + ServerConfig.DefaultServerRoot);
            }
            else {
                foreach (StaticFileConfiguration staticFileConfiguration in configuration.StaticFileConfigurations)
                {
                    filterChain.AddRouteHandler(new FileSystemRouteHandler(staticFileConfiguration));
                    logger.Info("add static file mapping " + staticFileConfiguration.Prefix + " to " + staticFileConfiguration.BaseRoot);
                }
            }

            filterChain.AddRouteHandler(new ApiRouteHandler(list));

            HttpServer httpServer = new HttpServer(ServerConfig.Port, filterChain);

            Thread thread = new Thread(new ThreadStart(httpServer.Listen));
            thread.Start();
            logger.Info("RestApplicationServer started complete at port:"+ServerConfig.Port);
        }


        private FilterChain AutoFilterScan(IEnumerable<Assembly> assemblies)
        {
            logger.Info("auto filter scan start");
            FilterChain filterChain = new FilterChain();
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type t in assembly.GetTypes())
                {

                    object[] attrArray = t.GetCustomAttributes(typeof(WebFilterAttribute), false);
                    if (attrArray.Length == 0)
                    {
                        continue;
                    }

                    foreach (var item in attrArray)
                    {

                        WebFilterAttribute attribute = item as WebFilterAttribute;
                        logger.Info("add filter:" + t.FullName);
                        IFilter filter = (IFilter)ClassInstanceContext.GetInstance(t);

                        filterChain.AddFilter(attribute.Order, attribute.Prefix, filter);
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

                    object[] attrArray = t.GetCustomAttributes(typeof(ComponentAttribute), false);
                    if (attrArray.Length == 0)
                    {
                        continue;
                    }

                    ClassInstanceContext.AddBean(t);
                    logger.Debug("add bean:"+t.FullName);

                    MethodInfo[] methodInfos = t.GetMethods();
                    foreach (var methodInfo in methodInfos)
                    {
                        foreach (var attribute in methodInfo.GetCustomAttributes(typeof(RequestMappingAttribute), false))
                        {
                            RequestMappingAttribute attr = attribute as RequestMappingAttribute;
                            Route route = new Route();
                            route.Path = attr.Path;
                            route.Method = attr.Method;

                            object instance = ClassInstanceContext.GetInstance(t);
                            route.instance = instance;
                            route.method = methodInfo;

                            logger.Info("add route mapping " + route.Method + " " + route.Path + " to " + t.FullName + "." + methodInfo.Name);

                            list.Add(route);
                        }

                    }

                }
            }
            logger.Info("auto component complete");
            return list;

        }

        private void AutowiredScan(IEnumerable<Assembly> assemblies)
        {
            logger.Info("auto wired component start");
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type t in assembly.GetTypes())
                {

                    object[] attrArray = t.GetCustomAttributes(typeof(ComponentAttribute), false);
                    if (attrArray.Length == 0)
                    {
                        continue;
                    }

                    FieldInfo[]  fieldList = t.GetFields();

                    foreach (var item in fieldList)
                    {
                        foreach (var attribute in item.GetCustomAttributes(typeof(AutowiredAttribute), false))
                        {
                            AutowiredAttribute attr = attribute as AutowiredAttribute;
                            Type tp = item.FieldType;
                            object instance = ClassInstanceContext.GetInstance(t);
                            object wiredInstance = ClassInstanceContext.GetInstance(tp);
                            item.SetValue(instance, wiredInstance);

                            logger.Debug("autowired into " + t.FullName + "." + item.Name + "=" + tp.FullName);
                        }
                    }
                }
            }
            logger.Info("auto wired component complete");

        }

    }
}
