namespace RestServer.Common.Logger
{
    public class LoggerFactory
    {
        private static ILogger logger;
        public static ILogger GetLogger() {
            if (logger != null) {
                return logger;
            }
            logger = new ConsoleLogger();
            return logger;
        }

        public static void SetLogger(ILogger log) {
            logger = log;
        }
    }
}
