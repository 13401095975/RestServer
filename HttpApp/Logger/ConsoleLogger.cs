using System;

namespace HttpApp.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(string msg)
        {
            throw new NotImplementedException();
        }

        public void Error(string msg)
        {
            throw new NotImplementedException();
        }

        public void Info(string msg)
        {
            Console.WriteLine(LoggerTime()+"\t"+"[Info]"+"\t"+ msg);
        }

        public void Warn(string msg)
        {
            throw new NotImplementedException();
        }

        private string LoggerTime() {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
