using System;

namespace TaaontiaCore.Services
{
    public class LoggingService
    {
        public LoggingService(IServiceProvider services)
        {
        }

        public void Log(string log)
        {
            Console.WriteLine(log);
        }
    }
}
