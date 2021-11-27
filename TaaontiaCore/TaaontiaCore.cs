using Microsoft.Extensions.DependencyInjection;
using TaaontiaCore.Database;
using TaaontiaCore.Services;

namespace TaaontiaCore
{
    public class TaaontiaCore
    {
        private readonly ServiceProvider _services;
        private readonly GameService _game;

        private readonly FightService _fight;
        public FightService Fight => _fight;

        public TaaontiaCore()
        {
            _services = ConfigureServices();

            _game = _services.GetRequiredService<GameService>();
            _fight = _services.GetRequiredService<FightService>();
        }

        private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection()
                .AddSingleton<LoggingService>()
                .AddSingleton<GameService>()
                .AddSingleton<FightService>()
                .AddDbContext<TaaontiaEntities>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
