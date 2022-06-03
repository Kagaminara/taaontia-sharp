using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaaontiaCore.Database;
using TaaontiaCore.Database.Models;
using TaaontiaCore.Enums;
using TaaontiaCore.Events;
using TaaontiaCore.Interfaces;

namespace TaaontiaCore.Services
{
    public class PlayerService
    {
        private readonly TaaontiaEntities _db;

        public PlayerService(IServiceProvider services)
        {
            _db = services.GetRequiredService<TaaontiaEntities>();
        }

        public async Task<CharacterResult> FindConnectedPlayerCharacter(ulong remoteId)
        {
            Player connectedCharacter = await _db.Player.SingleOrDefaultAsync(player => player.RemoteId == remoteId);
            if (connectedCharacter == null)
            {
                return new CharacterResult
                {
                    Result = EResult.FAILURE,
                    Error = ECharacterError.CHARACTER_NOT_FOUND,
                };
            }

            return new CharacterResult
            {
                Result = EResult.SUCCESS,
                Character = connectedCharacter
            };
        }

        public async Task<CharacterResult> CreatePlayer(NewCharacter newPlayer)
        {
            var currentPlayer = await _db.Player.AnyAsync(c => c.RemoteId == newPlayer.RemoteId);
            if (currentPlayer)
            {
                return new CharacterResult
                {
                    Result = EResult.ERROR,
                    Error = ECharacterError.REMOTE_ID_ALREADY_EXISTS,
                };
            }

            var player = new Player()
            {
                Name = newPlayer.Name,
                Experience = 0,
                Level = 1,
                MaxHealth = 50,
                MaxEnergy = 50,
                Health = 50,
                Energy = 50,
                RemoteId = newPlayer.RemoteId,
                Skills = new List<Skill>
                {
                    // Skills by default, mainly for the alpha phase.
                    // Later to be replaced by a skill selection by the client at character creation
                    _db.Skill.First(skill => skill.Id == 1),
                    _db.Skill.First(skill => skill.Id == 2)
                }
            };
            await _db.Player.AddAsync(player);
            await _db.SaveChangesAsync();

            return new CharacterResult
            {
                Result = EResult.SUCCESS,
                Character = player,
            };
        }

        public Fiend GetRandomEnemyCharacter()
        {
            var ids = _db.FiendType.ToArray();
            var fiendType = ids[new Random().Next(ids.Count())];
            return new Fiend(fiendType);
        }

    }
}