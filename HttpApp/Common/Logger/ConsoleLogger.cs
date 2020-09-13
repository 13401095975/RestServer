using System;

namespace RestServer.Common.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(string msg)
        {
            Console.WriteLine(LoggerTime() + "\t" + "[Debug]" + "\t" + msg);
        }

        public void Error(string msg)
        {
            Console.WriteLine(LoggerTime() + "\t" + "[Error]" + "\t" + msg);
        }

        public void Info(string msg)
        {
            Console.WriteLine(LoggerTime()+"\t"+"[Info]"+"\t"+ msg);
        }

        public void Warn(string msg)
        {
            Console.WriteLine(LoggerTime() + "\t" + "[Warn]" + "\t" + msg);
        }

        private string LoggerTime() {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
