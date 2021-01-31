using RestServer.Common.Logger;
using RestServer.Config;
using RestServer.Filter;
using RestServer.Http;
using RestServer.RestAttribute;
using RestServer.RouteHandler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace RestServer
{
    public class RestApplicationServer
    {
        private ILogger logger = LoggerFactory.GetLogger();

        HttpServer httpServer;

        Thread thread;

        public RestApplicationServer() {
        }

        public void run() {
            run(null);
        }

        public void run(RestConfiguration configuration) {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            logger.Info("RestApplicationServer starting...");

            var dllAssemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                        .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));

            var exeAssemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.exe")
                        .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));

            Assembly[] assemblyArray = dllAssemblies.Concat(exeAssemblies).ToArray();
            ProcessChain filterChain = AutoFilterScan(assemblyArray);

            List<Route> list = AutoComponentScan(assemblyArray);

            AutowiredScan(assemblyArray);

            if (configuration == null || configuration.StaticFileConfigurations == null || configuration.StaticFileConfigurations.Count == 0)
            {
                filterChain.AddRouteHandler(new FileRouteHandler());
                logger.Info("Add default static file mapping [" + HttpConfig.DefaultStaticFilePrefix + "] to " + HttpConfig.DefaultServerRoot);
            }
            else {
                foreach (StaticFileConfiguration staticFileConfiguration in configuration.StaticFileConfigurations)
                {
                    filterChain.AddRouteHandler(new FileRouteHandler(staticFileConfiguration));
                    logger.Info("Add static file mapping [" + staticFileConfiguration.Prefix + "] to " + staticFileConfiguration.BaseRoot);
                }
            }

            filterChain.AddRouteHandler(new ApiRouteHandler(list));

            httpServer = new HttpServer(ServerConfig.Port, filterChain);

            thread = new Thread(new ThreadStart(httpServer.Listen));
            thread.Start();
            logger.Info("RestApplicationServer started complete at port:"+ServerConfig.Port);

            stopwatch.Stop();
            logger.Info("Started Application in " + (stopwatch.ElapsedMilliseconds).ToString() + " millseconds");
        }


        private ProcessChain AutoFilterScan(IEnumerable<Assembly> assemblies)
        {
            logger.Info("Auto filter scan start");
            ProcessChain filterChain = new ProcessChain();
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = GetTypes(assembly);
                foreach (Type t in types)
                {

                    object[] attrArray = t.GetCustomAttributes(typeof(WebFilterAttribute), false);
                    if (attrArray.Length == 0)
                    {
                        continue;
                    }

                    foreach (var item in attrArray)
                    {

                        WebFilterAttribute attribute = item as WebFilterAttribute;
                        logger.Info("Mapping filter("+attribute.Order+"): [" + attribute.Prefix + "] onto [" + t.FullName + "]");
                        IFilter filter = (IFilter)ClassInstanceContext.GetInstance(t);

                        filterChain.AddFilter(attribute.Order, attribute.Prefix, filter);
                    }

                }
            }
            logger.Info("Auto filter scan complete");
            return filterChain;

        }

        private List<Route> AutoComponentScan(IEnumerable<Assembly> assemblies)
        {
            logger.Info("Auto component scan start");
            List<Route> list = new List<Route>();
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = GetTypes(assembly);
                foreach (Type t in types)
                {

                    object[] attrArray = t.GetCustomAttributes(typeof(ComponentAttribute), false);
                    if (attrArray.Length == 0)
                    {
                        continue;
                    }

                    ClassInstanceContext.AddBean(t);
                    logger.Debug("Add bean:"+t.FullName);

                    MethodInfo[] methodInfos = t.GetMethods();
                    foreach (var methodInfo in methodInfos)
                    {
                        foreach (var attribute in methodInfo.GetCustomAttributes(typeof(RequestMappingAttribute), false))
                        {
                            RequestMappingAttribute attr = attribute as RequestMappingAttribute;
                            Route route = new Route();
                            route.Path = attr.Path;
                            route.Method = attr.Method;

                            ParameterInfo[] parmInfo = methodInfo.GetParameters();
                            
                            object instance = ClassInstanceContext.GetInstance(t);
                            route.instance = instance;
                            route.method = methodInfo;
                            route.parameterInfo = parmInfo;

                            logger.Info("Mapped " + route.Method + " [" + route.Path + "] onto [" + t.FullName + "." + methodInfo.Name + "]");

                            list.Add(route);
                        }

                    }

                }
            }
            logger.Info("Auto component complete");
            return list;

        }

        private void AutowiredScan(IEnumerable<Assembly> assemblies)
        {
            logger.Info("Auto wired component start");
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = GetTypes(assembly);
                foreach (Type t in types)
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

                            logger.Debug("Autowired into " + t.FullName + "." + item.Name + "=" + tp.FullName);
                        }
                    }
                }
            }
            logger.Info("Auto wired component complete");

        }

        private Type[] GetTypes(Assembly assembly) {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                types = e.Types;
            }
            return types.Where(t => t != null).ToArray();
        }

        public void Shutdown() {
            httpServer.Shutdown();
            if (thread != null) {
                thread.Abort();
                while ((thread.ThreadState != System.Threading.ThreadState.Stopped) && (thread.ThreadState != System.Threading.ThreadState.Aborted))
                {
                    Thread.Sleep(10);
                }
            }
            
        }

    }
}
