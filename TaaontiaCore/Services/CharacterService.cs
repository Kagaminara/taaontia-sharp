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

namespace TaaontiaCore.Services
{
    public class CharacterService
    {
        private readonly TaaontiaEntities _db;

        public CharacterService(IServiceProvider services)
        {
            _db = services.GetRequiredService<TaaontiaEntities>();
        }

        public async Task<CharacterResult> FindConnectedPlayerCharacter(ulong remoteId)
        {
            Character connectedCharacter = await _db.Character.SingleOrDefaultAsync(character => character.Player.RemoteId == remoteId);
            if (connectedCharacter == null)
            {
                return new CharacterResult
                {
                    Result = EResult.FAILURE,
                    Error = ECharacterCreationError.CHARACTER_NOT_FOUND,
                };
            }

            return new CharacterResult
            {
                Result = EResult.SUCCESS,
                Character = connectedCharacter
            };
        }

        public async Task<CharacterResult> CreateCharacter(NewCharacter newCharacter)
        {
            var currentCharacter = await _db.Character.AnyAsync(c => c.Player.RemoteId == newCharacter.RemoteId);
            if (currentCharacter)
            {
                return new CharacterResult
                {
                    Result = EResult.ERROR,
                    Error = ECharacterCreationError.REMOTE_ID_ALREADY_EXISTS,
                };
            }

            var character = new Character()
            {
                Name = newCharacter.Name,
                Experience = 0,
                Level = 1,
                MaxHealth = 50,
                MaxEnergy = 50,
                Health = 50,
                Energy = 50,
                Player = new Player
                {
                    RemoteId = newCharacter.RemoteId,
                },
                Skills = new List<Skill>
                {
                    // Skills by default, mainly for the alpha phase.
                    // Later to be replaced by a skill selection by the client at character creation
                    _db.Skill.First(skill => skill.Id == 1),
                    _db.Skill.First(skill => skill.Id == 2)
                }
            };
            await _db.Character.AddAsync(character);
            await _db.SaveChangesAsync();

            return new CharacterResult
            {
                Result = EResult.SUCCESS,
                Character = character,
            };
        }

        public Character GetRandomEnemyCharacter()
        {
            var ids = _db.FiendType.ToArray();
            var fiendType = ids[new Random().Next(ids.Count())];
            return new Character(fiendType);
        }

    }
}
