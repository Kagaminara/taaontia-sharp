using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TaaontiaCore.Database;
using TaaontiaCore.Events;

namespace TaaontiaCore.Services
{
    public class GameService
    {
        private readonly TaaontiaEntities _db;
        private readonly LoggingService _logging;
        private readonly PlayerService _player;
        private readonly FightService _fight;

        public GameService(IServiceProvider services)
        {
            _db = services.GetRequiredService<TaaontiaEntities>();
            _logging = services.GetRequiredService<LoggingService>();
            _player = services.GetRequiredService<PlayerService>();
            _fight = services.GetRequiredService<FightService>();
            // TODO: Add a StatService to do stats
        }

        // TODO: Rename this to something like "GameAction" or something to be more versatile
        public async Task<CharacterResult> HealCharacter(CharacterEvent e)
        {
            var charResult = await _player.FindConnectedPlayerCharacter(e.RemoteId);
            if (charResult.Result == Enums.EResult.ERROR)
            {
                return charResult;
            }

            var fightResult = await _fight.GetFight(charResult.Character);
            if (fightResult.Result == Enums.EResult.SUCCESS)
            {
                return new CharacterResult()
                {
                    Result = Enums.EResult.FAILURE,
                    Error = Enums.ECharacterError.CURRENTLY_IN_FIGHT,
                };
            }

            charResult.Character.Health = charResult.Character.MaxHealth;
            _db.Player.Update(charResult.Character);

            return charResult;
        }
    }
}
