using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TaaontiaCore.Database;
using TaaontiaCore.Database.Models;
using TaaontiaCore.Events;
using TaaontiaCore.DTO;
using System.Collections.Generic;
using TaaontiaCore.Interfaces;

namespace TaaontiaCore.Services
{
    public class FightService
    {
        private readonly TaaontiaEntities _db;
        private readonly LoggingService _logging;
        private readonly PlayerService _player;

        public FightService(IServiceProvider services)
        {
            _db = services.GetRequiredService<TaaontiaEntities>();
            _logging = services.GetRequiredService<LoggingService>();
            _player = services.GetRequiredService<PlayerService>();
        }

        internal async Task<FightResult> GetFight(Player player)
        {
            return await _getCurrentFight(player.RemoteId);
        }

        private async Task<FightResult> _getCurrentFight(ulong remoteId, bool global = false)
        {
            var charResult = await _player.FindConnectedPlayerCharacter(remoteId);
            if (charResult.Result == Enums.EResult.ERROR)
            {
                switch (charResult.Error)
                {
                    case Enums.ECharacterError.CHARACTER_NOT_FOUND:
                        return new FightResult
                        {
                            Result = Enums.EResult.ERROR,
                            Error = Enums.EFightError.CHARACTER_NOT_FOUND,
                        };
                    case Enums.ECharacterError.UNKNOWN_ERROR:
                    default:
                        return new FightResult
                        {
                            Result = Enums.EResult.ERROR,
                            Error = Enums.EFightError.UNKNOWN_ERROR,
                        };
                }
            }

            var fight = await _db.Fight.Include(fight => fight.Fiend).SingleOrDefaultAsync(
                fight => fight.IsActive &&
                fight.IsGlobal == global &&
                fight.Player.RemoteId == charResult.Character.RemoteId);
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
                Result = Enums.EResult.SUCCESS,
                Fight = fight,
            };
        }

        public async Task<FightResult> Engage(EventBase e)
        {
            var charResult = await _player.FindConnectedPlayerCharacter(e.RemoteId);
            if (charResult.Result == Enums.EResult.ERROR)
            {
                switch (charResult.Error)
                {
                    case Enums.ECharacterError.CHARACTER_NOT_FOUND:
                        return new FightResult
                        {
                            Result = Enums.EResult.ERROR,
                            Error = Enums.EFightError.CHARACTER_NOT_FOUND,
                        };
                    case Enums.ECharacterError.UNKNOWN_ERROR:
                    default:
                        return new FightResult
                        {
                            Result = Enums.EResult.ERROR,
                            Error = Enums.EFightError.UNKNOWN_ERROR,
                        };
                }
            }
            if (charResult.Character.Health <= 0)
            {
                return new FightResult()
                {
                    Result = Enums.EResult.ERROR,
                    Error = Enums.EFightError.NOT_ENOUGH_HEALTH,
                };
            }

            var currentFight = await _getCurrentFight(e.RemoteId);
            if (currentFight.Result == Enums.EResult.SUCCESS)
            {
                return new FightResult
                {
                    Result = Enums.EResult.FAILURE,
                    Error = Enums.EFightError.ALREADY_IN_FIGHT,
                };
            }

            var enemy = _player.GetRandomEnemyCharacter();

            var fight = new Fight
            {
                Player = charResult.Character,
                Fiend = enemy,
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

        public async Task<FightResult> Flee(FightEvent e)
        {
            var fightResult = await _getCurrentFight(e.RemoteId);
            if (fightResult.Result != Enums.EResult.SUCCESS)
            {
                return fightResult;
            }

            fightResult.Fight.IsActive = false;
            await _db.SaveChangesAsync();

            return new FightResult()
            {
                Result = Enums.EResult.SUCCESS
            };
        }

        public async Task<FightResult> Action(FightEvent e)
        {
            var skill = await _db.Skill.SingleOrDefaultAsync(skill => skill.Id == e.SkillId);
            var fightResult = await _getCurrentFight(e.RemoteId);
            if (fightResult.Error != null)
            {
                return fightResult;
            }

            var deviation = new Random().Next(4) - 2;
            var damage = (skill.BaseTargetDamage ?? 0) + deviation;

            if (e.Target == Enums.EFightEventTarget.FIEND)
            {
                fightResult.Fight.Fiend.Health -= damage;
            }
            else
            {
                fightResult.Fight.Player.Health -= damage;
            }

            var result = new FightResult()
            {
                Result = Enums.EResult.SUCCESS,
                Fight = fightResult.Fight,
                TargetDamage = damage,
            };

            if (_isFightCompleted(fightResult.Fight))
            {
                result.Fight.IsActive = false;
                result.Rewards = new FightCompleteRewards()
                {
                    Currency = new Random().Next(10) + 10,
                    Experience = new Random().Next(10) + 10,
                    Items = new List<Item>()
                };
            }

            await _db.SaveChangesAsync();

            return result;
        }

        public async Task<FightResult> GetFight(FightEvent e)
        {
            var fightResult = await _getCurrentFight(e.RemoteId);
            if (fightResult.Result != Enums.EResult.SUCCESS)
            {
                return fightResult;
            }

            return fightResult;
        }

        private bool _isFightCompleted(Fight fight)
        {
            if (fight.Fiend.Health <= 0 || fight.Player.Health <= 0)
            {
                return true;
            }
            return false;
        }
    }
}
