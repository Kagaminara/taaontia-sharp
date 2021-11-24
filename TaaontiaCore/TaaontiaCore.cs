using Microsoft.Extensions.DependencyInjection;
using System;
using TaaontiaCore.Database;
using TaaontiaCore.Services;

namespace TaaontiaCore
{
    public class TaaontiaCore : ITaaontiaCore
    {
        private readonly ServiceProvider _services;

        public TaaontiaCore()
        {
            _services = ConfigureServices();


        }

        private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection()
                .AddSingleton<LoggingService>()
                .AddDbContext<TaaontiaEntities>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        public void SayHello()
        {
            Console.Write("Hello");
        }
    }
}
