using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TaaontiaCore.Database;
using TaaontiaCore.Database.Models;
using TaaontiaCore.Events;

namespace TaaontiaCore.Services
{
    public class FightService
    {
        private readonly TaaontiaEntities _db;
        private readonly LoggingService _logging;
        private readonly CharacterService _character;

        public FightService(IServiceProvider services)
        {
            _db = services.GetRequiredService<TaaontiaEntities>();
            _logging = services.GetRequiredService<LoggingService>();
            _character = services.GetRequiredService<CharacterService>();
        }

        private async Task<FightResult> GetCurrentFight(ulong remoteId, bool global = false)
        {
            var charResult = await _character.FindConnectedPlayerCharacter(remoteId);
            if (charResult.Result == Enums.EResult.ERROR)
            {
                switch (charResult.Error)
                {
                    case Enums.ECharacterCreationError.CHARACTER_NOT_FOUND:
                        return new FightResult
                        {
                            Result = Enums.EResult.ERROR,
                            Error = Enums.EFightError.CHARACTER_NOT_FOUND,
                        };
                    case Enums.ECharacterCreationError.UNKNOWN_ERROR:
                    default:
                        return new FightResult
                        {
                            Result = Enums.EResult.ERROR,
                            Error = Enums.EFightError.UNKNOWN_ERROR,
                        };
                }
            }

            var fight = await _db.Fight.SingleOrDefaultAsync(
                fight => fight.IsActive &&
                fight.IsGlobal == global &&
                fight.Allies.Contains(charResult.Character));
            if (fight == null)
            {
                return new FightResult
                {
                    Result = Enums.EResult.FAILURE,
                    Error = Enums.EFightError.NO_CURRENT_FIGHT
                };
            }
            return new FightResult
            {
                Fight = fight,
            };
        }

        public async Task<FightResult> Engage(EventBase e)
        {
            var character = await _character.FindConnectedPlayerCharacter(e.RemoteId);
            if (character.Result != Enums.EResult.SUCCESS)
            {
                switch (character.Error)
                {
                    case Enums.ECharacterCreationError.CHARACTER_NOT_FOUND:
                        return new FightResult
                        {
                            Result = Enums.EResult.FAILURE,
                            Error = Enums.EFightError.CHARACTER_NOT_FOUND,
                        };
                    default:
                        return new FightResult
                        {
                            Result = Enums.EResult.ERROR,
                            Error = Enums.EFightError.UNKNOWN_ERROR,
                        };
                }
            }
            var currentFight = await GetCurrentFight(e.RemoteId);
            if (currentFight != null)
            {
                return new FightResult
                {
                    Result = Enums.EResult.FAILURE,
                    Error = Enums.EFightError.ALREADY_IN_FIGHT,
                };
            }

            var enemy = _character.GetRandomEnemyCharacter();

            var fight = new Fight
            {
                Allies = new List<Character>
                {
                    character.Character,
                },
                Fiends = new List<Character> { enemy },
                IsActive = true,
            };
            await _db.Fight.AddAsync(fight);
            await _db.SaveChangesAsync();

            return new FightResult
            {
                Result = Enums.EResult.SUCCESS,
                Fight = fight,
            };
        }

        public FightResult Flee()
        {
            return new FightResult();
        }

        public FightResult Action(FightEvent e)
        {
            return new FightResult();
        }
    }
}
