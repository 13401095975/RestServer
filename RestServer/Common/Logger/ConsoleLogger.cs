using System;

namespace RestServer.Common.Logger
{
    public class ConsoleLogger : ILogger
    {
        public LoggerType LoggerLevel { get; set; } = LoggerType.DEBUG;

        public void Debug(string msg)
        {
            Log(LoggerType.DEBUG, msg);
        }

        public void Error(string msg)
        {
            Log(LoggerType.ERROR, msg);
        }

        public void Info(string msg)
        {
            Log(LoggerType.INFO, msg);
        }

        public void Warn(string msg)
        {
            Log(LoggerType.WARN, msg);
        }

        private void Log(LoggerType level, string msg) {
            if ((int)level < (int)LoggerLevel) {
                return;
            }
            Console.WriteLine(LoggerTime() + "\t" + "["+ level.ToString() +"]" + "\t" + msg);
        }

        private string LoggerTime() {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
