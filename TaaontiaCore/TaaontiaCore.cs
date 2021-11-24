using Microsoft.Extensions.DependencyInjection;
using System;
using TaaontiaCore.Database;
using TaaontiaCore.Services;

namespace TaaontiaCore
{
    public class TaaontiaCore
    {
        private readonly ServiceProvider _services;

        public TaaontiaCore()
        {
            _services = ConfigureServices();

            var game = _services.GetRequiredService<GameService>();

        }

        private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection()
                .AddSingleton<LoggingService>()
                .AddSingleton<GameService>()
                .AddDbContext<TaaontiaEntities>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
