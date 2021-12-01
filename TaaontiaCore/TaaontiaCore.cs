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

        private readonly FightService _fight;
        public FightService Fight => _fight;

        private readonly CharacterService _character;
        public CharacterService Character => _character;

        public TaaontiaCore()
        {
            _services = ConfigureServices();

            _game = _services.GetRequiredService<GameService>();
            _fight = _services.GetRequiredService<FightService>();
            _character = _services.GetRequiredService<CharacterService>();
        }

        private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection()
                .AddSingleton<LoggingService>()
                .AddSingleton<GameService>()
                .AddSingleton<FightService>()
                .AddSingleton<CharacterService>()
                .AddDbContext<TaaontiaEntities>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
