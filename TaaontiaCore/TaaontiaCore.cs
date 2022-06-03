using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TaaontiaCore.Database;
using TaaontiaCore.Enums;
using TaaontiaCore.Events;
using TaaontiaCore.Services;

namespace TaaontiaCore
{
    public class TaaontiaCore
    {
        private readonly ServiceProvider _services;
        private readonly GameService _game;
        public GameService Game => _game;

        private readonly FightService _fight;
        public FightService Fight => _fight;

        private readonly PlayerService _player;
        public PlayerService Player => _player;

        public TaaontiaCore()
        {
            _services = ConfigureServices();

            _game = _services.GetRequiredService<GameService>();
            _fight = _services.GetRequiredService<FightService>();
            _player = _services.GetRequiredService<PlayerService>();
        }

        private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection()
                .AddSingleton<LoggingService>()
                .AddSingleton<GameService>()
                .AddSingleton<FightService>()
                .AddSingleton<PlayerService>()
                .AddDbContext<TaaontiaEntities>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
