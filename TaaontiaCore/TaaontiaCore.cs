using Microsoft.Extensions.DependencyInjection;
using TaaontiaCore.Database;
using TaaontiaCore.Events.Fight;
using TaaontiaCore.Services;

namespace TaaontiaCore
{
    public class TaaontiaCore
    {
        private readonly ServiceProvider _services;
        private readonly GameService _game;

        public TaaontiaCore()
        {
            _services = ConfigureServices();

            _game = _services.GetRequiredService<GameService>();
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

        public FightResult Fight(FightEvent fightEvent)
        {
            return _game.HandleFight(fightEvent);
        }
    }
}
