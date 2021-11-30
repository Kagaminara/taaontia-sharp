using Discord.Commands;
using Discord_Bot.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaaontiaCore.Events;

namespace Discord_Bot.Modules
{
    [Name("Character")]
    [Summary("Character related commands")]
    public class CharacterModule : ModuleBase<SocketCommandContext>
    {
        private readonly TaaontiaCore.TaaontiaCore _game;

        public CharacterModule(IServiceProvider services)
        {
            _game = services.GetRequiredService<TaaontiaCore.TaaontiaCore>();
        }

        //[Command("heal")]
        //[Summary("Heal yourself")]
        //public async Task HealAsync()
        //{
        //    Character character = await _db.FindOrCreateConnectedCharacter(Context.User);

        //    int hpHealed = character.MaxHealth - character.Health;
        //    character.Health = character.MaxHealth;
        //    await _db.SaveChangesAsync();
        //    await ReplyAsync($"You healed yourself for {hpHealed}, you are now at full HP !");
        //}

        [Command("register")]
        [Summary("Create your character")]
        public async Task RegisterAsync()
        {
            var result = await _game.Character.CreateCharacter(new NewCharacter
            {
                Name = Context.User.Username,
                RemoteId = Context.User.Id,
            });

            if (result.Result == TaaontiaCore.Enums.EResult.FAILURE)
            {
                switch (result.Error)
                {
                    case TaaontiaCore.Enums.ECharacterCreationError.REMOTE_ID_ALREADY_EXISTS:
                        await ReplyAsync("Your character already exist !");
                        return;
                    case TaaontiaCore.Enums.ECharacterCreationError.UNKNOWN_ERROR:
                    default:
                        await ReplyAsync("There was an error, but I don't know what exactly :/");
                        return;
                }
            }
            await ReplyAsync($"Your character named {Context.User.Username} has been created !");
        }
    }
}
