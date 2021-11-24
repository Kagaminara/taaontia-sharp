using System;

namespace TaaontiaCore.Services
{
    public class LoggingService
    {
        private ConsoleColor _defaultColor;
        public LoggingService(IServiceProvider services)
        {
            _defaultColor = Console.ForegroundColor;
        }

        public void Log(string log)
        {
            Console.WriteLine(log);
        }

        public void Warn(string log)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[WARN] " + log);
            Console.ForegroundColor = _defaultColor;
        }

        public void Error(string log)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR] " + log);
            Console.ForegroundColor = _defaultColor;
        }
    }
}
